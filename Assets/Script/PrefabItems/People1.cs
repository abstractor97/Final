using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "自定义人物", menuName = "自定义生成系统/自定义人物")]
public class People1 : ScriptableObject
{

    [Tooltip("薪资")]
    public int salary;
    [Tooltip("每周消耗的食物")]
    public int consume = 1;
    [Tooltip("默认士气")]
    public int morale = 100;

    public Attribute attribute;
    /// <summary>
    /// 其他和属性有关
    /// hp=end*10
    /// sp=mint*5
    /// </summary>
    [System.Serializable]
    public class Attribute
    {
        public int str;
        public int agi;
        public int mint;
        public int end;

    }

    
}
