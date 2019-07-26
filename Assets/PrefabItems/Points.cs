using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "自定义兴趣点", menuName = "自定义生成系统/兴趣点/兴趣点")]
public class Points : ScriptableObject
{
    public string name;
    public string describe;
    public Sprite lowSprite;
    // public PointsControl control;
    //public Scripts
    //public EventEmitter eventSend;
    public Buff[] buffs;
    public Item[] Items;
    public People[] people;
    public string[] environments;

    public Stronghold.StrongholdControl.Type holdType;
}
