using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "自定义地点", menuName = "自定义生成系统/地点")]
public class Place : ScriptableObject
{
    [Range(0,100)]
    public int intact;
    [Tooltip("完整度从前到后")]
    public Sprite[] lowSprite;

    public bool isShow;


    public List<ExploreAction> firstActions;


    public ExploreAction[] exploreActions;

    [System.Serializable]
    public struct ExploreAction
    {
        public EventEmitter.ExploreEvent type;
        [Tooltip("yarn文件")]
        public TextAsset note;
        [Tooltip("开始标签")]
        public string talkToNode;

        [Range(0, 1)]
        [Tooltip("触发概率")]
        public float probability;

        public bool isTrigger;

        public People people;

        public Place place;

        //  public 

        //  public UnityEvent et;
    }

}
