using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAI : MonoBehaviour
{
    Deer deer;
    // if deer is idle, get a thing to do
    [SerializeField]
    private bool is_idle;

    void Awake()
    {
        deer = GetComponent<Deer>();

    }

    void Update()
    {
        if (is_idle)
        {
            BehaviourTree();
        }    
    }

    public void BehaviourTree()
    {
        // if the creature has no hunger or thirst, try to reproduce
        if (deer.Hunger < deer.ReproductionUrge 
         && deer.Thirst < deer.ReproductionUrge 
         && deer.IsMature)
        {
            // Find a mate to reproduce with
            deer.FindMate();
        }

        if (deer.Hunger > deer.ReproductionUrge)
        {
            // Find food to eat based on diet
            deer.FindFood();
        }

        if (deer.Hunger > deer.ReproductionUrge)
        {
            deer.FindWater();
        }



    }
    
}
