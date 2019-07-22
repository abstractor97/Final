using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer : MonoBehaviour
{
   
    public StupidAI ai;
    // Start is called before the first frame update
    void Start()
    {
        ai= gameObject.AddComponent<StupidAI>();
        ai.runCallBack += Run;
    }

    // Update is called once per frame
    void Update()
    {
       
      
    }

    void Run()
    {

    }
   
}
