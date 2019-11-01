using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTeamController
{
    private static GameTeamController gameData;

    public static GameTeamController GameData
    {
        private set { gameData = value; }
        get
        {
            if (gameData == null)
            {
                gameData = new GameTeamController();
            }
            return gameData;
        }
    }

    private List<People1> waitTeam;

    private List< TeamMember> outTeam;

    private int maxTeam=10;



    public GameTeamController()
    {
        waitTeam = new List<People1>(maxTeam);
        outTeam = new List<TeamMember>(4);
    }

    public bool AddWaitTeam(People1 people)
    {
        if (waitTeam.Count >= maxTeam)
        {
            return false;
        }
        else
        {
            waitTeam.Add(people);
            return true;
        }

    }

    public void FireWaitTeam(People1 people)
    {
        waitTeam.Remove(people);
    }

    public bool AddOutTeam(People1 people)
    {
        if (outTeam.Count >= 4)
        {
            return false;
        }
        else
        {
            TeamMember tm = new TeamMember
            {
                people = people
            };
            outTeam.Add(tm);
            return true;
        }

    }

    /// <summary>
    /// 出发检查
    /// </summary>
    public bool DepartureCheck()
    {
        return outTeam.Count == 4;
    }

    public class TeamMember
    {

        public int id;

       public People1 people;

        public Buff1 buff;

        public bool dead;
    }
}
