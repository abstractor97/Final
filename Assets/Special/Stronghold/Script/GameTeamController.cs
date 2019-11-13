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

    public List<People1> waitTeam;
    /// <summary>
    /// 整备好的队伍
    /// </summary>
    public List<Team> outTeam;

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
    private int id;

    public GameTeamController()
    {
        waitTeam = new List<People1>(maxTeam);
        outTeam = new List<Team>();
        waitRecruit = new List<People1>();
        cellPrisoners = new List<People1>();
       //todo 转为AB包资源
        lv1 = Resources.LoadAll<People1>("Assets/People1/Lv1");
        lv2 = Resources.LoadAll<People1>("Assets/People1/Lv2");
        lv3 = Resources.LoadAll<People1>("Assets/People1/Lv3");
        id = ProcessManager.GetSaveData<int>("TeamId");
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


    public Team CreateTeam()
    {
        id++;
       return new Team {id=id-1 };
    }

    public void AddOutTeam(int id,int i, People1 people)
    {
        outTeam.Find((Team t)=>
        {
            return t.id == id;
        }).teamMembers[i] = people;
    }

    public void AddOutTeamSp(int id,int i, People1 people)
    {
        outTeam.Find((Team t) =>
        {
            return t.id == id;
        }).supports[i] = people;
    }

    /// <summary>
    /// 出发检查
    /// </summary>
    public bool DepartureCheck(int id)
    {
        foreach (var tm in outTeam.Find((Team t) =>{return t.id == id;}).teamMembers)
        {
            if (tm != null)
            {
                return true;
            }
        }
        return false;
    }

    public class Team
    {

        public int id;
        public People1[] teamMembers=new People1[3];
        public Buff1 buff;
        public People1[] supports = new People1[2];
    }
}
