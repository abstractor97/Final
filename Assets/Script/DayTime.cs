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
    public float scale=1f;
    public TimeSpeed timeSpeed=TimeSpeed.wait;

    public string targetTime;
    /// <summary>
    /// 经过的天数
    /// </summary>
    public int totalDay;
    /// <summary>
    /// 经过的周数
    /// </summary>
    public int totalWeek;
    public bool isRun;
    public bool isStop;

    public Text hourText;
    public Text minsText;
    private Sequence mScoreSequence;
    private int mOldScore;

    private void Start()
    {
        StartDay();
        mScoreSequence = DOTween.Sequence();
        mScoreSequence.SetAutoKill(false);
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

    public void QuickTime(string time)
    {
        //todo 时间流逝时动画效果
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

    public void JumpToTime(int mweek, int mday, int mhour, int mins)
    {
        mhour += (mins + minute) / 60;
        mday += (mhour + hour) / 24;
        mweek += (mday + totalDay) / 7;
        minute = (mins + minute) % 60;
        hour = (mhour + hour) % 24;
        totalDay = (mday + totalDay) % 7;
        totalWeek += mweek;
        isStop = true;
        AniNumber(hourText, hour);
        AniNumber(minsText, minute);
    }


    IEnumerator Run()
    {
        while (true)
        {
            if (isStop)
            {
                yield return new WaitForSeconds(scale);
                continue;
            }
            switch (timeSpeed)
            {
                case TimeSpeed.wait:
                    scale = 1f;
                    break;
                case TimeSpeed.walk:
                    scale = 0.4f;
                    break;
                case TimeSpeed.jump:
                    scale = 0.1f;
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
                targetTime = "";
            }
            if (nowTime.Equals("00:00"))
            {
                totalDay++;
                if (totalDay == 7)
                {
                    totalWeek++;
                    totalDay = 0;
                }
            }
            hourText.text = hours;
            minsText.text = minutes;
            //  HaTime(hour, minute);
            callback?.Invoke(nowTime);
        }

    }

    private void AniNumber(Text text,int tar)
    {
        mScoreSequence.Append(DOTween.To(delegate (float value) {
            //向下取整
            var temp = Math.Floor(value);
            var mtext = temp.ToString();
            text.text = mtext;
        }, mOldScore, tar, 0.4f));
        //将更新后的值记录下来, 用于下一次滚动动画
        mOldScore = tar;
    }

    public enum TimeSpeed
    {
        wait,
        walk,
        jump,
    }
}
