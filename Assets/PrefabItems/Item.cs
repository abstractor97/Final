using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "自定义道具", menuName = "自定义生成系统/道具")]
public class Item : ScriptableObject
{

   // public string name;

    public float cost;

    public string describe;

    public Sprite lowSprite;

    public UnityAction<Item> use;

    public float weight;

    public ItemType type;
}

