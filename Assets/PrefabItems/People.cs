using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "自定义NPC", menuName = "自定义生成系统/NPC")]
public class People : ScriptableObject
{
    public string name;
    public string describe;
    [Range(0, 100)]
    public int relation;

    public Inclination inclination;


    public enum Inclination
    {
        free,
        order,
        confusion,
    }
}
