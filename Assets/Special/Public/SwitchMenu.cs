using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMenu : MonoBehaviour
{
    public RectTransform bLayout;

    public Button[] buttons;

    public CanvasGroup[] menus;

    public int defShow;

    private int nowShow;

    private int oidShow;
    // Start is called before the first frame update
    void Start()
    {
        if (bLayout!=null)
        {
            buttons = bLayout.GetComponentsInChildren<Button>();
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(delegate () { ShowThis(i); });
            menus[i].alpha = 0;
        }
        buttons[defShow].onClick.Invoke();
        //  transform.DOScale(transform.position,1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            nowShow--;
            if (nowShow < 0)
            {
                nowShow = menus.Length - 1;
            }
            buttons[nowShow].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            nowShow++;
            if (nowShow > menus.Length - 1)
            {
                nowShow = 0;
            }
            buttons[nowShow].onClick.Invoke();
        }
    }

    public void ShowThis(int id)
    {
        menus[oidShow].alpha = 0;
        nowShow = id;
        //buttons[id].按钮切换特效
        menus[id].alpha = 1;
        oidShow = nowShow;
    }
}
