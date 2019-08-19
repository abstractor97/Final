using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour
{
    public GameObject fullScreen;

    public string DialoguePath = "Assets/Story/";

    private Dictionary<string, string> dialogueData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitFullScreen(string text,Sprite sprite)
    {
        if (fullScreen.activeSelf)
        {

        }
        else
        {
            fullScreen= GameObject.Instantiate<GameObject>(fullScreen);
            fullScreen.GetComponent<Typewriter>().AddQueue(text);
            fullScreen.GetComponent<Image>().sprite = sprite;
        }
         
    }

    
    public void Submit(string dialogueName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(DialoguePath+dialogueName);
        dialogueData= LoadXml(textAsset.ToString());
    }

    public void Play(string nodeName)
    {
        if (dialogueData != null&& dialogueData.ContainsKey(nodeName))
        {
            Analysis(dialogueData[nodeName]);
        }
        else
        {
            throw new Exception("未载入数据或没有找到节点");
        }
    }

    public void Analysis(string data)
    {
        if (data.Contains("["))
        {

        }
        else
        {

        }
    }

    public void SubmitAsync(string dialogueName)
    {
        Resources.Load<TextAsset>(DialoguePath + dialogueName);
    }

    public Dictionary<string,string> LoadXml(string xmlText)
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

}
