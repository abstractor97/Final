using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceDetails : MonoBehaviour
{
    public Text terrainl;
    public Text level;
    public Text risk;
    public Text expose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Details details) {

    }

    public class Details
    {
        public Terrainl details;
        public int lv;
        public string enemyRisk;

    }


}

public enum Terrainl
{
    mountain,
    plain,
    rivers,
}


