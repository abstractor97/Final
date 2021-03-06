﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "自定义地点", menuName = "自定义生成系统/地点")]
public class Place : ScriptableObject
{
    [Range(0, 100)]
    public int intact;
    [Tooltip("说明")]
    public string explain;
    [Tooltip("")]
    public Sprite lowSprite;
    [Tooltip("可能生成的状态")]
    public State[] states;

    [Tooltip("默认状态")]
    public State state;
    [Tooltip("偏好位置,值越低生成概率越低")]
    [Range(1,9)]
    public int position=1;

    [Tooltip("出入口")]
    public bool door;
    
    public List<ExploreAction> firstActions;


    public ExploreAction[] exploreActions;

    [Serializable]
    public class ExploreAction
    {
        public EventEmitter.ExploreEvent type;
        [Tooltip("yarn文件")]
        public TextAsset note;
        [Tooltip("开始标签")]
        public string talkToNode;

        [Range(0, 1)]
        [Tooltip("触发概率")]
        public float probability;

        public bool isTrigger;

        public People people;

        public Place place;

        //  public 

        //  public UnityEvent et;
    }
    [Serializable]
    public enum State
    {
        Unreconnoitre,
        safe,
        risky,
        hostile,
    }

}
