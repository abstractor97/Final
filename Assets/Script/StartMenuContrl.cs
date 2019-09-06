using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//todo ui
public class StartMenuContrl : MonoBehaviour
{

    [Tooltip("进入加载界面")]
    public GameObject loaddlg;
    [Tooltip("界面")]
    public GameObject storyMenu;
    /// <summary>
    /// 加载进度条
    /// </summary>
    private Slider loadingSlider;
    /// <summary>
    /// 文本
    /// </summary>
    private Text loadingText;

    private readonly string storyOS = "Asset/StoryAssets";

    private Story[] stories;
    AsyncOperation asyncOperation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Continue()
    {
        ToGameScene();
    }

    public void StartNew()
    {
        storyMenu.SetActive(true);
             stories = Resources.LoadAll<Story>(storyOS);
            Transform storyList = storyMenu.transform.Find("StoryList").transform.GetChild(0).transform.GetChild(0);
            for (int j = 0; j < stories.Length; j++)
            {
                GameObject t = new GameObject();
                ListItem l = t.AddComponent<ListItem>();
                l.sel = j;
                l.leftAction = StorySelect;
                Text text = t.AddComponent<Text>();
                text.text = stories[j].name;
                text.fontSize = 24;
                text.color = new Color(0f, 0f, 0f);
                text.alignment = TextAnchor.MiddleCenter;
                t = GameObject.Instantiate<GameObject>(t);
                t.transform.SetParent(storyList);
                if (j == 0)
                {
                    //GameObject.Find("StoryScore/DegreeCivilization").GetComponent<Image>().fillAmount = stories[j].degreeCivilization;
                    //GameObject.Find("StoryScore/Difficulty").GetComponent<Image>().fillAmount = stories[j].difficulty;
                    //Transform storyNote = storyMenu.transform.Find("StoryNote").transform.GetChild(0);
                    //storyNote.gameObject.GetComponent<Text>().text = stories[j].describe;
                storyMenu.GetComponentInChildren<Text>().text = stories[j].describe;
                storyMenu.GetComponentInChildren<Image>().sprite = stories[j].icon;
            }
            }
        
    }

    int storyIndex = 0;

    private void StorySelect(int i)
    {
        //GameObject.Find("StoryScore/DegreeCivilization").GetComponent<Image>().fillAmount = stories[i].degreeCivilization;
        //GameObject.Find("StoryScore/Difficulty").GetComponent<Image>().fillAmount = stories[i].difficulty;
        //Transform storyNote = GameObject.Find("StorySelectMenu").transform.Find("StoryNote").transform.GetChild(0);
        //storyNote.gameObject.GetComponent<Text>().text = stories[i].describe;
        storyMenu.GetComponentInChildren<Text>().text = stories[i].describe;
        storyMenu.GetComponentInChildren<Image>().sprite = stories[i].icon;
        storyIndex = i;
    }


    private void ToGameScene()
    {
        loaddlg.SetActive(true);
        loadingSlider = loaddlg.transform.Find("Slider").GetComponent<Slider>();
        loadingText = loaddlg.transform.Find("Slider/Text").GetComponent<Text>();
        asyncOperation = SceneManager.LoadSceneAsync("MapScene");
        //StartCoroutine(LoadingScene());
    }

    public void LoadProgress(int progress)
    {
        if (loadingSlider.value != 1.0f)
        {
            loadingText.text = "加载中。。。" + (loadingSlider.value * 100).ToString() + "%";
        }
    }

    public void Quit()
    {
        Application.Quit();

    }

    private IEnumerator LoadingScene()
    {
        asyncOperation.allowSceneActivation = false;  //如果加载完成，也不进入场景

        int toProgress = 0;
        int showProgress = 0;

        //测试了一下，进度最大就是0.9
        while (asyncOperation.progress < 0.9f)
        {
            toProgress = (int)(asyncOperation.progress * 100);

            while (showProgress < toProgress)
            {
                showProgress++;
                LoadProgress( showProgress);
            }
            yield return new WaitForEndOfFrame(); //等待一帧
        }
        //计算0.9---1   其实0.9就是加载好了，我估计真正进入到场景是1  
        toProgress = 100;

        while (showProgress < toProgress)
        {
            showProgress++;
            LoadProgress( showProgress);
            yield return new WaitForEndOfFrame(); //等待一帧
        }
        asyncOperation.allowSceneActivation = true;  //如果加载完成，可以进入场景
        //yield return new WaitForEndOfFrame(); 

        Messenger.OnListenerRemoved(EventCode.APP_START_GAME);
    }
}
