using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("文本增强/数位控制文本")]
public class DigitText : MonoBehaviour
{
    [Tooltip("允许显示几位")]
    public int digit = 4;
    [Tooltip("开启滚动动画")]
    public bool openAni;

    private Text uitext;

    public string text { get { return uitext.text; }
        set {
            string mtext= value;

            if (mtext.Length> digit)//数位溢出
            {
                mtext= mtext.Substring(mtext.Length- digit);
                uitext.text = mtext;
            }
            else
            {
                if (openAni)
                {
                    AniNumber(int.Parse(mtext));
                }
                else
                {
                    for (int i = 0; i < digit - mtext.Length; i++)
                    {

                        mtext = "0".ToString() + mtext;
                    }
                    uitext.text = mtext;
                }
            }
          
        } }


    private Sequence mScoreSequence;
    private int mOldScore;

    private void Awake()
    {
        uitext = GetComponent<Text>();
      
    }

    // Start is called before the first frame update
    void Start()
    {
        if (openAni)
        {
            mScoreSequence = DOTween.Sequence();
            mScoreSequence.SetAutoKill(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AniNumber(int tar)
    {
        mScoreSequence.Append(DOTween.To(delegate (float value) {
            //向下取整
            var temp = Math.Floor(value);
            var mtext = temp.ToString();
            for (int i = 0; i < digit - mtext.Length; i++)
            {
                mtext = "0".ToString() + mtext;
            }
            uitext.text = mtext;
        }, mOldScore, tar, 0.4f));
        //将更新后的值记录下来, 用于下一次滚动动画
        mOldScore = tar;
    }

    private void OnDestroy()
    {
        mScoreSequence.SetAutoKill(true);
    }
}
