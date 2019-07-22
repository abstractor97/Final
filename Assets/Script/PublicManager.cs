using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PublicManager:MonoBehaviour
{

    public GameObject PointsUI;

    // public 


    private void Start()
    {
        PointsUI = GameObject.Instantiate<GameObject>(PointsUI);
        CanvasGroup group= PointsUI.AddComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    public void ShowPointsUIThis(Vector3 post) {
        post= Camera.main.WorldToScreenPoint(post);
        post.x += 3;
        post.y -= PointsUI.GetComponent<RectTransform>().sizeDelta.y;
        PointsUI.transform.position = post;
        CanvasGroup group = PointsUI.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void HidePointsUI() {
        CanvasGroup group = PointsUI.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    public void ChangePointsUI(string name,string del)
    {
        Text[] texts= PointsUI.GetComponentsInChildren<Text>();
        texts[0].text = name;
        texts[1].text = del;
    }
}
