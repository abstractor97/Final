using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuContrl : MonoBehaviour
{

    public Image blackbr;

    public RectTransform backGround;

    [Tooltip("界面")]
    public GameObject storyMenu;

    private readonly string storyOS = "Asset/Story";

    private Story[] stories;

    // Start is called before the first frame update
    void Start()
    {
        CanvasGroup canvasGroup = blackbr.GetComponent<CanvasGroup>();
        Tween tween = canvasGroup.DOFade(1, 1f).SetLoops(-1,LoopType.Yoyo);
        
    }


    public void MoveShowMenu()
    {
        backGround.transform.DOLocalMoveY(540, 1.5f);
    }

    public void StartNew()
    {
        storyMenu.transform.DOLocalMoveY(0,0.5f);
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

    private void StorySelect(int i)
    {
        storyMenu.GetComponentInChildren<Text>().text = stories[i].describe;
        storyMenu.GetComponentInChildren<Image>().sprite = stories[i].icon;
        ProcessManager.factor = stories[i];
    }


    public void DefStory()
    {
        ProcessManager.factor = Resources.Load<Story>(storyOS+ "/简约之旅");
    }

    public void ToGameScene()
    {
        if (ProcessManager.factor==null)
        {
            DefStory();
        }
        FindObjectOfType<ProcessManager>().CreateOnAutoSave();
        PublicManager.ToScene(this, "StrongholdScene");
    }

    public void CloseNew()
    {
        storyMenu.transform.DOLocalMoveY(540, 0.5f);
    }

    public void Continue()
    {
        PublicManager.ShowSaveMenu();
    }

    public void Set()
    {
        PublicManager.ShowSetMenu();
    }

    public void Quit()
    {
        PublicManager.ShowArlog("是否退出", delegate (Pass pass) {
            if (pass == Pass.yes)
            {
                Application.Quit();
            }
        });
    }

   
}
