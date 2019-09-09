using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "自定义NPC", menuName = "自定义生成系统/NPC")]
public class People : ScriptableObject
{
  //  public string name;
    public string describe;
    [Range(0, 100)]
    [Tooltip("关系")]
    public int relation;
    [Tooltip("倾向")]
    public Inclination inclination;

    public State state;

    [System.Serializable]
    public class State
    {
        public string other;
        public float hp;
        public float power;
        /// <summary>
        /// 体力消耗量
        /// </summary>
        public float powerLoop;


        public float moveSpeed;
        /// <summary>
        /// 重量
        /// </summary>
        public float weight;
        public float water;
        /// <summary>
        /// 饱腹度
        /// </summary>
        public float satiety;
        /// <summary>
        /// 基准饱腹度
        /// </summary>
        public float benchmarkSatiety;
        /// <summary>
        /// hp自然流逝率
        /// </summary>
        public float hpEfflux;
        public float powerEfflux;
        public float satietyEfflux;
        public float waterEfflux;
    }

    [System.Serializable]
    public class Attribute
    {
        public int str;
        public int ski ;
        public int pow;
        public int luk;

    }

    public enum Inclination
    {
        free,
        order,
        confusion,
    }
}
