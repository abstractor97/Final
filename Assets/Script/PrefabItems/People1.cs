using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "自定义人物", menuName = "自定义生成系统/自定义人物1")]
public class People1 : ScriptableObject
{

    public Sprite head;

    public string defName;

    [Tooltip("薪资")]
    public int salary;
    [Tooltip("每周消耗的食物")]
    public int consume = 1;
    [Tooltip("默认士气")]
    public int morale = 100;

    public int lv = 1;

    public Attribute attribute;
    public Skill skill;
    [Tooltip("简述")]
    public TextAsset sketch;
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
    [System.Serializable]
    public class Skill
    {
        public int explore;
        public int battle;
        public int transport;
        public int strain;

        public SuperSkill superName;
    }
    [System.Serializable]
    public class Equip
    {
       
    }

    public enum SuperSkill
    {
        reinforcements,
        shelling,
        subjugate,
        garrison
    }
}
