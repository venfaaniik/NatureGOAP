using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    Deer deer;
    //public Queue<GOAPAction> current_actions = new Queue<GOAPAction>();
    GOAPAction.Action[] actions = new GOAPAction.Action[20];  //Let's use array instead of queue

    private void Awake()
    {
        deer = GetComponent<Deer>();
        //current_actions.Enqueue(new GOAPAction(GOAPAction.ActionType.IDLE));
        //GOAPAction newAction = new GOAPAction(GOAPAction.ActionType.DRINK);
        GOAPAction.Action newAction;
        newAction.status = GOAPAction.Status.WAITING;
        newAction.type = GOAPAction.ActionType.DRINK;

        actions[0] = newAction;
        //current_actions.Enqueue(new GOAPAction(GOAPAction.ActionType.DRINK));

        ExecuteAction(actions[0].type);

    }


    void ExecuteAction(GOAPAction.ActionType type)
    {
        switch (type)
        {
            case GOAPAction.ActionType.DRINK:
                deer.DrinkTask();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeObjective();
        
    }

    //public void ChangeObjective()
    //{
    //    // if the creature has no hunger or thirst, try to reproduce
    //    if (deer.Hunger < deer.ReproductionUrge
    //     && deer.Thirst < deer.ReproductionUrge
    //     && deer.IsMature)
    //    {
    //        // Find a mate to reproduce with
    //        current_actions.Enqueue(new GOAPAction(GOAPAction.ActionType.MATE));
    //    }

    //    if (deer.Hunger > deer.ReproductionUrge)
    //    {
    //        //for (int i = 0; i < current_actions.Count; i++)
    //        //{
    //        //    if (current_actions[i].current_action_type == GOAPAction.ActionType.EAT)
    //        //    {
                    
    //        //    }
    //        //}

    //        //Parempi tapa
    //        foreach (GOAPAction obj in current_actions)
    //        {
    //            if (obj.current_action_type == GOAPAction.ActionType.EAT)
    //            {

    //            }
    //        }

    //        // start food action
    //        current_actions.Enqueue(new GOAPAction(GOAPAction.ActionType.EAT));
    //    }

    //    if (deer.Thirst > deer.ReproductionUrge)
    //    {
    //        // start drink action
    //        current_actions.Enqueue(new GOAPAction(GOAPAction.ActionType.DRINK));
    //    }

    //}

}
