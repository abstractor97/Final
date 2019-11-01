using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using UnityEngine.Events;
/// <summary>
/// 数据驱动的对话系统
/// params 为对话参数【参数描述自行约定】
///所有分行都要在行首添加>符，包括注释,需要外部实现控制节点
/// </summary>
public class Dialogue : MonoBehaviour
{
    //  public GameObject fullScreen;
    /// <summary>
    /// 生成选项的示例，至少要拥有一个button脚本和一个text脚本
    /// </summary>
    public GameObject selectExample;

    //  public GameObject downScreen;
    /// <summary>
    /// 对话框
    /// </summary>
    public RectTransform dialogueFrame;
    /// <summary>
    /// 显示文字的控件，不传入会自动在dialogueFrame下寻找名称为"Text"的子控件
    /// </summary>
    public Text text;
    /// <summary>
    /// 放置选项的框，自动生成的选项会添加到这里，堆叠式对话文本也会被添加进这里,
    /// 不传入会自动在dialogueFrame下寻找名称为"ButtonFrame"的子控件
    /// </summary>
    public RectTransform buttonFrame;

    /// <summary>
    /// 受文本控制的对话图像，不传入的话会自动寻找，可用于筛选
    /// </summary>
    public Image[] images;

    /// <summary>
    /// 跳转下一句所需的按键留空为任意键
    /// </summary>
    public string nextKey;
    /// <summary>
    /// 每个段落文本处理完成后返回给外部
    /// </summary>
    [HideInInspector]
    public UnityAction<string> titleCallBack;
    /// <summary>
    /// 播放完成
    /// </summary>
    [HideInInspector]
    public UnityAction endCallBack;
    /// <summary>
    /// 对话历史
    /// </summary>
    [HideInInspector]
    public List<string> history;

    public Font font;

    public string DialoguePath = "Assets/Story/";
    /// <summary>
    /// 是否启动打字机效果
    /// </summary>
    public bool isWriter=true;

    [HideInInspector]
    public bool hasText;
#if UNITY_EDITOR
    public TextAsset testAsset;
#endif
    /// <summary>
    /// 全局参数
    /// </summary>
    private Dictionary<string, string> dialogueOaData;

    private Dictionary<string, Runner> dialogueData;

    private Dictionary<string, string> dialoguePairs;

    private string readPath;

    private Mode mode;

    private string runDialoueTitle;
    [HideInInspector]
    public bool isRun;

    // private 

    // Start is called before the first frame update
    void Start()
    {
        if (images == null)
        {
            images = dialogueFrame.gameObject.GetComponentsInChildren<Image>();

        }
        dialogueFrame.gameObject.AddComponent<CanvasGroup>().alpha = 0;
        history = new List<string>();
#if UNITY_EDITOR
        dialoguePairs = new Dictionary<string, string>();
        dialogueData = new Dictionary<string, Runner>();
        LoadText(testAsset.ToString());
        Show(Mode.stacked).Play("jianmian");
#endif
    }

// Update is called once per frame
void Update()
    {
        if (nextKey.Equals(""))
        {
            if (Input.anyKeyDown && isRun)
            {
                Next();
            }

        }
        else {
            if ( Input.GetKeyDown(nextKey) && isRun)
            {

                Next();
            }
        }
        
    }

    private void Next()
    {
        string data = dialogueData[runDialoueTitle].Next();
#if UNITY_EDITOR
        Debug.LogWarning(data);
#endif

        if (data.Equals("END"))
        {
            isRun = false;
            hasText = false;
            dialogueFrame.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            endCallBack?.Invoke();
            if (text != null)
            {
                text.text = "";
            }
            foreach (var f in buttonFrame)
            {
                Destroy(((Transform)f).gameObject);
            }

        }
        else
        {
            if (Analysis(data) > 0)
            {
                Analysis(data);
            }

        }
    }


    /// <summary>
    /// 加载对话文本
    /// </summary>
    /// <param name="dialogueName">名称或路径</param>
    public Dialogue Load(string dialogueName)
    {
        if (dialogueName.Contains("/"))
        {
            readPath = "";
            string[] paths= dialogueName.Split('/');
            for (int i = 0; i < paths.Length-1; i++)
            {
                readPath = readPath + paths[i] + "/";
            }
        }
        
        TextAsset textAsset = Resources.Load<TextAsset>(DialoguePath+dialogueName);
        dialoguePairs = new Dictionary<string, string>();
        dialogueData = new Dictionary<string, Runner>();
        LoadText(textAsset.ToString());

        hasText = true;
        return this;
    }

    /// <summary>
    /// 加载对话文本
    /// </summary>
    /// <param name="dialogueName">名称或路径</param>
    public Dialogue Load(TextAsset dialogue)
    {
        dialoguePairs = new Dictionary<string, string>();
        dialogueData = new Dictionary<string, Runner>();
        LoadText(dialogue.ToString());

        hasText = true;
        return this;
    }

    /// <summary>
    /// 设置显示模式
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public Dialogue Show(Mode mode)
    {
        this.mode = mode;
        if (text == null&&mode!=Mode.stacked)
        {
            text = dialogueFrame?.Find("Text").gameObject.GetComponent<Text>();
            text.gameObject.AddComponent<CanvasGroup>().alpha = 0;
        }
        if (buttonFrame == null)
        {
            buttonFrame = dialogueFrame?.transform.Find("ButtonFrame").gameObject.GetComponent<RectTransform>();

        }
        VerticalLayoutGroup verticalLayout= buttonFrame.gameObject.AddComponent<VerticalLayoutGroup>();
        verticalLayout.childControlHeight = false;
        verticalLayout.childControlWidth = true;
        verticalLayout.childForceExpandHeight = false;
        verticalLayout.spacing = 4;
        buttonFrame.gameObject.AddComponent<CanvasGroup>().alpha = 0;
        dialogueFrame.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        if (isWriter && mode != Mode.stacked)
        {
            text.gameObject.AddComponent<Wirter>().wirterOver=delegate(string s) { isRun = true; };
        }
        return this;
    }
    /// <summary>
    /// todo 设置显示图片张数（默认为dialogueframe下子对象中image控件的数量）
    /// </summary>
    /// <returns></returns>
    public Dialogue Image()
    {
        return this;
    }

    /// <summary>
    /// 开始播放
    /// </summary>
    /// <param name="nodeName"></param>
    public Dialogue Play(string nodeName)
    {
        if (nodeName.Equals(""))
        {
            foreach (var data in dialogueData)
            {
                nodeName = data.Key;
                break;
            }
        }
        if (dialogueData != null&& dialogueData.ContainsKey(nodeName))
        {
            if (!isWriter)
            {
                isRun = true;
            }
            runDialoueTitle = nodeName;
            dialogueData[runDialoueTitle].index = 0;//重置下下标
            if (Analysis(dialogueData[runDialoueTitle].Next())>0)
            {
                Analysis(dialogueData[runDialoueTitle].Next());
            } ;//从第一句开始
        }
        else
        {
            throw new Exception("未载入数据或没有找到节点");
        }
        titleCallBack?.Invoke(nodeName);
        return this;
    }
    /// <summary>
    ///强制停止 
    /// </summary>
    public void Stop() {
        isRun = false;
        hasText = false;
        dialogueFrame.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        endCallBack?.Invoke();
        if (text != null)
        {
            text.text = "";
        }
        foreach (var f in buttonFrame)
        {
            Destroy(((Transform)f).gameObject);
        }
    }

    /// <summary>
    /// 托管参数，托管后参数会受到剧本选项等影响
    /// </summary>
    public void TrustParam(string pName,string pValue)
    {
        dialoguePairs.Add(pName,pValue);
        if (dialoguePairs.ContainsKey(pName))
        {
#if UNITY_EDITOR
            Debug.LogWarning("重复托管参数:"+ pName);
#endif
        }
    }

    /// <summary>
    /// 取出某一个托管参数
    /// </summary>
    public string GetTrustParam(string pName)
    {
        return dialoguePairs[pName];
        //if (dialoguePairs.ContainsKey(pName))
        //{

        //}
    }
    /// <summary>
    /// 托管全局参数，托管后参数会受到剧本选项等影响，这个参数不会被回收//todo 剧本中添加
    /// </summary>
    public void TrustOverallParam(string pName, string pValue)
    {
        dialogueOaData.Add(pName, pValue);
    }

    /// <summary>
    /// 解析文本
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private int Analysis(string data)
    {

        if (data.Contains("["))
        {
            if (mode != Mode.stacked)
            {
                text.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                buttonFrame.GetComponent<CanvasGroup>().alpha = 1;
                foreach (var f in buttonFrame)
                {
                    Destroy(((Transform)f).gameObject);
                }
            }
            bool inBracket = false;
           
            StringBuilder sbr = new StringBuilder();
            foreach (var s in data)
            {
                if (s.Equals(']'))
                {
                    inBracket = false;
                }
                if (inBracket)
                {
                    sbr.Append(s);
                }
                if (s.Equals('['))
                {
                    inBracket = true;
                }
            }
            int imageIndex = 0;
            foreach (var s in sbr.ToString().Split('|'))
            {
                string[] pcs = s.Split('#');
                
                foreach (var pc in pcs)
                {
                    if (pc.Contains("-"))
                    {
                        string[] pcm = pc.Split('-');
                        string pam = dialoguePairs[pcm[0]]==null?dialogueOaData[pcm[0]]: dialoguePairs[pcm[0]];
                        dialoguePairs[pcm[0]]= (int.Parse(pam)-int.Parse(pcm[1])).ToString();
                        continue;
                    }
                    if (pc.Contains("+"))
                    {
                        string[] pcm = pc.Split('+');
                        string pam = dialoguePairs[pcm[0]] == null ? dialogueOaData[pcm[0]] : dialoguePairs[pcm[0]];
                        dialoguePairs[pcm[0]] = (int.Parse(pam) + int.Parse(pcm[1])).ToString();
                        continue;
                    }
                    if (pc.Contains("="))
                    {
                        string[] pcm = pc.Split('=');
                        string pam = dialoguePairs[pcm[0]] == null ? dialogueOaData[pcm[0]] : dialoguePairs[pcm[0]];
                        dialoguePairs[pcm[0]] = pcm[1];
                        continue;
                    }
                    if (pc.StartsWith("path:"))
                    {
                       images[imageIndex].sprite= Resources.Load<Sprite>(DialoguePath + readPath + pc.Substring(5, pc.Length));
                        imageIndex++;
                    }
                }
                if (!pcs[0].Trim().Equals(""))
                {
                    isRun = false;
                    GameObject b=null;
                    if (selectExample==null)
                    {
                        b = new GameObject();
                        b.AddComponent<Button>();
                        b.AddComponent<Text>().font = font;
                        b.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                        b.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);
                    }
                    else
                    {
                        b = GameObject.Instantiate<GameObject>(selectExample);
                    }
                    Text btext = b.GetComponent<Text>();
                    if (btext == null)
                    {
                        btext = b.GetComponentInChildren<Text>();
                    }
                    btext.text = pcs[0];              
                    b.GetComponent<Button>().onClick.AddListener(delegate () {
                        isRun = true;
                        if (dialogueData.ContainsKey(pcs[0]))
                        {
                            Play(dialogueData[pcs[0]].title);
                        }
                        else
                        {
                            Next();
                        }                      
                        foreach (var button in buttonFrame.GetComponentsInChildren<Button>())
                        {
                            button.onClick.RemoveAllListeners();
                        }

                    });
                    b.transform.SetParent(buttonFrame, false);
                }
             
            }
            if (imageIndex > 0)//如果这一行操作图片，自动下移一行
            {
                return 1;
            }
         
        }
        else
        {
            if (mode == Mode.stacked)
            {
                GameObject b = new GameObject
                {
                    name = "text"
                };
                // b = GameObject.Instantiate<GameObject>(b);
                Text t = b.AddComponent<Text>();            
                t.font = font;
                t.alignment = TextAnchor.MiddleLeft;
                b.GetComponent<RectTransform>().sizeDelta=new Vector2(100,30);
                if (isWriter)
                {
                    Wirter wirter= b.AddComponent<Wirter>();
                    wirter.AddQueue(data);
                    wirter.wirterOver = delegate (string s) { isRun = true; };
                }
                else
                {
                    t.text = data;
                }            
                b.transform.SetParent(buttonFrame,false);
                buttonFrame.GetComponent<CanvasGroup>().alpha = 1;
            }
            else {
                text.text = data;
                buttonFrame.GetComponent<CanvasGroup>().alpha = 0;
                text.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        return 0;
    }

    public void SubmitAsync(string dialogueName)
    {
        Resources.Load<TextAsset>(DialoguePath + dialogueName);
    }

    private void LoadText(string text)
    {
        dialoguePairs.Clear();
        dialogueData.Clear();
        string[] ls = text.Split('>');
        Runner runner=null;
        foreach (var l in ls)
        {

            if (!l.TrimStart().StartsWith("//")&&!l.Trim().Equals(""))
            {
                if (l.StartsWith("title:"))
                {
                    if (runner!=null)
                    {
                        dialogueData.Add(runner.title, runner);
                    }
                    runner = new Runner
                    {
                        title = l.Trim().Substring(6, l.Trim().Length-6)
                    };
                    continue;
                }
                if (l.StartsWith("params:"))
                {
                    foreach (var param in l.Trim().Substring(7, l.Trim().Length-7).Split('$'))
                    {
                        if (!param.Trim().Equals(""))
                        {
                            string[] pst = param.Split('=');
                            dialoguePairs.Add(pst[0].Trim(), pst[1].Trim());
                        }
                    }
                    continue;
                }
                if (l.StartsWith("paramsOa:"))
                {
                    foreach (var param in l.Trim().Substring(7, l.Trim().Length - 7).Split('$'))
                    {
                        if (!param.Trim().Equals(""))
                        {
                            string[] pst = param.Split('=');
                            dialogueOaData.Add(pst[0].Trim(), pst[1].Trim());
                        }
                    }
                    continue;
                }
                if (l.StartsWith("sprite:"))
                {
                    string cz = "[";
                    foreach (var sp in l.Trim().Substring(7, l.Trim().Length-8).Split(','))
                    {
                        cz =cz+"#path:"+sp;
                    }
                    cz += "]";
                    runner?.sentence.Add(cz);
                    continue;
                }
                runner?.sentence.Add(l.Trim());
            }
        }
        if (!dialogueData.ContainsKey(runner.title))
        {
            dialogueData.Add(runner.title, runner);
        }


    }


    public class Runner
    {
        public List<string> sentence = new List<string>();

        public string title;

        public int index;

        public string Next()
        {
            index++;
            if (index <= sentence.Count)
            {
                return sentence[index-1];
            }
            else {
                index = 0;
                return "END";
            }
        }
    }

    public enum Mode
    {
        stacked,
        cover,
    }


 
}
