using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cycle : MonoBehaviour
{
    double hour = 0.0;
    double minute = 0.0;
    double second = 0.0;
    private Text cycle;
    void Start()
    {
        StartCoroutine(ExecuteAfterTime());
    }
    IEnumerator ExecuteAfterTime()
    {
        while(hour != 50)
        {
        yield return new WaitForSeconds(1);
        cycle = GetComponent<Text>();
        cycle.text = hour + ":" + minute + ":" + second;
        second++;

        if (second > 59)
        {
            second = 0.0;
            minute++;
            if (minute > 59)
            {
                minute = 0.0;
                hour++;
                if (hour > 23)
                {
                    hour = 0.0;
                    minute = 0.0;
                    second = 0.0;
                }
            }
        }
        }
    }
}