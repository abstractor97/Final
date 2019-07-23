using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTime
{
    public string nowTime = "00:00";

    public float speed = 1;
    private int hour = 0;
    private int minute = 0;
    public delegate void TimeCallBack(string daytime);
    public event TimeCallBack callback;



    public void StartDay(MonoBehaviour mono)
    {
        mono. StartCoroutine(Run());
    }
    public void StopDay(MonoBehaviour mono)
    {
        mono.StopAllCoroutines();
    }

    IEnumerator Run() {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (minute < 59)
            {
                minute += (int)(1 * speed);
            }
            else
            {
                hour++;
                minute = 0;
                if (hour == 24)
                {
                    hour = 0;
                }
            }
            nowTime = hour + ":" + minute;
            callback(nowTime);
        }       
    }
}
