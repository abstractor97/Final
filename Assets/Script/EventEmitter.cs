using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventEmitter
{
    public Points points;

    public abstract void OnSend();

    public abstract void OnWait ();

    public abstract void OnCamp();

    public abstract void OnArrive();

    public abstract void OnLeave();


}

