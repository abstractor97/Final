using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [Obsolete]
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
                PublicManager.ShowTips("该地点没有营地");
            }
                      
        }
        [Obsolete]
        private void ActionFrame(GameObject ui, EventEmitter.HoldEvent item)
        {
            Text text= ui.GetComponent<Text>();
            switch (item)
            {
                case EventEmitter.HoldEvent.cook:
                    text.text =ProcessManager.language.Text("烹饪") ;
                    break;
                case EventEmitter.HoldEvent.wait:
                    text.text = ProcessManager.language.Text("等待");
                    break;
                case EventEmitter.HoldEvent.sleep:
                    text.text = ProcessManager.language.Text("睡眠");
                    break;
                
            }
        }

        public void Action()
        {
            if (FindObjectOfType<PublicManager>().lockWalk)
            {
                return;
            }
            if (eventEmitter != null)
            {
                task.GetComponent<GridView>().AddDataDef(eventEmitter.eventNotes.ToArray(), ActionFrame, eventEmitter.OnAction);
            }
        }

        private void ActionFrame(GameObject ui, EventEmitter.TakeAction item)
        {
            ui.GetComponent<Text>().text = eventEmitter.EventToString(item);
        }

        public void Bag()
        {
           PublicManager.Show(bag);
        }

        public void HideBag()
        {
            PublicManager.Hide(bag);
        }

        public void GetInto()
        {
            if (FindObjectOfType<PublicManager>().lockWalk)
            {
                return;
            }
            PlaceFrame pf = placeFrame.GetComponent<PlaceFrame>();
        
            if (eventEmitter.points.isRandom)
            {
                if (eventEmitter.points.places.Length!=eventEmitter.points.maxPlace)
                {
                    Place[] places = new Place[eventEmitter.points.maxPlace];
                    pf.totalDistance = eventEmitter.points.maxPlace;
                    for (int i = 0; i < eventEmitter.points.maxPlace; i++)
                    {
                        int door = UnityEngine.Random.Range(0, eventEmitter.points.maxPlace);
                        places[door] = Resources.Load<Place>("Assets/Places/出入口");
                        int index=0;

                        if (places[i]==null)
                        {
                            int random = UnityEngine.Random.Range(1, eventEmitter.points.totalWeight);
                            foreach (var place in eventEmitter.points.places)
                            {
                                if (random > place.position)
                                {
                                    random -= place.position;
                                    index++;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            places[i] = eventEmitter.points.places[index];
                        }
                       
                    }

                    pf.position = eventEmitter.position;
                    eventEmitter.points.places = places;
                }
                       
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
              
            }
            placeFrame.GetComponent<PlaceFrame>().Show(eventEmitter.points.places);

        }

     
    }

}

