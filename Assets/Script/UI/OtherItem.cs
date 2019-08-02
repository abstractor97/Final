using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OtherItem : MonoBehaviour, IPointerClickHandler
{
    public Button addBag;

    public UnityAction<int> clickAction;

    public UnityAction<int> addAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            clickAction.Invoke(int.Parse(name));
        }
    }

    public void Add()
    {
        addAction.Invoke(int.Parse(name));
    }

    // Start is called before the first frame update
    void Start()
    {
        addBag.onClick.AddListener(Add);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
