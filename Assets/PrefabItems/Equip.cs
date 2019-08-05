using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "装备数据", menuName = "自定义生成系统/装备数据")]
public class Equip : ScriptableObject
{
    public int attack;

    public int protect;

    public int warm;

    public int beautiful;
}
