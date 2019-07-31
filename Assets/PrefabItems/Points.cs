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
    [Range(0, 10)]
    public float speedMultiplier;
    [Range(0, 10)]
    public float powerMultiplier;
    [Tooltip("扎营时间")]
    public string holdTime;

    // public PointsControl control;
    //public Scripts
    //public EventEmitter eventSend;
    public Buff[] buffs;
    /// <summary>
    /// 可以采集到的
    /// </summary>
    public Item[] Items;
    public People[] people;
    public string[] environments;



    public Stronghold.StrongholdControl.Type holdType;

    public EventNote[] eventNotes;
    [System.Serializable]
    public struct EventNote
    {
        public EventEmitter.Event e;
        [HideInInspector]
        public string t;

    }

}
