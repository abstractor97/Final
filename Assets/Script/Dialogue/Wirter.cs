using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Wirter : MonoBehaviour
{
    /// <summary>
    /// 显示文字的间隔
    /// </summary>
    public float pause = 0.1f;
    /// <summary>
    /// 缓存数据条数
    /// </summary>
    public int cacheNum = 10;
    /// <summary>
    /// 是否保留之前的数据
    /// </summary>
    public bool isSave;
    /// <summary>
    /// 打字完成回调
    /// </summary>
    public UnityAction<string> wirterOver;

    private Queue<string> queue;
    private Text textUI;
    private string printText;//打印的字
    private bool isRun;


    public void AddQueue(string text)
    {
        if (queue==null)
        {
            queue = new Queue<string>(cacheNum);
            textUI = gameObject.GetComponent<Text>();
        }
        queue.Enqueue(text);
        if (!isRun)
        {
            isRun = true;
            StartCoroutine(TypeText());
        }
    }

    public void Clear()
    {
        textUI.text = "";
    }


    /**输出文本功能*/
    IEnumerator TypeText()
    {

        string cache = isSave ? textUI.text.ToString() : "";
        string word = queue.Dequeue();
        foreach (char letter in word.ToCharArray())
        {
            printText += letter;//把这些字赋值给Text
            yield return new WaitForSeconds(pause);
            textUI.text = cache + printText;
        }
        //  textUI.text = textUI.text + "\n";
        printText = "";
        wirterOver?.Invoke(textUI.text);
        yield return new WaitForSeconds(3 * pause);
        if (queue.Count > 0)
        {
            StartCoroutine(TypeText());
        }
        else {
            isRun = false;
        }
    }
}
