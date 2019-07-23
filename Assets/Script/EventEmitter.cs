using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventEmitter
{


    public abstract void OnSend();
    public abstract void OnSleep ();
    public abstract void OnCamp();
    public abstract void OnArrive();

    public abstract void OnLeave();


}

