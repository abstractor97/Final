using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "自定义状态", menuName = "自定义生成系统/状态")]
public class Buff : ScriptableObject
{

    public string describe;

    public Sprite lowSprite;

    public bool isPlayer;

    public BuffState[] state;

    public float totalTime;


    public enum BuffType
    {
        speed,
        state,

    }
    [System.Serializable]
    public struct BuffState
    {
        public BuffType type;

        public PlayerManager.StateTag tag;

        public float drgee;

        //public float totalTime;
    }

}
