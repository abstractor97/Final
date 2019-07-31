
using UnityEngine;
/// <summary>
/// 格子类型
/// </summary>
public enum ItemType
{
    material,
    food,
    water,
    consumables,
}

public class ItemInfo
{
    public string name="";
    public string note="";
    public int num=0;
    public float weight=0;
    public float cost;
    public bool isUse=true;
    public Sprite sprite;
    public int maxNum;
    public ItemType type;
}