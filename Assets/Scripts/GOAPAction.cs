using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAction : MonoBehaviour
{
    public Status currentStatus;

    public ActionType current_action_type;

    public enum ActionType
    {
        IDLE,
        WANDER,
        DRINK,
        EAT,
        SLEEP,
        MATE
    };

    public struct Action
    {
       public  ActionType type;
       public Status status;
    }

    public enum Status
    {
        WAITING,
        DOING,
        DONE
    };

}
