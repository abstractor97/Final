using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorldTime:MonoBehaviour
{
    [Tooltip("起始时间")]
    public AllTime allTime;
    [HideInInspector]
    [Tooltip("时间流逝速度 1s=1m")]
    public int speed = 1;
    [Tooltip("时间伸缩率")]
    public float scale=1f;

    private string targetTime;
    [HideInInspector]
    public bool isRun;

    public UnityAction<int> weekCall;
    public UnityAction<int> dayInWeekCall;
    public UnityAction<int, int> timeInDayCall;

    private Sequence mScoreSequence;
    private int mOldScore;

    private void Start()
    {
        StartDay();
        mScoreSequence = DOTween.Sequence();
        mScoreSequence.SetAutoKill(false);
        DontDestroyOnLoad(gameObject);
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
    }
    [Obsolete("只能加速48小时以下的时间，无效")]
    public void QuickTime(string time)
    {
        //todo 时间流逝时动画效果
        string[] ts = time.Split(':');

        int thour = allTime.hour + int.Parse(ts[0]);
      
        int tmin= allTime. minute + int.Parse(ts[1]);
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
        ChangeSpeed(TimeSpeed.jump);
    }

    public void JumpToTime(int mweek, int mday, int mhour, int mins)
    {
        mhour += (mins + allTime. minute) / 60;
        mday += (mhour + allTime. hour) / 24;
        mweek += (mday + allTime.day) / 7;
        allTime.minute = (mins + allTime. minute) % 60;
        allTime.hour = (mhour + allTime.hour) % 24;
        allTime.day = (mday + allTime.day) % 7;
        allTime.week += mweek;
       // isStop = true;
    //    AniNumber(hourText, hour);
    //    AniNumber(minsText, minute);
    }

    public string GetTime()
    {
        string hours = allTime.hour.ToString();
        if (allTime.hour < 10)
        {
            hours = "0" + hours;
        }
        string minutes = allTime.minute.ToString();
        if (allTime.minute < 10)
        {
            minutes = "0" + minutes;
        }
        string nowTime = hours + ":" + minutes;
        return nowTime;
    }

    IEnumerator Run()
    {
        while (true)
        {         
            yield return new WaitForSeconds(scale);
            if (allTime.minute +speed < 60)
            {
                allTime.minute += speed;
            }
            else
            {
                allTime.hour++;
                allTime.minute = allTime.minute +speed-60;
                if (allTime.hour == 24)
                {
                    allTime.hour = 0;
                }
            }
           
            if (allTime.hour ==0&& allTime.minute ==0)
            {
                allTime.day++;
                dayInWeekCall?.Invoke(allTime.day);
                if (allTime.day == 7)
                {
                    allTime.week++;
                    allTime.day = 0;
                    weekCall?.Invoke(allTime.week);
                }
            }
            timeInDayCall?.Invoke(allTime.hour, allTime.minute);
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
    [Serializable]
    public class AllTime
    {
        /// <summary>
        /// 经过的周数
        /// </summary>
        public int week;
        /// <summary>
        /// 经过的天数
        /// </summary>
        public int day;
        public int hour = 0;
        public int minute = 0;
    }
}
