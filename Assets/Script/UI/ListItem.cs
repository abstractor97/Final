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

    public GameObject panel;

    public string panelPath;

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
        if (panelPath != null)
        {
            panel = gameObject.transform.parent.Find("ListPanel")?.gameObject;
            if (panel == null)
            {
                panel = Resources.Load<GameObject>(panelPath);
                panel = GameObject.Instantiate<GameObject>(panel);
                panel.transform.SetParent(gameObject.transform.parent,false);
                panel.transform.position = gameObject.transform.position + new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x/2, gameObject.GetComponent<RectTransform>().sizeDelta.y / 2);
            }
            else {
                panel.transform.position = gameObject.transform.position + new Vector3(gameObject.GetComponent<RectTransform>().sizeDelta.x / 2, gameObject.GetComponent<RectTransform>().sizeDelta.y / 2);
            }
            panelAction?.Invoke(panel);
            CanvasGroup group = panel.GetComponent<CanvasGroup>();
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
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


