using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI增强/Tab页")]
public class TabPage : MonoBehaviour
{
    [Tooltip("页签")]
    public GameObject tabTag;

    public Direction direction;

    private TabLayout tabLayout;
    /// <summary>
    /// 要添加的是第几个tag
    /// </summary>
    public static int tagPostion { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        tabLayout = transform.parent.GetComponent<TabLayout>();
        tabTag = GameObject.Instantiate<GameObject>(tabTag);
        tabTag.transform.SetParent(transform, false);
        tabTag.transform.position = new Vector3(ta,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Direction
    {
        left,
        right,
        centr,
        up,
        down,
    }
}
