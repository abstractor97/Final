using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "自定义兴趣点", menuName = "自定义生成系统/兴趣点")]
public class Points : ScriptableObject
{
   // public string name;
    public string describe;
    public Sprite lowSprite;

    /// <summary>
    /// 速度乘数
    /// </summary>
    [Range(0, 2)]
    public float speedMultiplier=1;
    [Range(0, 2)]
    public float powerMultiplier=1;
    [Tooltip("扎营时间")]
    public string holdTime="04:00";
    [Tooltip("扎营类型")]
    public Stronghold.StrongholdControl.Type holdType;

    public bool isHold;
    [Tooltip("是否拦截通过者")]
    public bool intercept;

    // public PointsControl control;
    //public Scripts
    //public EventEmitter eventSend;
    /// <summary>
    /// 可以获得的
    /// </summary>
    public Buff[] buffs;

    public int minGet;
    public int maxGet;

    /// <summary>
    /// 可以采集到的
    /// </summary>
    public ItemInPoint[] Items;
    /// <summary>
    /// 可以遇到的
    /// </summary>
    public People[] people;



    [Tooltip("在行动中的事件")]
    public EventNote[] eventNotes;
    [System.Serializable]
    public struct EventNote
    {
        public EventEmitter.Event e;
        [HideInInspector]
        public string t;

    }

    [Tooltip("在营地中的事件")]
    public EventEmitter.HoldEvent[] HoldNotes= { EventEmitter.HoldEvent.cook };

 
    [System.Serializable]
    public class ItemInPoint
    {

        [Range(0, 1)]
        [Tooltip("获得概率")]
        public float probability;
        [Tooltip("可以获取的最大数量")]
        public int max;
        public Item item;

    }

}
