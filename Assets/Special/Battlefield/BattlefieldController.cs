using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlefieldController : MonoBehaviour
{

    [HideInInspector]
    public GameTeamController.Team team;

    public RectTransform lounge;

    public WaitCard waitCard;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var teamer in team.teamMembers)
        {
            WaitCard card = GameObject.Instantiate<WaitCard>(waitCard);
            card.Init(teamer.head,true);
            card.transform.SetParent(lounge, false);
        }

        foreach (var teamer in team.supports)
        {
            WaitCard card = GameObject.Instantiate<WaitCard>(waitCard);
            card.Init(teamer.head, false);
            card.transform.SetParent(lounge, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
