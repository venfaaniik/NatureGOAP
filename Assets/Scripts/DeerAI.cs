using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAI : MonoBehaviour
{
    Deer deer;
    
    void Awake()
    {
        deer = GetComponent<Deer>();

    }

    void Update()
    {
        ChangeObjective();
        BehaviourTree();
    }

    public void ChangeObjective()
    {
        //// if the creature has no hunger or thirst, try to reproduce
        //if (deer.Hunger < deer.ReproductionUrge
        // && deer.Thirst < deer.ReproductionUrge
        // && deer.IsMature)
        //{
        //    // Find a mate to reproduce with
        //    deer.FindMate();
        //}

        //if (deer.Hunger > deer.ReproductionUrge)
        //{
        //    deer.FindFood();
        //}

        //if (deer.Thirst > deer.ReproductionUrge)
        //{
        //    deer.FindWater();
        //}

    }

    public void BehaviourTree()
    { 
        Collider[] gameObjectsInRange = Physics.OverlapSphere(transform.position, deer.SightRadius);
    }



    
}
