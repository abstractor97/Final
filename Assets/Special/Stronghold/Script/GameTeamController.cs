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

    private Team outTeam;

    public int maxTeam=20;
    /// <summary>
    /// 招募等级
    /// </summary>
    public int rtLv;

    public List<People1> waitRecruit;



    public GameTeamController()
    {
        waitTeam = new List<People1>(maxTeam);
        outTeam = new Team();
        waitRecruit = new List<People1>();

    }

    public bool CheckWaitTeam()
    {
        return waitTeam.Count < maxTeam;
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

    public void AddOutTeam(int i, People1 people)
    {
        outTeam.teamMembers[i] = people;
    }

    public void AddOutTeamSp(int i, People1 people)
    {
        outTeam.supports[i] = people;
    }

    /// <summary>
    /// 出发检查
    /// </summary>
    public bool DepartureCheck()
    {
        foreach (var tm in outTeam.teamMembers)
        {
            if (tm!=null)
            {
                return true;
            }
        }
        return false;
    }

    public class Team
    {

       public People1[] teamMembers=new People1[3];

        public Buff1 buff;

        public bool dead;

        public People1[] supports = new People1[2];
    }
}
