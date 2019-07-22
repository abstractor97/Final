using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public GameObject tips;
    private Grid tileGrid;
    private bool lockWalk;
    // Start is called before the first frame update
    void Start()
    {
        tileGrid = GameObject.FindObjectOfType<Grid>();
        GameObject.FindObjectOfType<MapPlayer>().ai.arriveCallBack += Arrive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (!lockWalk&&Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = tileGrid.GetCellCenterWorld(tileGrid.WorldToCell(mousePosition));
            mousePosition.z = 0;
            GameObject.FindObjectOfType<MapPlayer>().ai.Goto(mousePosition);
            lockWalk = true;
        }
    }

    public void Arrive()
    {
        lockWalk = false;
    }
}
