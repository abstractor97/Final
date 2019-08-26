using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// 数据驱动的对话系统
/// params 为对话参数【参数描述自行约定】
/// 默认参数 img_*(0-9)对话时要显示的图片 
/// </summary>
public class Dialogue : MonoBehaviour
{
  //  public GameObject fullScreen;
  /// <summary>
  /// 选项的示例，至少要拥有一个button脚本和一个text脚本
  /// </summary>
    public GameObject selectExample;

  //  public GameObject downScreen;
    /// <summary>
    /// 对话框
    /// </summary>
    public GameObject dialogueFrame;

   // public bool 

    public string DialoguePath = "Assets/Story/";

    private Dictionary<string, Runner> dialogueData;

    private Dictionary<string, string> dialoguePairs;

    private string readPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void InitFullScreen(Sprite sprite)
    {
       // dialogueFrame = GameObject.Instantiate<GameObject>(fullScreen);
        dialogueFrame.GetComponent<Image>().sprite = sprite;
         
    }

    private void InitDownScreen(Sprite p1, Sprite p2)
    {
       // dialogueFrame = GameObject.Instantiate<GameObject>(downScreen);
        if (p1!=null)
        {
            dialogueFrame.GetComponentsInChildren<Image>()[0].sprite = p1;
        }
        if (p2 != null)
        {
            dialogueFrame.GetComponentsInChildren<Image>()[1].sprite = p2;
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
            string[] paths= dialogueName.Split('/');
            for (int i = 0; i < paths.Length-1; i++)
            {
                readPath = readPath + paths[i] + "/";
            }
        }
        
        TextAsset textAsset = Resources.Load<TextAsset>(DialoguePath+dialogueName);
        dialoguePairs = new Dictionary<string, string>();
        LoadText(textAsset.ToString());
    
     
        return this;
    }

    /// <summary>
    /// 设置显示模式
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public Dialogue Mode(ShowMode mode)
    {
        switch (mode)
        {
            case ShowMode.full:
                InitFullScreen(Resources.Load<Sprite>(DialoguePath + readPath+ dialoguePairs["img_0"]));
                break;
            case ShowMode.twoSpeaker:
                InitDownScreen(Resources.Load<Sprite>(DialoguePath + readPath + dialoguePairs["img_0"]), Resources.Load<Sprite>(DialoguePath + readPath + dialoguePairs["img_1"]));
                break;
            case ShowMode.onlyText:
                break;
            case ShowMode.Tips:
                break;
        }
        return this;
    }

    /// <summary>
    /// 显示出来并开始播放
    /// </summary>
    /// <param name="nodeName"></param>
    public void ShowAndPlay(string nodeName)
    {
        if (dialogueData != null&& dialogueData.ContainsKey(nodeName))
        {
            Analysis(dialogueData[nodeName].sentence[0]);//从第一句开始
        }
        else
        {
            throw new Exception("未载入数据或没有找到节点");
        }
    }

    private void Analysis(string data)
    {
        if (data.Contains("["))
        {
            bool inBracket = false;
            string selects="";
            foreach (var s in data)
            {
                if (s.Equals("]"))
                {
                    inBracket = false;
                }
                if (inBracket)
                {
                    selects = selects + s;
                }
                if (s.Equals("["))
                {
                    inBracket = true;
                }
            }
            foreach (var s in selects.Split('|'))
            {   
                GameObject b= GameObject.Instantiate<GameObject>(selectExample);
                b.GetComponent<Text>().text = s;
                b.GetComponent<Button>().onClick.AddListener(delegate () { ShowAndPlay(dialogueData[b.GetComponent<Text>().text].title); });
                Transform sf = dialogueFrame.transform.Find("SelectFrame");
                if (sf != null)
                {
                    b.transform.SetParent(sf, false);
                }
            
            }

        }
        else
        {

        }
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
        Runner runner = new Runner();
        foreach (var l in ls)
        {
            if (!l.StartsWith("//"))
            {
                if (l.StartsWith("title:"))
                {
                    runner.title = l;
                    continue;
                }
                if (l.StartsWith("params:"))
                {
                    foreach (var param in l.Split('$'))
                    {
                        string[] pst = param.Split('=');
                        dialoguePairs.Add(pst[0].Trim(), pst[1].Trim());
                    }
                    continue;
                }
                runner.sentence.Add(l);
            }
        }
        dialogueData.Add(runner.title, runner);

    }

    private Dictionary<string,string> LoadXml(string xmlText)
    {
        xmlText.Insert(0, "<object>");

        xmlText.Insert(xmlText.Length, "</object>");

        //创建xml文档
        XmlDocument xml = new XmlDocument();

        xml.LoadXml(xmlText);

        //得到objects节点下的所有子节点
        XmlNodeList xmlNodeList = xml.SelectSingleNode("object").ChildNodes;

        Dictionary<string, string> listXmlElement = new Dictionary<string, string>();

        //遍历所有子节点
        foreach (XmlElement xl1 in xmlNodeList)
        {
            listXmlElement.Add(xl1.Name, xl1.Value);
           
        }

        return listXmlElement;


    }


    public class Runner
    {
        public List<string> sentence = new List<string>();

        public string title;


    }

    public enum ShowMode
    {
        full,
        twoSpeaker,
        onlyText,
        Tips
    }

}
