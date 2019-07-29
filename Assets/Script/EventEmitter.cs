using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventEmitter : MonoBehaviour
{
    public Points points;

    /// <summary>
    /// 速度乘数
    /// </summary>
    [Range(0, 10)]
    public float speedMultiplier;
    [Range(0, 10)]
    public float powerMultiplier;
    [Tooltip("扎营时间")]
    public string holdTime;
   // public Event[] events;

    public EventNote[] eventNotes;
    [System.Serializable]
    public struct EventNote
    {
        public Event e;
        [HideInInspector]
        public string t;
        [Range(0,1)]
        [Tooltip("触发概率")]
        public float probability;
    }

    private void Start()
    {
        gameObject.AddComponent<PointsControl>();
        for (int i = 0; i < eventNotes.Length; i++)
        {
            eventNotes[i].t = EventToString(eventNotes[i].e);
        }
    }

    public void ShowAction()
    {
        FindObjectOfType<PublicManager>().ShowActionFrame(eventNotes, OnAction);
    }

    public abstract void OnAction(int i);

    public virtual void OnStronghold() {

    }

    public virtual void OnArrive(){
        FindObjectOfType<MapPlayer>().state.moveSpeed *= speedMultiplier;
        FindObjectOfType<MapPlayer>().state.powerLoop *= powerMultiplier;
    }

    public virtual void OnLeave()
    {
        FindObjectOfType<MapPlayer>().state.moveSpeed /= speedMultiplier;
        FindObjectOfType<MapPlayer>().state.powerLoop /= powerMultiplier;
    }

    private string EventToString(Event e)
    {
        string t="";
        switch (e)
        {
            case Event.tocamp:
                t="扎营";
                break;
            case Event.explore:
                t = "探索";
                break;
            case Event.collection:
                t = "采集";
                break;
            case Event.medical:
                t = "医疗";
                break;
            case Event.rest:
                t = "休息";
                break;
            case Event.make:
                t = "制作";
                break;
            case Event.transaction:
                t = "交易";
                break;
            case Event.beg:
                t = "乞讨";
                break;
            default:
                break;
        }
        return t;
    }

    public enum Event {
        tocamp,
        explore,
        collection,
        medical,
        rest,
        make,
        transaction,
        beg,
    }

}

