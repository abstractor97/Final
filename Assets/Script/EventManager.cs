using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// todo 全局事件发送，管理
/// </summary>
public class EventManager : MonoBehaviour
{
    public UnityAction action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveEvent()
    {

    }

    public class CustomEvent
    {
        public EventCode code;

        public float drgee;
    }

    public enum EventCode
    {

    }

    
}
