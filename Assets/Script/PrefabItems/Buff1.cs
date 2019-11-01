using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "自定义状态", menuName = "自定义生成系统/状态1")]
public class Buff1 : ScriptableObject
{

    public string describe;

    public Sprite lowSprite;

    public BuffState state;

    [Tooltip("持续时间")]
    public int totalWeek=1;

    [System.Serializable]
    public struct BuffState
    {

        public int str;

        public int agi;

        public int mint;

        public int end;
    }

}
