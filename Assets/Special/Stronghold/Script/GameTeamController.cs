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
    public int rtLv=1;
    /// <summary>
    /// 等待招募的队列
    /// </summary>
    public List<People1> waitRecruit;
    /// <summary>
    /// 囚犯队列
    /// </summary>
    public List<People1> cellPrisoners;
    /// <summary>
    /// 当列表更新时刷新
    /// </summary>
    public bool recruitRefresh;

    private People1[] lv1;
    private People1[] lv2;
    private People1[] lv3;

    public GameTeamController()
    {
        waitTeam = new List<People1>(maxTeam);
        outTeam = new Team();
        waitRecruit = new List<People1>();
        cellPrisoners = new List<People1>();
       //todo 转为AB包资源
        lv1 = Resources.LoadAll<People1>("Assets/People1/Lv1");
        lv2 = Resources.LoadAll<People1>("Assets/People1/Lv2");
        lv3 = Resources.LoadAll<People1>("Assets/People1/Lv3");
    }


    public void CreateRecruitOnLv()
    {
        int createNumber = 2;
        createNumber += rtLv - 1;
        for (int i = 0; i < createNumber; i++)
        {
            int r = Random.Range(0, 100);
            if (r < 100 - rtLv * 10)
            {
                waitRecruit.Add( lv1[ Random.Range(0,lv1.Length-1)]);
            }
            else if (100 - rtLv * 10 < r && r < 100 - rtLv * 3)
            {
                waitRecruit.Add(lv1[Random.Range(0, lv2.Length - 1)]);
            }
            else
            {
                waitRecruit.Add(lv1[Random.Range(0, lv3.Length - 1)]);
            }
        }
        recruitRefresh = true           ;


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
