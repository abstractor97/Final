using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ListItem : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    //  [SerializeField]
    //  public ItemEventTrigger m_ItemEventTrigger = new ItemEventTrigger();

    // Start is called before the first frame update

    public int sel;

    public UnityAction<int> leftAction;
    public UnityAction<int> rightAction;
    public UnityAction<int> enterAction;
    public UnityAction<int> exitAction;

    public GameObject panel;

    public UnityAction<GameObject> panelAction;

    void Start()
    {
        sel = int.Parse(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                leftAction?.Invoke(sel);

            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                rightAction?.Invoke(sel);

            }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panel != null)
        {
            panel = gameObject.transform.parent.Find("ListPanel")?.gameObject;

            panel.transform.position = gameObject.transform.position + new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x / 2, gameObject.GetComponent<RectTransform>().sizeDelta.y / 2);

            panelAction?.Invoke(panel);
            CanvasGroup group = panel.GetComponent<CanvasGroup>();
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
        enterAction?.Invoke(sel);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (panel != null)
        {
            CanvasGroup group = panel.GetComponent<CanvasGroup>();
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
        exitAction?.Invoke(sel);
    }




    //  [Serializable]
    //  public class ItemEventTrigger
    //  {
    //     public int index = 0;

    //    [Serializable]
    //    public class ItemEvent : UnityEvent
    //   {

    //   }

    // [SerializeField]
    //  public ItemEvent itemEvent = new ItemEvent();
    // }

}


