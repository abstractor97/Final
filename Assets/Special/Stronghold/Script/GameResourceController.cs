using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourceController
{
    private static GameResourceController gameData;

    public static GameResourceController GameData
    {
        private set { gameData = value; }
        get
        {
            if (gameData == null)
            {
                gameData = new GameResourceController();
            }
            return gameData;
        }
    }

    private Dictionary<Type, int> resourceData;

    private GameResourceController() {
        int i = Enum.GetNames(typeof(Type)).Length;
        resourceData = new Dictionary<Type, int>(i);
        foreach (Type type in Enum.GetValues(typeof(Type)))
        {
            resourceData.Add(type, 0);
        }      
    }

    public void ResourceAdd(Type type,int num)
    {
        int dnum = resourceData[type] + num;
        dnum= dnum < 0 ? 0 : dnum;
        resourceData.Add(type,dnum);
    }
    /// <summary>
    /// 存量比检查量大,返回true
    /// </summary>
    /// <param name="type"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool Check(Type type, int num)
    {
        return resourceData[type] > num;
    }

    public bool Check(Dictionary<Type,int> pairs)
    {
        foreach (var p in pairs)
        {
            if (resourceData[p.Key] < p.Value)
            {
                return false;
            }
           
        }
        return true;
    }

    public enum Type
    {
        coin,
        food,
        mineral,
        crystal,
    }
}
