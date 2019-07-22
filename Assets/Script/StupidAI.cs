using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

namespace AI
{
    /// <summary>
    /// 这是一个不会躲避障碍物的AI
    /// </summary>
    public class StupidAI : MonoBehaviour
    {
      //  GameObject[] playerTargets = null;
        
        public float speed = 1.0f;
        public delegate void Arrive();
        public event Arrive arriveCallBack;
        public event Arrive runCallBack;
        private Vector3 target;
      

        void Start()
        {
            target = Vector3.zero;
        //    playerTargets = GameObject.FindGameObjectsWithTag("Player");
        }

        void Update()
        {
            if (target!= Vector3.zero)
            {

                EngageTarget(target);
                runCallBack();
                if (target.Equals(transform.position))
                {
                    arriveCallBack();
                    target = Vector3.zero;
                }
            }
          
        }

        public void Goto(Vector3 post )
        {
            target = post;
          //  EngageTarget(post);
        }

        public void Stop() {
            target = Vector3.zero;
        }

        private void EngageTarget(Vector3 post)
        {
            Vector3 targetDir = post - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
}

