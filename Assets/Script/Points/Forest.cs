using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Forest : EventEmitter
    {
        public override void OnAction(int i)
        {
            switch (eventNotes[i].e)
            {
                case Event.explore:
                    break;
                case Event.collection:
                    break;
                case Event.tocamp:
                    if (points.holdType== Stronghold.StrongholdControl.Type.none)
                    {
                        points.holdType = Stronghold.StrongholdControl.Type.forest;
                        FindObjectOfType<MapPlayer>().dayTime.JumpTime(holdTime);
                    }                  
                    break;
                case Event.medical:
                    break;
            }
            
        }


      
    }

}
