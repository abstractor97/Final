using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeMenu : MonoBehaviour
{

    public Text weekNumbert;

    #region 收入
    public DigitText iCoinst;

    public DigitText iFoodt;

    public DigitText iMineralt;

    public DigitText iCrystalt;
    #endregion

    #region 支出
    public DigitText mCoinst;

    public DigitText mFoodt;

    public DigitText mMineralt;

    public DigitText mCrystalt;
    #endregion

    public RectTransform WeekEventLayout;

    public GameObject eventItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Show(WeekNotice weekNotice)
    {
        ClearData();
        weekNumbert.text = weekNumbert.text.Replace("d", weekNotice.weekNumber.ToString());
        iCoinst.text = weekNotice.iCoins.ToString();
        iFoodt.text = weekNotice.iFood.ToString();
        iMineralt.text = weekNotice.iMineral.ToString();
        iCrystalt.text = weekNotice.iCrystal.ToString();
        mCoinst.text = weekNotice.mCoins.ToString();
        mFoodt.text = weekNotice.mFood.ToString();
        mMineralt.text = weekNotice.mMineral.ToString();
        mCrystalt.text = weekNotice.mCrystal.ToString();
        foreach (var eve in weekNotice.noticeEvents)
        {
            GameObject.Instantiate<GameObject>(eventItem).transform.SetParent(WeekEventLayout,false); 
        }
        AgainShow();
    }

    public void AgainShow() 
    {
        GetComponent<CanvasGroup>().alpha = 1;
        transform.localScale = new Vector3(0.3f, 0.3f, 1);
        transform.DOScale(1, 0.6f);
    }

    public void ClearData()
    {
        foreach (RectTransform item in WeekEventLayout)
        {
            Destroy(item.gameObject);
        }
    }

    public void Enter()
    {
        GetComponent<CanvasGroup>().alpha = 0;
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
        public string birthplace;
        public bool isLuck;
    }
}
