using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class EventEmitter : MonoBehaviour
{
    public Points points;

    /// <summary>
    /// 入口位置或缓存的当前位置，人物在该坐标初始化
    /// </summary>
    public int position;

    public int level;

    public List<TakeAction> eventNotes=new List<TakeAction>();

    private void Start()
    {
        eventNotes.Add(TakeAction.explore);
        eventNotes.Add(TakeAction.collection);
        eventNotes.Add(TakeAction.make);
        eventNotes.Add(TakeAction.medical);
        eventNotes.Add(TakeAction.transaction);
        gameObject.AddComponent<PointsControl>().eventEmitter=this;
        for (int i = 0; i < points.eventNots.Length; i++)
        {
            if (eventNotes.Contains(points.eventNots[i]))
            {
                eventNotes.Remove(points.eventNots[i]);
            }
        }
        
    }

    public virtual void OnAction(int i) {
        switch (eventNotes[i])
        {
            case TakeAction.explore:
                //  Explore();
                break;
            case TakeAction.collection:
                Collection();
                break;
            case TakeAction.tocamp:
                FindObjectOfType<DayTime>().JumpTime(points.holdTime);
                break;
            case TakeAction.medical:
                break;
        }

    }

    public virtual void OnStronghold(int i)
    {

    }
    /// <summary>
    /// 拦截回调
    /// </summary>
    public virtual void OnIntercept()
    {

    }
    /// <summary>
    /// 周边探索
    /// </summary>
    public void Explore() {

    }


    public void SendMoveEvent(Place.ExploreAction action)
    {//todo 事件触发
        switch (action.type)
        {
            case ExploreEvent.NPC:
                //todo 打开fight页

                
                break;
            case ExploreEvent.place:
                
                break;
            case ExploreEvent.e:
                FindObjectOfType<DialogueRunner>().AddScript(action.note);
                FindObjectOfType<DialogueRunner>().StartDialogue(action.talkToNode);

                break;
        }
    }

    public void MoveEvent(Place place,Dictionary<ExploreEvent,float> perturbation=null)
    {
        if (place.firstActions !=null)
        {
            for (int i = 0; i < place.firstActions.Count; i++)
            {
                if (!place.firstActions[i].isTrigger)
                {
                    SendMoveEvent(place.firstActions[i]);
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
                SendMoveEvent(e);
                return;
            }
        }

    }

    public void Collection()
    {
        FindObjectOfType<PublicManager>().ShowTimeDialog("设置采集时间",delegate (string time) {

            string[] ts = time.Split(':');
            int tmin = int.Parse(ts[0]) * 60+ int.Parse(ts[1]);
            int ext = tmin / 30;

            FindObjectOfType<DayTime>().JumpTime(time);
            GameObject temporaryBag = Resources.Load<GameObject>("UI/Bag");
            temporaryBag = GameObject.Instantiate<GameObject>(temporaryBag);
            FindObjectOfType<PublicManager>().AdditionalFrame(temporaryBag).transform.SetParent(GameObject.FindGameObjectWithTag("HUD").transform, false);

            int n = Random.Range(points.minGet+ ext/2, points.maxGet+ ext);

            for (int i = 0; i < n; i++)
            {
                float r = Random.Range(0, 1);
                float l = 0;
                foreach (var e in points.Items)
                {
                    l += e.probability;
                    if (l >= r && e.max != 0)
                    {
                        temporaryBag.GetComponent<Bag>().AddItem(new Bag.ItemInBag() { item = e.item, num = 1 });
                    }
                }
            }
        });
       
       
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

    public void UpLevel(int lv)
    {

    }

    public string EventToString(TakeAction e)
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
        }
        return ProcessManager.Instance.language.Text(t);
    }

    public enum HoldEvent
    {
        cook,
        wait,
        sleep,
        
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

        none
    }

    public enum ExploreEvent
    {
        NPC,
        place,
        e,
    }

}

