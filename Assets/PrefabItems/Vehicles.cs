using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "自定义载具", menuName = "自定义生成系统/载具")]
public class Vehicles : ScriptableObject
{
    public string name;
    public string describe;
    public float speed;
    public float hp;
    public float oilConsumption;
    public float totalOil;

}
