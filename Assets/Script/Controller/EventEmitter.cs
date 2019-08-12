using Map;
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

   

    private void Start()
    {
        gameObject.AddComponent<PointsControl>().eventEmitter=this;
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

    public virtual void OnStronghold(int i)
    {

    }
    /// <summary>
    /// 拦截回调
    /// </summary>
    public virtual void OnIntercept()
    {

    }

    public void SendExplore(Place.ExploreAction action)
    {
        switch (action.type)
        {
            case ExploreEvent.NPC:
                  FindObjectOfType<DialogueRunner>().AddScript(action.note);
                  FindObjectOfType<DialogueRunner>().StartDialogue(action.talkToNode);
                break;
            case ExploreEvent.place:
                
                break;
            case ExploreEvent.e:
                break;
        }
    }

    public void Explore(Place place,Dictionary<ExploreEvent,float> perturbation=null)
    {
        if (place.firstActions !=null)
        {
            for (int i = 0; i < place.firstActions.Count; i++)
            {
                if (!place.firstActions[i].isTrigger)
                {
                    SendExplore(place.firstActions[i]);
                    place.firstActions[i].isTrigger=true;
                    return;
                }
            }
           
        }
       
        float r = Random.Range(0,1);
        float l = 0;
        foreach (var e in place.exploreActions)
        {
            if (perturbation!=null)
            {
                if (perturbation.ContainsKey(e.type))
                {
                    l += (e.probability+perturbation[e.type]);
                }
            }
            else
            {
                l += e.probability;
            }
            if (l>=r)
            {
                SendExplore(e);
                return;
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

    

    public virtual void OnArrive(){
        FindObjectOfType<PlayerManager>().state.moveSpeed *= points. speedMultiplier;
        FindObjectOfType<PlayerManager>().state.powerLoop *= points.powerMultiplier;
    }

    public virtual bool OnLeave()
    {
        FindObjectOfType<PlayerManager>().state.moveSpeed /= points.speedMultiplier;
        FindObjectOfType<PlayerManager>().state.powerLoop /= points.powerMultiplier;
        return true;
    }

    public void SaveThis()
    {
       // points
    }

    private string EventToString(TakeAction e)
    {
        string t="";
        switch (e)
        {
            case TakeAction.tocamp:
                t="扎营";
                break;
            case TakeAction.explore:
                t = "探索";
                break;
            case TakeAction.collection:
                t = "采集";
                break;
            case TakeAction.medical:
                t = "医疗";
                break;
            case TakeAction.rest:
                t = "休息";
                break;
            case TakeAction.make:
                t = "制作";
                break;
            case TakeAction.transaction:
                t = "交易";
                break;
            case TakeAction.beg:
                t = "乞讨";
                break;
            default:
                break;
        }
        return t;
    }

    public enum HoldEvent
    {
        cook,
        readiness,
        sleep,
        dismantle
    }

    public enum TakeAction {
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

