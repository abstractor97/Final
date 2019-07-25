using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridView<T> : MonoBehaviour
{
    // public int allCapacity = 0;//总容量
    public int lineNum = 1;
    [Tooltip("整体距上")]
    public float top = 0;
    [Tooltip("整体距左")]
    public float left = 0;
    [Tooltip("格子间上间距")]
    public float autoTop = 0;
    [Tooltip("格子间左间距")]
    public float autoLeft = 0;
    [Tooltip("格子大小")]
    public float autoSize = 0;

    private T[] items;
    private UnityAction<int> leftc;

    public GameObject item;

    public delegate void ViewCallBack(GameObject ui,T item);

    public event ViewCallBack view;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddData(T[] t, ViewCallBack view, UnityAction<int> left)
    {
        items = t;
        leftc = left;
        this.view = view;
        NotExistItems();
    }


    /// <summary>
    /// 如果bu存在
    /// </summary>
    void NotExistItems()
    {
       
        int j = 0;
        for (int i = 0; i < items.Length; i++)
        {
            AutoAddLattice(i, j == 0 ? 0 : j / lineNum);
            j++;
        }              
    }

    /// <summary>
    /// 自动设置格子信息
    /// </summary>
    /// <param name="x"> 第几个</param>
    /// <param name="y">第几行</param>
    void AutoAddLattice(int x, int y)
    {
      
        //实例化
        GameObject mitem = Instantiate(item) as GameObject;
        mitem.name = x.ToString();
        mitem.AddComponent<ListItem>().leftAction += leftc;        
        RectTransform trans = item.GetComponent<RectTransform>();

        trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left + autoLeft + (autoLeft + autoSize) * (x % lineNum), autoSize);
        trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top + autoTop + (autoTop + autoSize) * y, autoSize);
        // trans.offsetMin = Vector2.zero;
        // trans.offsetMax = Vector2.zero;
        mitem.transform.SetParent(gameObject.GetComponent<RectTransform>().transform, false);//再将它设为canvas的子物体
        view(mitem,view,items[x]);
    }

   
}
