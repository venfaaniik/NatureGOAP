using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    // Age, how old is the creature, decreases energy based on this value
    [SerializeField]
    private float age;
    // When this rises too high, send a path request to food? clamp 0-1?
    [SerializeField]
    private float hunger;
    // When this rises too high, send a path request to water? clamp 0-1?
    [SerializeField]
    private float thirst;
    // While this is above hunger and thirst, look for mate. clamp 0-1?
    [SerializeField]
    private float reproduction_urge;
    // How fast the creature moves 
    [SerializeField]
    private float max_speed;
    // Can move fast / wants to sleep
    [SerializeField]
    private float energy;
    // Vision range, if food/water not in sight, wander around until something in sight
    [SerializeField]
    private int sight_radius;
    // Determinedness, affects how likely the creature changes direction while wandering
    [SerializeField]
    private float determinedness;
    // Diet type, what does the creature eat: 0 vegetarian, 1 carnivore, 2 both
    [SerializeField]
    private int diet_type;
    // Gender, male female
    [SerializeField]
    private bool is_male;

    // We never initialize a Creature using this?
    public Creature()
    {

    }

    // get path to water/food?
    public void GetPathToTarget(Vector3 some_place)
    {
        
    }
    
    public void Wander()
    {
        //move around aimlessly, more determinedness = moves longer in one direction 
        
    }

    public void FindMate()
    {
        bool targetGender = is_male ? false : true;

        Vector3 worldPos = transform.position;

        for (int x = 0; x < sight_radius; x++)
        {
            for (int y = 0; y < sight_radius; y++)
            {
                // if grid at position x, y contains a potential mate, request a path to it
                if (/*grid[x,y].Contains(targetGender)*/true)
                {
                    // RequestPath(target);
                }
            }
        }
    }

    /// <summary>
    /// Find food in sight_radius, request a path to food, eat
    /// </summary>
    public void FindFood()
    {
        
    }

    /// <summary>
    /// Find Deer to hunt in sight_radius, request a path to deer, eat
    /// </summary>
    public void Hunt()
    {
        Vector3 worldPos = transform.position;


    }

    /// <summary>
    /// Find water in sight_radius, request a path to water, drink
    /// </summary>
    public void FindWater()
    {



    }

    /// <summary>
    /// What the creature does based on parameters
    /// </summary>
    /// <param name="type">0: Vegetarian, 1: Carnivore, 2: Both</param>
    public void BehaviourTree(int type)
    {
        // if the creature has no hunger or thirst, try to reproduce
        if (hunger < reproduction_urge && thirst < reproduction_urge)
        {
            // Find a mate to reproduce with
            FindMate();
        }

        if (hunger > reproduction_urge && type == 0 || type == 2)
        {
            // Find food to eat based on diet
            FindFood();
        }

        if (hunger > reproduction_urge && type == 1)
        {
            Hunt();
        }

        if (thirst > reproduction_urge)
        {
            FindWater();
        }
        
    }

    /// <summary>
    /// Kuole
    /// </summary>
    public void Die()
    {
        //play some death animation?
        print("die");
        //deletes gameObject from scene
        Destroy(gameObject);
    }
    
    public float Age
    {
        get
        {
            return age;
        }
        set
        {
            age = value;
        }
    }

    public float Hunger
    {
        get
        {
            return hunger;
        }
        set
        {
            hunger = value;
        }
    }

    public float Thirst
    {
        get
        {
            return thirst;
        }
        set
        {
            thirst = value;
        }
    }

    public float ReproductionUrge
    {
        get
        {
            return reproduction_urge;
        }
        set
        {
            reproduction_urge = value;
        }
    }

    public float MaxSpeed
    {
        get
        {
            return max_speed;
        }
        set
        {
            max_speed = value;
        }
    }

    public float Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
        }
    }

    public int SightRadius
    {
        get
        {
            return sight_radius;
        }
        set
        {
            sight_radius = value;
        }
    }

    public float Determinedness
    {
        get
        {
            return determinedness;
        }
        set
        {
            determinedness = value;
        }
    }

    public int DietType
    {
        get
        {
            return diet_type;
        }
        set
        {
            diet_type = value;
        }
    }

    public bool IsMale
    {
        get
        {
            return is_male;
        }
        set
        {
            is_male = value;
        }
    }
}
