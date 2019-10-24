using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI增强/Tab页")]
public class TabPage : MonoBehaviour
{
    [Tooltip("页签")]
    public GameObject tabTag;

    public string tagTitle;

    public Direction direction;

    public float space;
    /// <summary>
    /// tag计数
    /// </summary>
    public static float tagPostion=1;

    public static int oldClick = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (tagPostion==1)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
        else
        {
            gameObject.AddComponent<CanvasGroup>().alpha = 0;
        }

        tabTag = GameObject.Instantiate<GameObject>(tabTag);
        if (direction == Direction.left || direction == Direction.right)
        {
            
            tabTag.GetComponent<RectTransform>().position =  new Vector3(-transform.parent.GetComponent<RectTransform>().sizeDelta.x / 2 -tabTag.GetComponent<RectTransform>().sizeDelta.x/2, transform.parent.GetComponent<RectTransform>().sizeDelta.y / 2- (tabTag.GetComponent<RectTransform>().sizeDelta.y + space)*(tagPostion-0.5f) +space/2 ,0);
        }
        else
        {
            tabTag.transform.position = new Vector3(transform.position.x + tabTag.GetComponent<RectTransform>().sizeDelta.x * tagPostion, transform.position.y + tabTag.GetComponent<RectTransform>().sizeDelta.y);
        }
        tagPostion++;
        tabTag.transform.SetParent(transform.parent, false);
        tabTag.GetComponentInChildren<Text>().text = tagTitle;
        ListItem li = tabTag.AddComponent<ListItem>();
        li.name = ((int)tagPostion - 2).ToString();
        li.leftAction=delegate(int i) {
            transform.parent.GetChild(oldClick).GetComponent<CanvasGroup>().alpha = 0;

            transform.parent.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
            oldClick = i;
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Direction
    {
        left,
        right,
        centr,
        up,
        down,
    }

}


