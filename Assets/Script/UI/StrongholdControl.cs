using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StrongholdControl : MonoBehaviour
{
    public GameObject map;
    public GameObject task;
    public GameObject cook;
    public GameObject readiness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMap() {
        Show(map);
    }

    public void Task()
    {
        Show(task);
    }

    public void Cook()
    {
        Show(cook);
    }
    public void Readiness()
    {
        Show(readiness);
    }
    public void Sleep()
    {

    }
    public void Leave()
    {
        SceneManager.LoadScene("MapScene", LoadSceneMode.Additive);
    }

    private void Show(GameObject ui) {
        CanvasGroup group = ui.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }
}
