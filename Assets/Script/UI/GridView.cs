using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridView : MonoBehaviour
{
    public int all { get; private set; } = 0;
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

    public GameObject item;
    //todo
    public bool drawLattice;

    public delegate void ViewCallBack<T>(GameObject ui,T item);

  //  private event ViewCallBack View;


    // Start is called before the first frame update
    void Start()
    {
        GridLayout grid= gameObject.AddComponent<GridLayout>();
        if (drawLattice)
        {

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDataDef<T>(T[] t, ViewCallBack<T> view,UnityAction<int> leftc)
    {
        all = t.Length;
        int j = 0;
        for (int i = 0; i < t.Length; i++)
        {
            // AutoAddLattice(i, j == 0 ? 0 : j / lineNum);
            //实例化
            GameObject mitem = Instantiate(item) as GameObject;
            mitem.name = i.ToString();
              mitem.AddComponent<ListItem>().leftAction += leftc;
            //RectTransform trans = item.GetComponent<RectTransform>();

            //trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left + autoLeft + (autoLeft + autoSize) * (i % lineNum), trans.sizeDelta.x);
            //trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top + autoTop + (autoTop + autoSize) * j == 0 ? 0 : j / lineNum, trans.sizeDelta.y);

            mitem.transform.SetParent(gameObject.GetComponent<RectTransform>().transform, false);//再将它设为canvas的子物体
            view(mitem, t[i]);
            j++;
        }
    }

    public void AddData<T>(T[] t, ViewCallBack<T> view)
    {
        all = t.Length;
        int j = 0;
        for (int i = 0; i < t.Length; i++)
        {
           // AutoAddLattice(i, j == 0 ? 0 : j / lineNum);
            //实例化
            GameObject mitem = Instantiate(item) as GameObject;
            mitem.name = i.ToString();
          //  mitem.AddComponent<ListItem>().leftAction += leftc;
            //RectTransform trans = item.GetComponent<RectTransform>();

            //trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left + autoLeft + (autoLeft + autoSize) * (i % lineNum), trans.sizeDelta.x);
            //trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top + autoTop + (autoTop + autoSize) * j == 0 ? 0 : j / lineNum, trans.sizeDelta.y);

            mitem.transform.SetParent(gameObject.GetComponent<RectTransform>().transform, false);//再将它设为canvas的子物体
            view(mitem, t[i]);
            j++;
        }
    }

    public void UpData<T>(T[] t, ViewCallBack<T> view)
    {
        all = t.Length;
        foreach (var tr in gameObject.transform)
        {
            Destroy(((Transform)tr).gameObject);
        }
        int j = 0;
        for (int i = 0; i < t.Length; i++)
        {
            // AutoAddLattice(i, j == 0 ? 0 : j / lineNum);
            //实例化
            GameObject mitem = Instantiate(item) as GameObject;
            mitem.name = i.ToString();
          //  mitem.AddComponent<ListItem>().leftAction += leftc;
            //RectTransform trans = item.GetComponent<RectTransform>();

            //trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left + autoLeft + (autoLeft + autoSize) * (i % lineNum), trans.sizeDelta.x);
            //trans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top + autoTop + (autoTop + autoSize) * j == 0 ? 0 : j / lineNum, trans.sizeDelta.y);
        
            mitem.transform.SetParent(gameObject.GetComponent<RectTransform>().transform, false);//再将它设为canvas的子物体
            view(mitem, t[i]);
            j++;
        }
    }

    public void AddItem<T>(T t, ViewCallBack<T> view)
    {
        int i = all + 1;
        GameObject mitem = Instantiate(item) as GameObject;
        mitem.name = i.ToString();
     //   mitem.AddComponent<ListItem>().leftAction += leftc;

        mitem.transform.SetParent(gameObject.GetComponent<RectTransform>().transform, false);//再将它设为canvas的子物体
        view(mitem, t);
        all++;
    }

    public void Remove(int sel)
    {
        //foreach (var tr in gameObject.transform)
        //{
        //    if (((Transform)tr).gameObject.name.Equals(sel.ToString()))
        //    {
        //        Destroy(((Transform)tr).gameObject);
        //    }

        //}
        Destroy(gameObject.transform.GetChild(sel).gameObject);
        
    }
 
    public void Sort(int a,int l)
    {
        gameObject.transform.GetChild(a).SetSiblingIndex(l);
        gameObject.transform.GetChild(l + 1).SetSiblingIndex(a);
      

    }
   
}
