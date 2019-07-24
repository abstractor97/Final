using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChoiceDialog : MonoBehaviour
{/// <summary>
/// 一次修改的时间
/// </summary>
    public int addTime = 30;

    public GameObject time;

    private Text hour;
    private Text min;
    public delegate void TimeCallback(string time);

    public event TimeCallback callback;

    // Start is called before the first frame update
    void Start()
    {
        hour=time.transform.Find("Hour").GetComponent<Text>();
        min=time.transform.Find("Min").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTime() {
        int imin = int.Parse(min.text) + addTime;
        int ihour = int.Parse(hour.text);

        if (imin>59)
        {
            ihour += imin / 60;
            imin = imin % 60;
        }
        if (ihour>23)
        {
            return;
        }
        if (ihour < 10)
        {
            hour.text ="0"+ ihour.ToString();
        }
        else 
        {
            hour.text = ihour.ToString();
        }

        if (imin<10)
        {
            min.text = "0" + imin.ToString();
        }
        else
        {
            min.text =  imin.ToString();
        }

    }

    public void ReduceTime() {
        int imin = int.Parse(min.text) ;
        int ihour = int.Parse(hour.text);
        if (imin<addTime)
        {
            ihour -= (addTime - imin) / 60;
            imin = 60 - addTime % 60;
        }

        if (ihour < 0)
        {
            ihour = 0;
        }
        hour.text = ihour.ToString();
        min.text = imin.ToString();

    }

    public void Enter() {
        callback(hour.text+":"+min.text);
    }

    public void Close() {
        CanvasGroup group = gameObject.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
