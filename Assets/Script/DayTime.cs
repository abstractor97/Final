using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTime:MonoBehaviour
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
    public bool isStop;

    public Text hourText;
    public Text minsText;

    private void Start()
    {
        StartDay();
      //  DontDestroyOnLoad(gameObject);
    }

    public void StartDay()
    {
        StartCoroutine(Run());
        isRun = true;
        
    }
    public void StopAll()
    {
        StopAllCoroutines();
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

    IEnumerator Run()
    {
        while (true)
        {
            switch (timeSpeed)
            {
                case TimeSpeed.wait:
                    scale = 2f;
                    break;
                case TimeSpeed.walk:
                    scale = 0.4f;
                    break;
                case TimeSpeed.jump:
                    scale = 0.1f;
                    break;
            }
            yield return new WaitForSeconds(scale);
            if (!isStop)
            {
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
                string hours = hour.ToString();
                if (hour < 10)
                {
                    hours = "0" + hours;
                }
                string minutes = minute.ToString();
                if (minute < 10)
                {
                    minutes = "0" + minutes;
                }
                nowTime = hours + ":" + minutes;

                if (nowTime.Equals(targetTime))
                {
                    timeSpeed = TimeSpeed.wait;
                }
                if (nowTime.Equals("00:00"))
                {
                    day++;
                }
                hourText.text = hours;
                minsText.text = minutes;
                //  HaTime(hour, minute);
                callback?.Invoke(nowTime);
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
