using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ardialog : MonoBehaviour
{

    public delegate void Callback(Pass pass);
    public event Callback call;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
        call(Pass.yes);
        CanvasGroup group = gameObject.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    public void No()
    {
        call(Pass.no);
        CanvasGroup group = gameObject.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    public enum Pass
    {
        yes,
        no
    }

}
