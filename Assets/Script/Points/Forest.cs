using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Forest : EventEmitter
    {
        public override void OnAction(int i)
        {
            switch (points.eventNotes[i].e)
            {
                case Event.explore:
                    Explore();
                    break;
                case Event.collection:
                    Collection();
                    break;
                case Event.tocamp:
                    if (points.holdType== Stronghold.StrongholdControl.Type.none)
                    {
                        points.holdType = Stronghold.StrongholdControl.Type.forest;
                        FindObjectOfType<DayTime>().JumpTime(points.holdTime);
                    }                  
                    break;
                case Event.medical:
                    break;
            }
            
        }


        public override void OnStronghold(int i)
        {
            base.OnStronghold(i);
            switch (points.HoldNotes[i])
            {
                case HoldEvent.cook:
                    break;
                case HoldEvent.readiness:
                    break;
                case HoldEvent.sleep:
                    break;
                case HoldEvent.dismantle:
                    break;
            }
        }
    }

}
