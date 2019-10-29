using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UI增强/鼠标放置悬浮框_子项")]
public class SuspensionItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SuspensionFrame Suspension;

    public void OnPointerEnter(PointerEventData eventData)
    {
            Suspension.GetFrame().GetComponent<CanvasGroup>().alpha = 1;
            Suspension.GetFrame().transform.position = transform.position + Suspension. deviation;
   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Suspension.GetFrame().GetComponent<CanvasGroup>().alpha = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Suspension = transform.parent.GetComponent<SuspensionFrame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
