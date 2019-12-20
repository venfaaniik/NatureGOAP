using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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


    public NavMeshAgent agent;
    public GOAPAction activeTask;
    public GOAPPlanner planner;
    bool moving = false;
    Vector3 target;
    bool drinking = false;

    GOAPAction.Step step;

    private void Awake()
    {
        activeTask = new GOAPAction();
        planner = GetComponent<GOAPPlanner>();
        agent = GetComponent<NavMeshAgent>();
        //Debug start
        age = 1;
        hunger = 0;
        thirst = 0;
        reproduction_urge = 0;
        max_speed = 10;
        energy = 100;
        sight_radius = 100;
        determinedness = 0;
        diet_type = 0;
        is_male = true;
        //Debug end
    }

    private void Update()
    {
        UpdateMoving();
        CheckTaskProgress();
    }

    public void StartDrinkTask()
    {
        activeTask.task = GOAPAction.Task.DRINK;
        activeTask.status = GOAPAction.Status.DOING;
        step = GOAPAction.Step.FINDING_LOCATION;

        Debug.Log("Task type and status " + activeTask.task + " : " + activeTask.status);


        DrinkTask();

        
    }


    void DrinkTask()
    {
        GameObject drinkingSpot = new GameObject();

        if (FindDrinkingSpot(ref drinkingSpot))
        {
            Debug.Log(drinkingSpot.name);

            GoToPosition(drinkingSpot.transform.position);

            step = GOAPAction.Step.MOVING_TO_LOCATION;

        }
        else
        {
            activeTask.status = GOAPAction.Status.CANCELED;
            planner.CancelTask();
        }
    }

    bool FindDrinkingSpot(ref GameObject callback)
    {
        GameObject[] waters = GameObject.FindGameObjectsWithTag("Water");
        GameObject nearestWater = new GameObject();
        if (waters.Length > 0)
        {
            float dist = 10000f;
            foreach (GameObject go in waters)
            {
                if (Vector3.Distance(this.gameObject.transform.position, go.transform.position) < dist)
                {
                    dist = Vector3.Distance(this.gameObject.transform.position, go.transform.position);
                    nearestWater = go;
                }
            }

            if (nearestWater.transform.childCount > 0 )
            {
                //See if spot occupied
                callback = nearestWater.transform.GetChild(0).gameObject;
                return true;
                    
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void GoToPosition(Vector3 pos)
    {
        agent.SetDestination(pos);
        moving = true;
    }

    void UpdateMoving()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                     moving = false;
                    step = GOAPAction.Step.AT_LOCATION;
                }
            }
            else
            {
                moving = false;
            }
        }
    }

    void CheckTaskProgress()
    {
        if (activeTask.task == GOAPAction.Task.DRINK)
        {
            if (step == GOAPAction.Step.AT_LOCATION && !drinking)
            {
                Drink();
            }
        }
    }

    void Drink()
    {
        drinking = true;
        while (thirst > 0)
        {
            thirst += 0.0001f * Time.deltaTime;
        }

        //Done drinking
        TaskCompleted();
        drinking = false;
    }

    void TaskCompleted()
    {
        activeTask.status = GOAPAction.Status.COMPLETE;
        planner.TaskCompeleted();
    }

    //OLD CODE-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //// get path to water/food?
    //public void GetPathToTarget(Vector3 some_place)
    //{

    //}

    //public void Wander()
    //{
    //    //move around aimlessly, more determinedness = moves longer in one direction 

    //}

    //public void FindMate()
    //{
    //    bool targetGender = is_male ? false : true;

    //    Vector3 worldPos = transform.position;

    //    for (int x = 0; x < sight_radius; x++)
    //    {
    //        for (int y = 0; y < sight_radius; y++)
    //        {
    //            // if grid at position x, y contains a potential mate, request a path to it
    //            if (/*grid[x,y].Contains(targetGender)*/true)
    //            {
    //                // RequestPath(target);
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// Find food in sight_radius, request a path to food, eat
    ///// </summary>
    //public void FindFood()
    //{

    //}

    ///// <summary>
    ///// Find Deer to hunt in sight_radius, request a path to deer, eat
    ///// </summary>
    //public void Hunt()
    //{
    //    Vector3 worldPos = transform.position;


    //}

    ///// <summary>
    ///// Find water in sight_radius, request a path to water, drink
    ///// </summary>
    //public void FindWater()
    //{



    //}

    ///// <summary>
    ///// What the creature does based on parameters
    ///// </summary>
    ///// <param name="type">0: Vegetarian, 1: Carnivore, 2: Both</param>
    //public void BehaviourTree(int type)
    //{
    //    // if the creature has no hunger or thirst, try to reproduce
    //    if (hunger < reproduction_urge && thirst < reproduction_urge)
    //    {
    //        // Find a mate to reproduce with
    //        FindMate();
    //    }

    //    if (hunger > reproduction_urge && type == 0 || type == 2)
    //    {
    //        // Find food to eat based on diet
    //        FindFood();
    //    }

    //    if (hunger > reproduction_urge && type == 1)
    //    {
    //        Hunt();
    //    }

    //    if (thirst > reproduction_urge)
    //    {
    //        FindWater();
    //    }

    //}

    ///// <summary>
    ///// Kuole
    ///// </summary>
    //public void Die()
    //{
    //    //play some death animation? yes
    //    print("die");
    //    //deletes gameObject from scene
    //    Destroy(gameObject);
    //}
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
