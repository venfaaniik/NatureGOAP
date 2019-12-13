using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [Range(0, 1.1f)]
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
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    GOAPAction action;
    [SerializeField]
    GOAPPlanner planner;

    private void Awake()
    {
        planner = GetComponent<GOAPPlanner>();
        action = GetComponent<GOAPAction>();
        agent = GetComponent<NavMeshAgent>();

    }



    private void Update()
    {
        Debug.Log(agent.remainingDistance);
        //Check action
        //call that task if action.type = waiting
        
    }

    public void DrinkTask()
    {
        //action.status = GOAPAction.ActionType.DOING;
        FindRequest callback = FindDrinkingSpot();
        if (callback.successful)
        {
            bool moving = false;
            agent.SetDestination(callback.go.transform.position);

            while (agent.pathPending)
            {

            }

            
            moving = true;
            if (agent.remainingDistance < 2)
            {
                Drink();
                Debug.Log("We got here");
            }


        }

        //action.currentStatus = GOAPAction.Status.DONE;
        //Send message to GOAPPlaner
        //Remove task from que
    }

    public void WanderTask()
    {
        //action.status = GOAPAction.ActionType.DOING;
    }

    public void IdleTask()
    {
        //action.status = GOAPAction.ActionType.DOING;
    }

    //Finds nearest water place and finds a open drinking spot
    public FindRequest FindDrinkingSpot()
    {
        FindRequest callback = new FindRequest();
        callback.successful = false;
        GameObject water = new GameObject();
        GameObject[] go;
        go = GameObject.FindGameObjectsWithTag("Water");


        float distance = 100000;
        if (go.Length != 0)
        {
            foreach (GameObject g in go)
            {
                if (distance > Vector3.Distance(g.transform.position, this.transform.position))
                {
                    water = g;
                    distance = Vector3.Distance(g.transform.position, this.transform.position);
                }
            }

            for (int i = 0; i < water.transform.childCount; ++i)
            {
                if (!water.transform.GetChild(i).GetComponent<DrinkingSpot>().occupied)
                {
                    callback.go = water.transform.GetChild(i).gameObject;
                    callback.successful = true;
                    break;
                }
            }
        }

        return callback;
    }

    /// <summary>
    /// Drinks till not thirsty
    /// </summary>
    public void Drink()
    {
        while (thirst > 0.02)
        {
            thirst -= 0.1f * Time.deltaTime;
        }

        //action.status = GOAPAction.ActionType.DONE
    }
    
    //Abstract methods
    public abstract FindRequest FindFood();
    public abstract FindRequest FindMate();
    public abstract void EatTask();
    public abstract void MateTask();

    /// <summary>
    /// 
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

    public class FindRequest
    {
        public bool successful { get; set; }
        public GameObject go { get; set; }
    }

}
