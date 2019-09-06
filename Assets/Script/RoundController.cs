using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [HideInInspector]
    public int roundNumber { get; private set; }
    public Round round;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CAction()
    {
        Round.RoundAction roundAction = new Round.RoundAction {
            type = Round.RoundType.move,
            startAction=delegate () {  }
        };
        round.AddAction(roundAction);
    }
    [System.Serializable]
    public class Instruction
    {

    }
}
