using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale=new Vector3(0.3f,0.3f,1);
        transform.DOScale(1, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextWeek()
    {

    }

    public void AgainShow()
    {

    }

    public void ClearData()
    {

    }

    public class WeekNotice
    {
        public int weekNumber;

        #region 收入
        public int iCoins;

        public int iFood;

        public int iMineral;

        public int iCrystal;
        #endregion

        #region 支出
        public int mCoins;

        public int mFood;

        public int mMineral;

        public int mCrystal;
        #endregion

        public List<NoticeEvent> noticeEvents;

    }

    public class NoticeEvent
    {
        public string title;
        public string note;
    }
}
