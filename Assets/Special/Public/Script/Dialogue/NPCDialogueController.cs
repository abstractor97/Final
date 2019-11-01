using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueController : MonoBehaviour
{
    public string runNode;

    public TextAsset story;

    public Dialogue.Mode mode=Dialogue.Mode.cover;

    private Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        dialogue = FindObjectOfType<Dialogue>();
        if (!dialogue.hasText)//解释器文本未到最后一个node,也就是播放中状态
        {
            dialogue.Load(story).Show(mode).Play(runNode);
        }
        
    }

    /// <summary>
    /// 托管参数，托管后参数会受到剧本选项等影响
    /// </summary>
    public void TrustParam(string pName, string pValue)
    {
        dialogue.TrustParam(pName,pValue);
    }

    /// <summary>
    /// 取出某一个托管参数,在装载新数据之前取出
    /// </summary>
    public string GetTrustParam(string pName)
    {
       return dialogue.GetTrustParam(pName);
    }
}
