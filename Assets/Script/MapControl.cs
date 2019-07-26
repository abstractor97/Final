using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Stronghold;

namespace Map
{
    public class MapControl : MonoBehaviour
    {
        public EventEmitter eventEmitter;

        public GameObject load;

        public GameObject task;

        public GameObject bag;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Stronghold()
        {
            if (eventEmitter != null&&eventEmitter.points.holdType!=StrongholdControl.Type.none)
            {
                eventEmitter.OnStronghold();
                CanvasGroup group = load.GetComponent<CanvasGroup>();
                group.alpha = 1;
                group.interactable = true;
                group.blocksRaycasts = true;
                SceneManager.LoadSceneAsync("StrongholdScene", LoadSceneMode.Additive);
            }
                      
        }

        public void Action()
        {
            if (eventEmitter != null)
            {
                eventEmitter.ShowAction();
            }
        }

        public void Bag()
        {
            FindObjectOfType<PublicManager>().Show(bag);
        }

        public void Task()
        {
            FindObjectOfType<PublicManager>().Show(task);
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

