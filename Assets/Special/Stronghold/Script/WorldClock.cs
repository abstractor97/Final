using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldClock : MonoBehaviour
{
    public WorldTime worldTime;

    public DigitText weekText;
    public DigitText dayText;
    public DigitText hourText;
    public DigitText minText;

    public UnityAction clock;
    // Start is called before the first frame update
    void Start()
    {
        worldTime.weekCall += WeekCallBack;
        worldTime.dayInWeekCall += DayCallBack;
        worldTime.timeInDayCall += HourAndMinCallBack;
        weekText.text = worldTime.allTime.week.ToString();
        dayText.text = worldTime.allTime.day.ToString();
        hourText.text = worldTime.allTime.hour.ToString();
        minText.text = worldTime.allTime.minute.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void WeekCallBack(int week)
    {
        weekText.text = week.ToString();
        worldTime.stop = true;
        //todo 显示周总结
    }
    private void DayCallBack(int day)
    {
        dayText.text = day.ToString();
        GameTeamController.GameData.CreateRecruitOnLv();
    }
    private void HourAndMinCallBack(int hour,int min)
    {
        hourText.text = hour.ToString();
        minText.text = min.ToString();
        clock?.Invoke();
        //todo 修改光线亮度
    }

}
