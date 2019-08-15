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
    public float speedMultiplier = 1;
    [Range(0, 2)]
    public float powerMultiplier = 1;
    [Tooltip("扎营时间")]
    public string holdTime = "04:00";
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
   // public Buff[] buffs;
   [Tooltip("最小获取物品数")]
    public int minGet;
    [Tooltip("最大获取物品数")]
    public int maxGet;

    /// <summary>
    /// 可以不进入采集到的
    /// </summary>
    public ItemInPoint[] Items;
    /// <summary>
    /// 可以不进入遇到的
    /// </summary>
    public People[] people;
    /// <summary>
    /// true places是可能生成的地点，false places等同于地点地图
    /// </summary>
    [Tooltip("是否随机生成地点")]
    public bool isRandom;
    [Tooltip("最小生成地点数")]
    public int minPlace;
    [Tooltip("最大生成地点数")]
    public int maxPlace;
    [HideInInspector]
    public int totalWeight;
    /// <summary>
    /// 可能生成的地点，随机最少生成一个
    /// </summary>
    public Place[] places;



    [Tooltip("在行动中的事件")]
    public EventNote[] eventNotes;
    [System.Serializable]
    public struct EventNote
    {
        public EventEmitter.TakeAction e;
        [HideInInspector]
        public string t;

    }

    [Tooltip("在营地中的事件")]
    public EventEmitter.HoldEvent[] HoldNotes= { EventEmitter.HoldEvent.cook, EventEmitter.HoldEvent.wait, EventEmitter.HoldEvent.sleep };

 
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
