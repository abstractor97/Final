using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "自定义道具", menuName = "自定义生成系统/道具")]
public class Item : ScriptableObject
{

    public string name;

    public Cost cost;

    public string describe;

    public Sprite lowSprite;

    public ItemControl control;

    public float weight;
}


public enum Cost
{
    ordinary,
    rare,
    seldomSeen,
}