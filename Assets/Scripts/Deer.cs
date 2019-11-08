using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Creature
{
    System.Random random = new System.Random();

    /// <summary>
    /// Create a Deer with these parametres:
    /// </summary>
    /// <param name="_age">Set to 0 please, increases as time passes</param>
    /// <param name="_hunger">Set to 0 please, increases as time passes + inverse energy, goes to 0 after eating</param>
    /// <param name="_thirst">Set to 0 please, increases as time passes + inverse energy, goes to 0 after drinking</param>
    /// <param name="_reproduction_urge">Set to 0 please, increases as time passes, goes to 0 after reproducing</param>
    /// <param name="_max_speed">0-1, goes down as age increases</param>
    /// <param name="_energy">set to 1 please, decreases as time passes, increases while idling/resting</param>
    /// <param name="_sight_radius">set to 10-50??, decreases as age increases</param>
    /// <param name="_determinedness">set to 0-1, how much does the creature change direction while wandering</param>
    /// <param name="_diet_type">0: vegetarian</param>
    /// <param name="_is_male">50-50 is the creature a male when born</param>

    public Deer(float _age, float _hunger, float _thirst, float _reproduction_urge, float _max_speed, float _energy, float _sight_radius, float _determinedness, int _diet_type, bool _is_male, bool _is_mature)
    {
        Age = _age;
        Hunger = _hunger;
        Thirst = _thirst;
        ReproductionUrge = _reproduction_urge;
        MaxSpeed = _max_speed;
        Energy = _energy;
        SightRadius = _sight_radius;
        Determinedness = _determinedness;
        DietType = _diet_type;
        IsMale = _is_male;
    }

    // Start is called before the first frame update
    void Start()
    {
        SightRadius = 10f;
        ReproductionUrge = 1.0f;
        gameObject.tag = "Deer";
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        // what even is this v
        Age += 0.1f * dt;
        Hunger += (0.001f - (Energy / 2048)) * dt;
        Thirst += 0.0015f * dt;
        Energy -= 0.020f * dt;
        ReproductionUrge += 0.001f * dt;


        Hunger = Mathf.Clamp01(Hunger);
        Thirst = Mathf.Clamp01(Thirst);
        Energy = Mathf.Clamp01(Energy);
        ReproductionUrge = Mathf.Clamp01(ReproductionUrge);

        if (Thirst >= 1.0f || Hunger >= 1.0f)
        {
            Die();
        }

        if (Age > 10)
        {
            IsMature = true;
        }

        BehaviourTree(0);

    }

    public override void FindMate()
    {
        // Determine what gender we are looking for
        bool targetGender = IsMale ? false : true;

        // Find all gameobjects in sight_radius
        Collider[] gameObjectsInRange = Physics.OverlapSphere(transform.position, SightRadius);

        int i = 0;

        // Loops through all the found objects
        while (i < gameObjectsInRange.Length)
        {
            if (gameObjectsInRange[i].gameObject.CompareTag("Deer"))
            {
                Deer target = gameObjectsInRange[i].GetComponent<Deer>();
                //Deer target = (Deer)FindObjectOfType(typeof(Deer));
                if (isSuitable(target))
                {
                    print(gameObjectsInRange[i].transform.position + "deer found, IS SUITABLE, GO GOOGOGOGOGO");
                }
                else
                {
                    //print(gameObjectsInRange[i].transform.position + "deer found, not suitable");
                }
            }

            i++;
        }

        // if grid at position x, y contains a potential mate, request a path to it
        if (/*grid[x,y].Contains(targetGender)*/true)
        {
            // RequestPath(target);
        }

    }

    bool isSuitable(Deer target)
    {
        // Is target of the right gender
        bool suitableGender;
        if (IsMale && !target.IsMale)
        {
            suitableGender = true;
        }
        else if (!IsMale && target.IsMale)
        {
            suitableGender = true;
        }
        else
        {
            suitableGender = false;
        }

        // If both requirements are met
        if (suitableGender && target.IsMature)
        {
            return true;
        }

        return false;
    }

}
