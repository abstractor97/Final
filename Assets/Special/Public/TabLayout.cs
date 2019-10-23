using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI增强/Tab页管理")]
public class TabLayout : MonoBehaviour
{

    private TabPage[] pages;

    private int tagPostion;

    // Start is called before the first frame update
    void Start()
    {
        pages = GetComponentsInChildren<TabPage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTagIndex()
    {
        return tagPostion;
    }
}
