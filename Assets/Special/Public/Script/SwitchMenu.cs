using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SwitchMenu : MonoBehaviour
{
    public RectTransform bLayout;

    private MenuGroup[] groups;

    public Button[] buttons;

    public RectTransform mainBackground;

    public float storeyHeight;

    public int defShow;

    private int nowShow;

    private Sequence se;
    [System.Serializable]
    public class MenuGroup
    {
        public Button button;
        public RectTransform menu;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (bLayout!=null)
        {
            buttons = bLayout.GetComponentsInChildren<Button>();
        }

        int i = 0;
        foreach (var button in buttons)
        {
            button.gameObject.name = i.ToString();
            button.onClick.AddListener(delegate () { ShowThis(int.Parse(button.gameObject.name)); });
            i++;
        }
        //int i = 0;
        //foreach (var group in groups)
        //{
        //    group.button.gameObject.name = i.ToString();
        //    group.button.onClick.AddListener(delegate () { ShowThis(int.Parse(group.button.gameObject.name)); });
        // //   group.menu.alpha = 0;
        //    i++;
        //}
        se = DOTween.Sequence();

        se.SetAutoKill(false);
        buttons[defShow].onClick.Invoke();
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
        Debug.LogWarning(storeyHeight * (id - nowShow));

        //  groups[nowShow].menu.alpha = 0;
        se.Append(mainBackground.DOLocalMoveY(storeyHeight * (id -buttons.Length/2), 1.5f));
        nowShow = id;
        //buttons[id].按钮切换特效
        //  groups[nowShow].menu.alpha = 1;
    }

    private void OnDestroy()
    {
        se.Kill();
    }
}
