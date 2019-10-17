using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private List<InputActionButton> inputActionButtons=new List<InputActionButton>();
    private RoundController roundController;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tr in gameObject.transform)
        {
            InputActionButton[] iabs= ((Transform)tr).gameObject.GetComponentsInChildren<InputActionButton>();
            foreach (var iab in iabs)
            {
                inputActionButtons.Add(iab);
            }
        }
        roundController = FindObjectOfType<RoundController>();
      //  ResetButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void ResetButton()
    //{
    //    foreach (var iab in inputActionButtons)
    //    {
    //        if (IsShow(iab))
    //        {
    //            iab.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            iab.gameObject.SetActive(false);
    //        }
    //    }
    //}

    //private bool IsShow(InputActionButton inputAction)
    //{
    //    if (inputAction.s_posture== RoundController.Posture.none)
    //    {
    //        return true;
    //    }

    //    if (roundController.GetPlayer().posture==inputAction.s_posture)
    //    {
    //        switch (inputAction.m_posture)
    //        {
    //            case RoundController.Posture.none:
    //                return true;
    //            default:
    //                if (roundController.GetTaget().posture == inputAction.m_posture)
    //                {
    //                    return true;

    //                }
    //                break;
    //        }
           
    //    }
       
    //    return false;
    //}
}
