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
        dialogue= FindObjectOfType<Dialogue>().Load(story).Show(mode).Play(runNode);
    }
}
