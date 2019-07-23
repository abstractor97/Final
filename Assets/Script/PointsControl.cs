using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsControl : MonoBehaviour
{
    public Points points;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<CircleCollider2D>().isTrigger=true;
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
            GameObject.FindObjectOfType<PublicManager>().ShowArlog(GenerateChar(),Go);

        
        }
           
    }

    public  void Explore()
    {
        points.eventSend.OnSend();
    }

    public void Sleep()
    {
        points.eventSend.OnSleep();
    }

    public void ToCamp()
    {
        points.eventSend.OnCamp();
    }

    public  void Arrive() {
        points.eventSend.OnArrive();
        GameObject.FindObjectOfType<MapPlayer>().Stop();
        GameObject.FindObjectOfType<PublicManager>().lockWalk = false;
    }

    public string GenerateChar()
    {
        return "前往"+gameObject.name+",预计用时"+ TimeConsum()+"小时";
    }

    public void Go(Ardialog.Pass pass)
    {
        if (pass==Ardialog.Pass.yes)
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
