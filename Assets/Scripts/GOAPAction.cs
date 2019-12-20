using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAction
{

    public enum Task
    {
        IDLE,
        DRINK
    }

    public enum Status
    {
        WAITING,
        DOING,
        COMPLETE,
        CANCELED
    }

    public enum Step
    {
        FINDING_LOCATION,
        MOVING_TO_LOCATION,
        AT_LOCATION
    }



    public Task task;
    public Status status;


    //private void Awake()
    //{
    //    task = Task.IDLE;
    //    status = Status.WAITING;
    //}

}
