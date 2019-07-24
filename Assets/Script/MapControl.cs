using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Map
{
    public class MapControl : MonoBehaviour
    {
        public PointsControl pointsControl;

        public GameObject load;

        public GameObject task;
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
            CanvasGroup group = load.GetComponent<CanvasGroup>();
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
            SceneManager.LoadScene("StrongholdScene");
        }

        public void Explore()
        {
            if (pointsControl!=null)
            {
                pointsControl.Explore();
            }
        }

        public void Camp()
        {
            if (pointsControl != null)
            {
                pointsControl.ToCamp();
            }
        }

        public void Task()
        {
            FindObjectOfType<PublicManager>().Show(task);
        }

        public void Wait()
        {
           
            FindObjectOfType<PublicManager>().ShowTimeDialog(JumpTime);
        

        }

        public void JumpTime(string time)
        {
            MapPlayer player = FindObjectOfType<MapPlayer>();
            player.dayTime.JumpTime(time);
            if (pointsControl != null)
            {
                pointsControl.Wait();
            }
        }
    }

}

