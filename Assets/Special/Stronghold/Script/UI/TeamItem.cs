using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamItem : MonoBehaviour
{
    public RectTransform mainTeamer;
    public RectTransform spTeamer;
    public CanvasGroup selectFarme;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(GameTeamController.Team team)
    {
       Image[] mains= mainTeamer.GetComponentsInChildren<Image>();
        for (int i = 0; i < mains.Length; i++)
        {
            mains[i].sprite = team.teamMembers[i].head;
        }
        Image[] sps = spTeamer.GetComponentsInChildren<Image>();
        for (int i = 0; i < sps.Length; i++)
        {
            sps[i].sprite = team.supports[i].head;
        }

    }
}
