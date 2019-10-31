using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Map;
using UnityEngine.Events;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
/// <summary>
///  通用ui显示控制
/// </summary>
public class PublicManager:MonoBehaviour
{
    [HideInInspector]
    public bool lockWalk;
    [HideInInspector]
    public GameObject actionFrame;

    public static RectTransform HUD;

    public static GameObject PointsUI;

    public static GameObject arlog;

    public static GameObject timelog;

    public static GameObject loaddlg;

    public static GameObject selectNum;

    public static GameObject saveMenu;

    public static GameObject setMenu;

    public static GameObject pauseMenu;

    public static List<GameObject> cacheUIs;

    public static UINames Names;

    public EditObj editObj;

    [Serializable]
    public class EditObj
    {
        public RectTransform HUD;

        public GameObject arlog;

        public GameObject timelog;

        public GameObject loaddlg;

        public GameObject selectNum;

        public GameObject saveMenu;

        public GameObject setMenu;

        public GameObject pauseMenu;
    }

    public class UINames
    {
        public string HUD;

        public string arlog;

        public string timelog;

        public string loaddlg;

        public string selectNum;

        public string saveMenu;

        public string setMenu;

        public string pauseMenu;
    }

    private void Awake()
    {
        #region 初始化

        if (editObj != null)
        {
            HUD = editObj.HUD;
            arlog = editObj.arlog;
            timelog = editObj.timelog;
            loaddlg = editObj.loaddlg;
            selectNum = editObj.selectNum;
            saveMenu = editObj.saveMenu;
            setMenu = editObj.setMenu;
            pauseMenu = editObj.pauseMenu;
            editObj = null;
        }
        cacheUIs = new List<GameObject>();
        #endregion

    }

    private void Start()
    {
       
        //  DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&cacheUIs.Count>0)
        {
            CloseUpperUI();
        }
    }
  

    public void ShowPointsUIThis(Vector3 post) {
        PointsUI = GameObject.Instantiate<GameObject>(PointsUI);
        post = Camera.main.WorldToScreenPoint(post);
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
        texts[0].text = ProcessManager.language.Text(name);
        texts[1].text = ProcessManager.language.Text(del);
    }


    #region 各种提示框构造
    /// <summary>
    /// 显示确认提示框
    /// </summary>
    /// <param name="note"></param>
    /// <param name="callback"></param>
    public static void ShowArlog(string note, UnityAction<Pass> callback)
    {
        GameObject ar = GameObject.Instantiate<GameObject>(arlog);
        ar.GetComponentInChildren<Text>().text = ProcessManager.language.Text(note);
        ar.GetComponent<Ardialog>().call += callback;
        ar.transform.SetParent(HUD, false);
        cacheUIs.Add(ar);
        //  Show(ar);
    }

    public static void ShowTimeDialog(string note, UnityAction<string> callback)
    {
        GameObject tl = GameObject.Instantiate<GameObject>(timelog);
        tl.GetComponent<TimeChoiceDialog>().SetText(ProcessManager.language.Text(note));
        tl.GetComponent<TimeChoiceDialog>().callback += callback;
        tl.transform.SetParent(HUD, false);
        cacheUIs.Add(tl);
        //  Show(tl);
    }

    public static void ShowSelectNum(int max, int min, UnityAction<int> num)
    {
        GameObject sn = GameObject.Instantiate<GameObject>(selectNum);
        sn.GetComponent<SelectNumDialog>().action += num;
        sn.GetComponent<SelectNumDialog>().max = max;
        sn.GetComponent<SelectNumDialog>().min = min;
        sn.transform.SetParent(HUD, false);
        cacheUIs.Add(sn);
        // Show(sn);
    }

    public static void ShowTips(string t)
    {

        GameObject ts = Resources.Load<GameObject>("UI/Tips");
        ts = GameObject.Instantiate<GameObject>(ts);
        ts.GetComponentInChildren<Text>().text = ProcessManager.language.Text(t); ;
        ts.transform.SetParent(GameObject.Find("HUD").transform, false);
        ts.AddComponent<LastLife>().time = 2;
    }
    #endregion

    #region 系统菜单
    public static void ShowSaveMenu()
    {
        GameObject lm = GameObject.Instantiate<GameObject>(saveMenu);
        lm.transform.SetParent(HUD, false);
        cacheUIs.Add(lm);
        //  Show(lm);
    }

    public static void ShowSetMenu()
    {
        GameObject sm = GameObject.Instantiate<GameObject>(setMenu);
        sm.transform.SetParent(HUD, false);
        cacheUIs.Add(sm);
        // Show(sm);
    }

    public static void ShowPauseMenu()
    {
        GameObject pm = GameObject.Instantiate<GameObject>(pauseMenu);
        pm.transform.SetParent(HUD, false);
        cacheUIs.Add(pm);
        // Show(sm);
    }
    #endregion


    #region Scene切换加载界面
    public static void ToScene(MonoBehaviour mono,string sceneName)
    {
        GameObject ld= GameObject.Instantiate<GameObject>(loaddlg);
        Slider loadingSlider = ld.transform.Find("Slider").GetComponent<Slider>();
        Text loadingText = ld.transform.Find("Slider/Text").GetComponent<Text>();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        mono.StartCoroutine(LoadingScene(asyncOperation, loadingSlider, loadingText));
    }

    private static void LoadProgress(Slider loadingSlider,Text loadingText, int progress)
    {
        loadingSlider.value = progress / 100;
        if (loadingText.text.Length<6)
        {
            loadingText.text = loadingText.text + ".";
        }
        else
        {
            loadingText.text = "加载中";
        }
    }

    private static IEnumerator LoadingScene(AsyncOperation asyncOperation, Slider loadingSlider, Text text)
    {
        asyncOperation.allowSceneActivation = false;  //如果加载完成，也不进入场景

        int toProgress = 0;
        int showProgress = 0;

        //测试了一下，进度最大就是0.9
        while (asyncOperation.progress < 0.9f)
        {
            toProgress = (int)(asyncOperation.progress * 1000/9);
            LoadProgress(loadingSlider,text, toProgress);           
            yield return new WaitForEndOfFrame(); 
        }
        //计算0.9---1   其实0.9就是加载好了，我估计真正进入到场景是1  
        toProgress = 100;

        //while (showProgress < toProgress)
        //{
        //    showProgress++;
        //    LoadProgress(showProgress);
        //    yield return new WaitForSeconds(0.1f); 
        //}
        asyncOperation.allowSceneActivation = true;  //如果加载完成，可以进入场景
        yield return new WaitForEndOfFrame();
        Destroy(loadingSlider.transform.parent.gameObject);
    }

    #endregion

    
    #region 控制canvasgroup显隐
    public static void Show(GameObject ui)
    {
        CanvasGroup group = ui.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public static void Hide(GameObject ui)
    {
        CanvasGroup group = ui.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    #endregion


    #region 关闭自身唤起的UI
    public static void CloseUIs()
    {
        for (int i = 0; i < cacheUIs.Count; i++)
        {
            Destroy(cacheUIs[i]);
        }
        cacheUIs.Clear();
    }

    public static void CloseUpperUI()
    {
        if (cacheUIs.Count > 1)
        {
            GameObject cui = cacheUIs[cacheUIs.Count - 1];
            cacheUIs.Remove(cui);
            Destroy(cui);
        } 
    }

    public static void CloseUI(string name)
    {
        GameObject cui = cacheUIs.Find(ui => ui.name.Equals(name));
        cacheUIs.Remove(cui);
        Destroy(cui);
    }
    #endregion


    public static bool IsShow(string name)
    {
        GameObject cui = cacheUIs.Find(ui => ui.name.Equals(name));
        if (cui==null)
        {
            return false;
        }
        else
        {
            return true;
        }
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

  
}
