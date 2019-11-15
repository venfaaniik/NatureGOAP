using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
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
    private float sight_radius;
    // Determinedness, affects how likely the creature changes direction while wandering
    [SerializeField]
    private float determinedness;
    // Diet type, what does the creature eat: 0 vegetarian, 1 carnivore, 2 both
    [SerializeField]
    private int diet_type;
    // Gender, male female
    [SerializeField]
    private bool is_male;
    // Is mature, can reproduce
    [SerializeField]
    private bool is_mature;

    // We never initialize a Creature using this?
    public Creature()
    {

    }

    // get path to water/food?
    public void GetPathToTarget(Vector3 some_place)
    {
        
    }
    
    public void Wander(int targetType)
    {
        //move around aimlessly, more determinedness = moves longer in one direction 

        bool targetFound = false;

        // Find all gameobjects in sight_radius
        Collider[] gameObjectsInRange = Physics.OverlapSphere(transform.position, sight_radius);
        
        // Loops through all the found objects

        /*while (!targetFound)
        {
        }*/
    }

    public abstract void FindMate();

    /// <summary>
    /// Find food in sight_radius, request a path to food, eat
    /// </summary>
    public void FindFoodInRange(Collider[] gameObjectsInRange)
    {
        int i = 0;
        while (i < gameObjectsInRange.Length)
        {
            if (gameObjectsInRange[i].gameObject.CompareTag("Food"))
            {
                GameObject target = gameObjectsInRange[i].gameObject;

                print(gameObjectsInRange[i].transform.position + "food found");

                // request path to target. eppu do

            }

            i++;
        }


    }

    /// <summary>
    /// Find water in sight_radius, request a path to water, drink
    /// </summary>
    public void FindWaterInRange(Collider[] gameObjectsInRange)
    {
        int i = 0;
        while (i < gameObjectsInRange.Length)
        {
            if (gameObjectsInRange[i].gameObject.CompareTag("Water"))
            {
                GameObject target = gameObjectsInRange[i].gameObject;

                print(gameObjectsInRange[i].transform.position + "water found");

                // request path to target. eppu do
              
            }

            i++;
        }


    }

    /// <summary>
    /// Kuole
    /// </summary>
    public void Die()
    {
        //play some death animation? yes
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

    public float SightRadius
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

    public bool IsMature
    {
        get
        {
            return is_mature;
        }
        set
        {
            is_mature = value;
        }
    }
}
