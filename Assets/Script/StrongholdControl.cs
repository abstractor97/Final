using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Stronghold
{
    public class StrongholdControl : MonoBehaviour
    {
        public GameObject map;
        public GameObject task;
        public GameObject cook;
        public GameObject readiness;

        public GameObject load;

        // Start is called before the first frame update
        void Start()
        {
            ProcessManager.Instance.dayTime.StartDay(this);
            ProcessManager.Instance.dayTime.ChangeSpeed(DayTime.TimeSpeed.wait);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ToMap()
        {
            FindObjectOfType<PublicManager>().Show(map);
        }

        public void Task()
        {
            FindObjectOfType<PublicManager>().Show(task);
        }

        public void Cook()
        {
            FindObjectOfType<PublicManager>().Show(cook);
        }
        public void Readiness()
        {
            FindObjectOfType<PublicManager>().Show(readiness);
        }
        public void Sleep()
        {
            FindObjectOfType<PublicManager>().ShowTimeDialog(JumpTime);
        }
        public void Leave()
        {
            StartCoroutine(RunMap());
        }

        IEnumerator RunMap()
        {
            load.GetComponentInChildren<Text>().text = "第" + ProcessManager.Instance.dayTime.day + "天";
            FindObjectOfType<PublicManager>().Show(load);
            AsyncOperation operation= SceneManager.LoadSceneAsync("MapScene");
            operation.allowSceneActivation = false;
            yield return new WaitForSeconds(2f);
            operation.allowSceneActivation = true;
        }

     

        public void JumpTime(string time)
        {
            ProcessManager.Instance.dayTime.JumpTime(time);
        }
    }

}

