using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UI增强/鼠标放置悬浮框")]
public class SuspensionFrame : MonoBehaviour
{
    public Vector3 deviation=new Vector3(50,50,0);

    public GameObject frame;

    private SuspensionItem[] suspensionItems;

    private bool isCache;

 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetFrame()
    {
        if (!isCache)
        {
            frame = GameObject.Instantiate<GameObject>(frame);
            frame.transform.SetParent(transform, false);
            isCache = true;
        }    
        return frame;
    }
}
