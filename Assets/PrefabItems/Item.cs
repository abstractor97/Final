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

    public float weight;

    public ItemType type;

    public Use[] use;

    public enum ItemType
    {
        available,
        material,
        equip
    }
    [System.Serializable]
    public struct Use
    {
        public UseType type;

        public PlayerManager.StateTag state;

        public float totalTime;

        public float degree;

        public Buff buff;

        public Equip equip;
    }

    public enum UseType
    {
        recovery,
        buff,
        arm,
        hat,
        clothes_up,
        clothes_down,
        shoes,
    }
}

