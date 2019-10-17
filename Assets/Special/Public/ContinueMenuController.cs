using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueMenuController: MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        foreach (var tr in transform)
        {
            GameObject item = ((Transform)tr).gameObject;
            if (item.name.Contains("Save"))
            {
                ListItem li= item.AddComponent<ListItem>();
                li.leftAction = delegate (int i) {
                    PublicManager.ShowArlog("读取存档", delegate (Pass pass)
                    {
                        if (pass==Pass.yes)
                        {
                            FindObjectOfType<ProcessManager>().RecoverySave();

                        }

                    });
                   // PublicManager.ToScene(this, "MapScene");
                  //  ProcessManager.Instance.StartGame
                };
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Close()
    {
        PublicManager.CloseUI(name);
    }

}
