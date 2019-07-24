﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class PointsControl : MonoBehaviour
    {
        public Points points;
        // Start is called before the first frame update
        void Start()
        {

            points.eventSend.points = points;
            gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            FindObjectOfType<MapControl>().pointsControl = null;
            points.eventSend.OnLeave();
        }

        private void OnMouseEnter()
        {
            GameObject.FindObjectOfType<PublicManager>().ShowPointsUIThis(transform.position);
        }

        private void OnMouseDown()
        {
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mousePosition = tileGrid.GetCellCenterWorld(tileGrid.WorldToCell(mousePosition));
            // mousePosition.z = 0;
            if (!GameObject.FindObjectOfType<PublicManager>().lockWalk)
            {
                GameObject.FindObjectOfType<PublicManager>().ShowArlog(GenerateChar(), Go);


            }

        }

        public void Explore()
        {
            points.eventSend.OnSend();
        }

        public void Wait()
        {
            points.eventSend.OnWait();
        }

        public void ToCamp()
        {
            points.eventSend.OnCamp();
        }

        public void Arrive()
        {
            points.eventSend.OnArrive();
            FindObjectOfType<MapControl>().pointsControl = this;
            FindObjectOfType<MapPlayer>().Stop();
            FindObjectOfType<PublicManager>().lockWalk = false;
        }

        public string GenerateChar()
        {
            return "前往" + gameObject.name + ",预计用时" + TimeConsum() + "小时";
        }

        public void Go(Ardialog.Pass pass)
        {
            if (pass == Ardialog.Pass.yes)
            {
                FindObjectOfType<PublicManager>().lockWalk = true;
                FindObjectOfType<MapPlayer>().ai.Goto(transform.position);
                FindObjectOfType<MapPlayer>().ai.arriveCallBack += Arrive;
            }
        }

        public virtual int TimeConsum()
        {
            return 0;
        }


    }
}
