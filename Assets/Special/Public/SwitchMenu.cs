using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 切换菜单
/// </summary>
public class SwitchMenu : MonoBehaviour
{
    public RectTransform bLayout;

    public MenuGroup[] groups;

    public int defShow;

    private int nowShow;

    [System.Serializable]
    public class MenuGroup
    {
        public Button button;
        public CanvasGroup menu;
    }


    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var group in groups)
        {
            group.button.gameObject.name = i.ToString();
            group.button.onClick.AddListener(delegate () { ShowThis(int.Parse(group.button.gameObject.name)); });
            group.menu.alpha = 0;
            i++;
        }

        groups[defShow].button.onClick.Invoke();
        nowShow = defShow;
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
                nowShow = groups.Length - 1;
            }
            groups[nowShow].button.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            nowShow++;
            if (nowShow > groups.Length - 1)
            {
                nowShow = 0;
            }
            groups[nowShow].button.onClick.Invoke();
        }
    }

    public void ShowThis(int id)
    {
        Debug.LogWarning(id);

        groups[nowShow].menu.alpha = 0;
        nowShow = id;
        //buttons[id].按钮切换特效
        groups[nowShow].menu.alpha = 1;
    }
}
