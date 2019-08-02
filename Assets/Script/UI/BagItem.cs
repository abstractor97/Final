using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : MonoBehaviour, IPointerClickHandler
{
    public Image icon;

    public Text itemName;

    public Text note;

    public Text num;

    public Text weight;

    public Button delete;

    public UnityAction<int> clickAction;

    public UnityAction<int> deleteAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Left)
        {
            clickAction.Invoke(int.Parse(name));
        }
    }

    public void Delete()
    {
        deleteAction.Invoke(int.Parse(name));
    }

    // Start is called before the first frame update
    void Start()
    {
        delete.onClick.AddListener(Delete);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
