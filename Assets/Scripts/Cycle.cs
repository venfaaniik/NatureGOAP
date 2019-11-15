using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cycle : MonoBehaviour
{
    public Transform sun;
    public Transform moon;
    public Text cycle;

    private int hour = 0;
    private int minute = 0;
    private int second = 0;
    void Start()
    {
        StartCoroutine(ExecuteAfterTime());
        Time.timeScale = 50;
    }
    private void Update()
    {
        // Sun and Moon objects spinning 360°
        sun.transform.RotateAround(Vector3.zero, Vector3.right, 1f * Time.deltaTime);
        sun.transform.LookAt(Vector3.zero);
        moon.transform.RotateAround(Vector3.zero, Vector3.right, 1f * Time.deltaTime);
        moon.transform.LookAt(Vector3.zero);

    }
    IEnumerator ExecuteAfterTime()
    {
        while(true)
        {
        yield return new WaitForSeconds(1);
             cycle.text = hour + ":" + minute + ":" + second;
             second++;

        if (second > 59)
            {
               second = 0;
               minute++;
                if (minute > 59)
                {
                   minute = 0;
                   hour++;
                    if (hour > 23)
                    {
                       hour = 0;
                       minute = 0;
                       second = 0;
                    }
                }
            }
        }
    }
}