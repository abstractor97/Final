using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrongholdController : MonoBehaviour
{
    public RectTransform SH_HUD;

    public RectTransform BigMap;

    public RectTransform MaskLoad;

    private void Awake()
    {
        PublicManager.HUD = SH_HUD;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PublicManager.IsShow(PublicManager.pauseMenu.name))
            {
                PublicManager.ShowPauseMenu();
            }
        }
    }

   public void Leave()
    {
        //PublicManager.ShowArlog("确定出发",delegate(Pass pass) {
        //    if (pass==Pass.yes)
        //    {
        //        PublicManager.ToScene(this, "MapScene");
        //    }
        //});
        PublicManager.Show(BigMap.gameObject);
    }

    public void MapToHold()
    {
        PublicManager.Hide(BigMap.gameObject);
    }

  
}
