﻿using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public abstract class EventEmitter : MonoBehaviour
{
    public Points points;

   // private float 

    // public Event[] events;

    public List<ExploreAction>  firstActions;


    public ExploreAction[] exploreActions;

    [System.Serializable]
    public struct ExploreAction
    {
        public ExploreEvent type;
        [Tooltip("yarn文件")]
        public TextAsset note;

        [Range(0, 1)]
        [Tooltip("触发概率")]
        public float probability;

        public bool isTrigger;

        public UnityEvent et;
    }

    private void Start()
    {
        gameObject.AddComponent<PointsControl>();
        for (int i = 0; i < points. eventNotes.Length; i++)
        {
            points.eventNotes[i].t = EventToString(points.eventNotes[i].e);
        }
        
    }

    public void ShowAction()
    {
        FindObjectOfType<PublicManager>().ShowActionFrame(points.eventNotes, OnAction);
    }

    public abstract void OnAction(int i);

    public virtual void OnStronghold() {

    }

    public void Explore()
    {
        if (firstActions!=null)
        {
            foreach (var e in firstActions)
            {
                if (!e.isTrigger)
                {
                    FindObjectOfType<DialogueRunner>().AddScript(e.note);
                    FindObjectOfType<DialogueRunner>().StartDialogue();
                    e.et.Invoke();
                    return;
                }
            }
        }
       
        float r = Random.Range(0,1);
        float l = 0;
        foreach (var e in exploreActions)
        {
            l += e.probability;
            if (l>=r)
            {
                FindObjectOfType<DialogueRunner>().AddScript(e.note);
                FindObjectOfType<DialogueRunner>().StartDialogue();
                e.et.Invoke();
            }
        }
    }

    public void Collection()
    {
        int n = Random.Range(points.minGet, points.maxGet);

        for (int i = 0; i < n; i++)
        {
            float r = Random.Range(0, 1);
            float l = 0;
            foreach (var e in points.Items)
            {
                l += e.probability;
                if (l >= r && e.max != 0)
                {
                    //todo showitem | additem
                }
            }
        }
       
    }

    public void AddFirstEvent(ExploreAction action) {
        if (firstActions == null)
        {
            firstActions = new List<ExploreAction>();
        }
        firstActions.Add(action);
    }

    public virtual void OnArrive(){
        FindObjectOfType<PlayerManager>().state.moveSpeed *= points. speedMultiplier;
        FindObjectOfType<PlayerManager>().state.powerLoop *= points.powerMultiplier;
    }

    public virtual void OnLeave()
    {
        FindObjectOfType<PlayerManager>().state.moveSpeed /= points.speedMultiplier;
        FindObjectOfType<PlayerManager>().state.powerLoop /= points.powerMultiplier;
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

    public enum ExploreEvent
    {
        NPC,
        place,
        e,
    }

}

