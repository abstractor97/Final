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
        texts[0].text = ProcessManager.Instance.language.Text(name);
        texts[1].text = ProcessManager.Instance.language.Text(del);
    }

    public void ShowArlog(string note,Ardialog.Callback callback)
    {
        arlog.GetComponentInChildren<Text>().text = ProcessManager.Instance.language.Text(note);
        arlog.GetComponent<Ardialog>().call += callback;
        Show(arlog);
    }

    public void ShowTimeDialog(string note, UnityAction<string> callback)
    {
        // timelog.GetComponentInChildren<Text>().text = note;
        timelog.GetComponent<TimeChoiceDialog>().SetText(ProcessManager.Instance.language.Text(note));
        timelog.GetComponent<TimeChoiceDialog>().callback += callback;
        Show(timelog);
    }
    [Obsolete]
    public void ShowActionFrame(EventEmitter.TakeAction[] et, UnityAction<int> left)
    {
        // timelog.GetComponentInChildren<Text>().text = note;
      //  FindObjectOfType<MapControl>().pointsControl.points.eventSend.points
        actionFrame.GetComponent<GridView>().AddDataDef(et, ActionFrame,left);
      //  Show(actionFrame);
    }

    [Obsolete]
    void ActionFrame(GameObject ui, EventEmitter.TakeAction et)
    {
       // ui.GetComponentInChildren<Text>().text = et.t;
    }

    public void ShowTips(string t)
    {
        
      GameObject ts=  Resources.Load<GameObject>("UI/Tips");
      ts=  GameObject.Instantiate<GameObject>(ts);
        ts.GetComponentInChildren<Text>().text = ProcessManager.Instance.language.Text(t); ;
        ts.transform.SetParent(GameObject.Find("HUD").transform,false);
        LastLife(ts,2);
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

    public GameObject AdditionalFrame(GameObject ui)
    {
        GameObject frame = Resources.Load<GameObject>("UI/ControlledFrame");
        frame = GameObject.Instantiate<GameObject>(frame);
        RectTransform uiRect = ui.GetComponent<RectTransform>();
        frame.GetComponent<RectTransform>().sizeDelta = uiRect.sizeDelta + new Vector2(20, 70);

        uiRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, uiRect.sizeDelta.y / 2 + 70, uiRect.sizeDelta.y);
        uiRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, uiRect.sizeDelta.x / 2 + 10, uiRect.sizeDelta.x);
        uiRect.SetParent(frame.transform, false);
        frame.GetComponentInChildren<Button>().onClick.AddListener(delegate () { Destroy(frame); });
        return frame;
    }

    public void LastLife(GameObject obj, float t)
    {
        StartCoroutine(LastDes(obj,t));
    }

    private IEnumerator LastDes(GameObject obj,float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);

    }
}
