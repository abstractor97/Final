using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputActionButton : MonoBehaviour,IPointerClickHandler
{
    public RoundController roundController;
    /// <summary>
    /// 输入类型
    /// </summary>
    public RoundController.InputAction ia;
    /// <summary>
    /// 状态限制
    /// </summary>
    public RoundController.Posture s_posture;
    /// <summary>
    /// 目标状态限制
    /// </summary>
    public RoundController.Posture m_posture;
    public float move;
    public float drgee;
    public float negotiation;
    /// <summary>
    /// 自身状态变为...
    /// </summary>
    public RoundController.Posture p_posture;
    /// <summary>
    /// 目标状态变为...
    /// </summary>
    public RoundController.Posture e_posture;
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Left)
        {
            roundController.CreateAction(this);
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
