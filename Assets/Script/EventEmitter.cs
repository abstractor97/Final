using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventEmitter : MonoBehaviour
{
    public Points points;

    /// <summary>
    /// 速度乘数
    /// </summary>
    [Range(0,10)]
    public float speedMultiplier;
    [Range(0, 10)]
    public float powerMultiplier;

    public virtual void OnAction() {

    }

    public abstract void OnWait();

    public abstract void OnCamp();

    public virtual void OnArrive(){
        FindObjectOfType<MapPlayer>().state.moveSpeed *= speedMultiplier;
        FindObjectOfType<MapPlayer>().state.powerLoop *= powerMultiplier;
    }

    public virtual void OnLeave()
    {
        FindObjectOfType<MapPlayer>().state.moveSpeed /= speedMultiplier;
        FindObjectOfType<MapPlayer>().state.powerLoop /= powerMultiplier;
    }


}

