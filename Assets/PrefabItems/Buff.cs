using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "自定义状态", menuName = "自定义生成系统/状态")]
public class Buff : ScriptableObject
{
    public string name;
    public string describe;
    public Sprite lowSprite;
    public BuffControl control;
    public bool isPlayer;
}
