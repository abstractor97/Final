using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputActionButton : MonoBehaviour,IPointerClickHandler
{
    public RoundController roundController;
    public RoundController.InputAction ia;
    public RoundController.Posture s_posture;
    public int move;
    public float drgee;
    public RoundController.Posture p_posture;
    public RoundController.Posture e_posture;
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Left)
        {
            roundController.CAction(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (roundController==null)
        {
            roundController = FindObjectOfType<RoundController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
