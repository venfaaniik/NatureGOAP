using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    //Handles planning tasks and puts them in queue to complete

    public Queue<GOAPAction> actionQueue = new Queue<GOAPAction>();

    public Creature creature;

    bool taskActive;

    private void Start()
    {
        creature = GetComponent<Creature>();
        //Debug start
        GOAPAction drink = new GOAPAction();
        drink.task = GOAPAction.Task.DRINK;
        drink.status = GOAPAction.Status.WAITING;
        QueueTask(drink);
        StartTask();
        //Debug end
    }

    private void Update()
    {
        Debug.Log("Task count:" + actionQueue.Count);
    }

    public void TaskCompeleted()
    {
        if (actionQueue.Count != 0)
        {
            actionQueue.Dequeue();
            taskActive = false;
        }

        if (actionQueue.Count != 0)
        {
            StartTask();
        }
    }

    public void CancelTask()
    {
        if (actionQueue.Count != 0)
        {
            actionQueue.Dequeue();
            taskActive = false;
        }
    }

    void StartTask()
    {
        if (actionQueue.Count > 0)
        {
            CallTask(actionQueue.Peek());
        }
        else
        {
            Debug.LogError("GOAPPlanner queue was empty");
        }
    }

    void QueueTask(GOAPAction a)
    {
        Debug.Log("Trying to que task");
        if (actionQueue.Count != 0)
        {
            if (!actionQueue.Contains(a))
            {
                if (actionQueue.Peek().task != a.task)
                {
                    actionQueue.Enqueue(a);
                }
            }
        }
        else
        {
            actionQueue.Enqueue(a);
            Debug.Log("Queued a task");
        }
        
    }

    void CallTask(GOAPAction a)
    {
        switch (a.task)
        {
            case GOAPAction.Task.IDLE:
                Debug.Log("IdleTask");
                break;
            case GOAPAction.Task.DRINK:
                Debug.Log("DrinkTask");
                creature.StartDrinkTask();
                break;
        }

        taskActive = true;
    }
}
