using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Map
{
    [HideInInspector]
    public class PointsControl : MonoBehaviour
    {

        public EventEmitter eventEmitter;
        private float d;
        // Start is called before the first frame update
        void Start()
        {

           // points.eventSend.points = points;
           // gameObject.AddComponent
            gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (eventEmitter.points.intercept)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<AILerp>().destination = transform.position;
                player.GetComponent<AILerp>().SearchPath();
                player.GetComponent<AILerp>().complete += Arrive;

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }

        private void OnMouseEnter()
        {
           // GameObject.FindObjectOfType<PublicManager>().ShowPointsUIThis(transform.position);
        }

        private void OnMouseDown()
        {
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mousePosition = tileGrid.GetCellCenterWorld(tileGrid.WorldToCell(mousePosition));
            // mousePosition.z = 0;
            if (Input.GetMouseButtonDown(0)&&!GameObject.FindObjectOfType<PublicManager>().lockWalk&& !FindObjectOfType<DialogueRunner>().isDialogueRunning)
            {
                if (FindObjectOfType<MapControl>().eventEmitter.OnLeave())
                {
                    GameObject.FindObjectOfType<PublicManager>().ShowArlog(GenerateChar(), Go);
                }
              
            }

        }

        public void Arrive()
        {
            // points.eventSend.OnArrive();
            EventEmitter e = gameObject.GetComponent<EventEmitter>();
            e.OnArrive();
            FindObjectOfType<MapControl>().eventEmitter = e;
            FindObjectOfType<DayTime>().ChangeSpeed(DayTime.TimeSpeed.wait);
            FindObjectOfType<PublicManager>().lockWalk = false;
          //  ProcessManager.Instance.save.x = gameObject.transform.position.x;
          //  ProcessManager.Instance.save.y = gameObject.transform.position.y;
            if (eventEmitter.points.intercept)
            {
                eventEmitter.OnIntercept();
            }
        }

        public string GenerateChar()
        {
            return "前往:" + gameObject.name ;
        }

        public void Go(Ardialog.Pass pass)
        {
            if (pass == Ardialog.Pass.yes)
            {
                FindObjectOfType<ATarget>().WalkThis(transform.position);
                FindObjectOfType<PublicManager>().lockWalk = true;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<AILerp>().destination= transform.position;
                player.GetComponent<AILerp>().speed = FindObjectOfType<PlayerManager>().GetSpeed();
                player.GetComponent<AILerp>().SearchPath();
                player.GetComponent<AILerp>().complete += Arrive;
                player.GetComponent<AILerp>().pathCallBack += delegate(float l) { d = l; };
                GameObject.FindObjectOfType<DayTime>().ChangeSpeed(DayTime.TimeSpeed.walk);
                // FindObjectOfType<MapPlayer>().ai.Goto(transform.position);
                // FindObjectOfType<MapPlayer>().ai.arriveCallBack += Arrive;
            }
        }

        


    }
}

