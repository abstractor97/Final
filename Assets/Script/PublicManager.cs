using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Map;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class PublicManager:MonoBehaviour
{

    public GameObject PointsUI;

    public bool lockWalk;

    public GameObject arlog;

    public GameObject timelog;

    public GameObject actionFrame;

    public GameObject selectNum;

    private GameObject cacheUI;

 

    private void Start()
    {
        PointsUI = GameObject.Instantiate<GameObject>(PointsUI);
        Hide(PointsUI);
      //  DontDestroyOnLoad(gameObject);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&cacheUI!=null)
        {
            Hide(cacheUI);
        }
    }

  
    public void ShowSelectNum(int max,int min,UnityAction<int> num)
    {
        selectNum.GetComponent<SelectNumDialog>().action += num;
        selectNum.GetComponent<SelectNumDialog>().max = max;
        selectNum.GetComponent<SelectNumDialog>().min = min;
        Show(selectNum);
    }

    public void ShowPointsUIThis(Vector3 post) {
        post= Camera.main.WorldToScreenPoint(post);
        post.x += 3;
        post.y -= PointsUI.GetComponent<RectTransform>().sizeDelta.y;
        PointsUI.transform.position = post;
        Show(PointsUI);
    }

    public void HidePointsUI() {
        Hide(PointsUI);
    }

    public void ChangePointsUI(string name,string del)
    {
        Text[] texts= PointsUI.GetComponentsInChildren<Text>();
        texts[0].text = name;
        texts[1].text = del;
    }

    public void ShowArlog(string note,Ardialog.Callback callback)
    {
        arlog.GetComponentInChildren<Text>().text = note;
        arlog.GetComponent<Ardialog>().call += callback;
        Show(arlog);
    }

    public void ShowTimeDialog(TimeChoiceDialog.TimeCallback callback)
    {
       // timelog.GetComponentInChildren<Text>().text = note;
        timelog.GetComponent<TimeChoiceDialog>().callback += callback;
        Show(timelog);
    }

    public void ShowActionFrame(Points.EventNote[] et, UnityAction<int> left)
    {
        // timelog.GetComponentInChildren<Text>().text = note;
      //  FindObjectOfType<MapControl>().pointsControl.points.eventSend.points
        actionFrame.GetComponent<GridView>().AddDataDef(et, ActionFrame,left);
        Show(actionFrame);
    }


    void ActionFrame(GameObject ui, Points.EventNote et)
    {
        ui.GetComponentInChildren<Text>().text = et.t;
    }

    public void Show(GameObject ui)
    {
        Hide(cacheUI);
        cacheUI = ui;
        CanvasGroup group = ui.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void Hide(GameObject ui)
    {     
        CanvasGroup group = ui.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
        cacheUI = null;
    }
}
