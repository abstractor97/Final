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
                case TakeAction.explore:
                   // todo placeselect
                  //  Explore();
                    break;
                case TakeAction.collection:
                    Collection();
                    break;
                case TakeAction.tocamp:                   
                        FindObjectOfType<DayTime>().JumpTime(points.holdTime);                                    
                    break;
                case TakeAction.medical:
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
