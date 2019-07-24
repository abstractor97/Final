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
    public float scale=0.1f;
    public TimeSpeed timeSpeed=TimeSpeed.wait;

    public string targetTime;
    public int day;
    public bool isRun;

    public void StartDay(MonoBehaviour mono)
    {
        if (!isRun)
        {
            mono.StartCoroutine(Run());
            isRun = true;
        }    
    }
    public void StopDay(MonoBehaviour mono)
    {
        mono.StopAllCoroutines();
        isRun = false;
    }

    public void ChangeSpeed(TimeSpeed timeSpeed) {
        this.timeSpeed = timeSpeed;
    }

    public void JumpTime(string time)
    {
 
        string[] ts = time.Split(':');

        int thour = hour + int.Parse(ts[0]);
      
        int tmin= minute+ int.Parse(ts[1]);
        if (tmin>59)
        {
            thour++;
            tmin -= 60;
        }
        if (thour > 23)
        {
            thour -= 24;
        }
        targetTime = thour+":"+tmin;
        timeSpeed = TimeSpeed.jump;
    }

    IEnumerator Run() {
        while (true)
        {
            switch (timeSpeed)
            {
                case TimeSpeed.wait:
                    scale = 0.1f;
                    break;
                case TimeSpeed.walk:
                    scale = 1f;
                    break;
                case TimeSpeed.jump:
                    scale = 60f;
                    break;
            }
            yield return new WaitForSeconds(scale);
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
            if (nowTime.Equals(targetTime))
            {
                timeSpeed = TimeSpeed.wait;
            }
        }       
    }

    public enum TimeSpeed
    {
        wait,
        walk,
        jump,
    }
}
