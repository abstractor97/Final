using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    /// <summary>
    /// 强化的值
    /// </summary>
    public DigitText strText;

    public DigitText resources_0;

    public DigitText resources_1;

    public Button up;

    public PersonnelMenu.Attribute attribute;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int lv,int res_0,int res_1)
    {
        strText.text = lv.ToString();
        resources_0.text = res_0.ToString();
        resources_1.text = res_1.ToString();
    }

    public int GetLevel()
    {
        return int.Parse(strText.text);
    }

    public void AddUp(UnityAction<PersonnelMenu.Attribute> upAction)
    {
        up.onClick.AddListener(delegate() { upAction(attribute); } );

    }

   
}
