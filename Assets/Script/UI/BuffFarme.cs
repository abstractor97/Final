using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuffFarme:MonoBehaviour
{

    public GameObject latt;

    public GameObject note;

    public UnityAction<Buff> remove;

    private void Start()
    {
        
    }

    public void AddBuff(Buff buff)
    {
       GameObject l=  GameObject.Instantiate<GameObject>(latt);
        l.transform.SetParent(gameObject.transform);
        StartCoroutine(Time(l, buff, buff.totalTime));
        l.name = buff.name;


    }
    public void AddBuffOnlyUI(Buff buff)
    {
        GameObject l = GameObject.Instantiate<GameObject>(latt);
        l.transform.SetParent(gameObject.transform);
        l.GetComponent<Image>().sprite = buff.lowSprite;
        l.name = buff.name;
        
    }

    public void RemoveBuff(string name)
    {
        Destroy(gameObject.transform.Find(name).gameObject);
      //  remove(buff);
    }

    IEnumerator Time(GameObject obj ,Buff buff, float time)
    {
        while (time>0)
        {
            obj.GetComponent<Text>().text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }
        Destroy(obj);
        remove(buff);
    }
}
