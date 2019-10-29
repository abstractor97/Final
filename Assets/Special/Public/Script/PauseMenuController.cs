using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Image saveTip;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 0;//暂停
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        Time.timeScale = 1;

    }
    public void RecoveryGame()
    {
        PublicManager.CloseUI(name);
    }

    public void SaveGame()
    {
        //齿轮旋转动画
        saveTip.transform.DORotate(new Vector3(360, 0, 0), 2);
        FindObjectOfType<ProcessManager>().CreateSaveData();
    }

    public void LoadMenu()
    {
        PublicManager.ShowSaveMenu();
    }

    public void SetMenu()
    {
        PublicManager.ShowSetMenu();

    }

    public void BackMainMenu()
    {
        PublicManager.ToScene(this, "StartMenuScene");
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
