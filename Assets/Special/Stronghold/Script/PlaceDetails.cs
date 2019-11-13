using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceDetails : MonoBehaviour
{
    public Text terrainl;
    public DigitText level;
    public Text risk;
    public Text expose;
    public Text arrivalTime;

    public GameObject teamList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Details details) {
        terrainl.text = Enum.GetName(typeof(Terrainl), details.terrainl);
        level.text = details.lv.ToString();
    }

    public void ToSetOut() {
        GameObject tl = GameObject.Instantiate<GameObject>(teamList);
       tl.transform.SetParent(PublicManager.HUD,false);
    }



    public class Details
    {
        public Terrainl terrainl;
        public int lv;
        public string enemyRisk;

    }


    public enum Terrainl
    {
        mountain,
        plain,
        rivers,
    }

}




