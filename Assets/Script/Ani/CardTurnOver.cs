﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

//卡牌状态，正面、背面
public enum CardState
{
    Front,
    Back
}
public class CardTurnOver : MonoBehaviour
{
    public GameObject mFront;//卡牌正面
    public GameObject mBack;//卡牌背面
    public CardState mCardState = CardState.Front;//卡牌当前的状态，是正面还是背面？
    public bool selfRatate = true;//是否允许点击自身旋转
    public float mTime = 0.3f;
    public UnityAction<CardState> overCallBack;
    private bool isActive = false;//true代表正在执行翻转，不许被打断


    /// <summary>
    /// 初始化卡牌角度，根据mCardState
    /// </summary>
    public void Init()
    {
        if (mCardState == CardState.Front)
        {
            //如果是从正面开始，则将背面旋转90度，这样就看不见背面了
            mFront.transform.eulerAngles = Vector3.zero;
            mBack.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            //从背面开始，同理
            mFront.transform.eulerAngles = new Vector3(0, 90, 0);
            mBack.transform.eulerAngles = Vector3.zero;
        }

        if (selfRatate)
        {
            gameObject.AddComponent<Button>().onClick.AddListener(delegate () {
                if (mCardState==CardState.Front)
                {
                    StartBack();
                }
                else
                {
                    StartFront();
                }
            });
        }
    }
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 留给外界调用的接口
    /// </summary>
    public void StartBack()
    {
        if (isActive)
            return;
        StartCoroutine(ToBack());
    }
    /// <summary>
    /// 留给外界调用的接口
    /// </summary>
    public void StartFront()
    {
        if (isActive)
            return;
        StartCoroutine(ToFront());
    }

    private void TurnOverCallBack(CardState cardState)
    {
        isActive = false;
        mCardState = cardState;
        overCallBack?.Invoke(mCardState);
    }

    /// <summary>
    /// 翻转到背面
    /// </summary>
	IEnumerator ToBack()
    {
        isActive = true;
        mFront.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        mBack.transform.DORotate(new Vector3(0, 0, 0), mTime);
        TurnOverCallBack(CardState.Back);

    }
    /// <summary>
    /// 翻转到正面
    /// </summary>
    IEnumerator ToFront()
    {
        isActive = true;
        mBack.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        mFront.transform.DORotate(new Vector3(0, 0, 0), mTime);
        TurnOverCallBack(CardState.Front);


    }
}

