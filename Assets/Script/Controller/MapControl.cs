using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Stronghold;
using UnityEngine.UI;
using System;

namespace Map
{
    public class MapControl : MonoBehaviour
    {
      //  [HideInInspector]
        public EventEmitter eventEmitter;

        public GameObject load;

        public GameObject task;

        public GameObject bag;

        public GameObject placeFrame;
        // Start is called before the first frame update
        void Start()
        {
            if (eventEmitter!=null&& GameObject.FindGameObjectWithTag("Player").transform.position.Equals(eventEmitter.transform.position))
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = eventEmitter.transform.position;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Stronghold()
        {
            if (eventEmitter != null&&eventEmitter.points.isHold)
            {
                task.GetComponent<GridView>().AddDataDef(eventEmitter.points.HoldNotes, ActionFrame, eventEmitter.OnStronghold);
                //eventEmitter.OnStronghold();
                //CanvasGroup group = load.GetComponent<CanvasGroup>();
                //group.alpha = 1;
                //group.interactable = true;
                //group.blocksRaycasts = true;
                //SceneManager.LoadSceneAsync("StrongholdScene", LoadSceneMode.Additive);
            }
            else
            {
                FindObjectOfType<PublicManager>().ShowTips("该地点没有营地");
            }
                      
        }

        private void ActionFrame(GameObject ui, EventEmitter.HoldEvent item)
        {
            Text text= ui.GetComponent<Text>();
            switch (item)
            {
                case EventEmitter.HoldEvent.cook:
                    text.text =ProcessManager.Instance.language.Text("烹饪") ;
                    break;
                case EventEmitter.HoldEvent.wait:
                    text.text = ProcessManager.Instance.language.Text("等待");
                    break;
                case EventEmitter.HoldEvent.sleep:
                    text.text = ProcessManager.Instance.language.Text("睡眠");
                    break;
                
            }
        }

        public void Action()
        {
            if (eventEmitter != null)
            {
                //  eventEmitter.ShowAction();
                task.GetComponent<GridView>().AddDataDef(eventEmitter.points.eventNotes, ActionFrame, eventEmitter.OnAction);
            }
        }

        private void ActionFrame(GameObject ui, Points.EventNote item)
        {
            ui.GetComponent<Text>().text = item.t;
        }

        public void Bag()
        {
            FindObjectOfType<PublicManager>().Show(bag);
        }

        public void GetInto()
        {
            PlaceFrame pf = placeFrame.GetComponent<PlaceFrame>();
        
            if (eventEmitter.points.isRandom)
            {
                Place[] places = new Place[eventEmitter.points.maxPlace];
                pf.totalDistance = eventEmitter.points.maxPlace;

                //todo 生成随机地块算法 
                pf.position = eventEmitter.position;
                placeFrame.GetComponent<PlaceFrame>().Show(places);
            }
            else
            {
                for (int i = 0; i < eventEmitter.points.places.Length; i++)
                {
                    if (eventEmitter.points.places[i].door)
                    {
                        pf.position = i;
                        eventEmitter.position = i;
                    }
                }
                pf.totalDistance = eventEmitter.points.places.Length;
                placeFrame.GetComponent<PlaceFrame>().Show(eventEmitter.points.places);
            }
            


          //  FindObjectOfType<PublicManager>().Show(bag);
        }

        //public void Wait()
        //{
           
        //    FindObjectOfType<PublicManager>().ShowTimeDialog(JumpTime);
        

        //}

        //public void JumpTime(string time)
        //{
        //    MapPlayer player = FindObjectOfType<MapPlayer>();
        //    player.dayTime.JumpTime(time);
        //    if (pointsControl != null)
        //    {
        //        pointsControl.Wait();
        //    }
        //}
    }

}

