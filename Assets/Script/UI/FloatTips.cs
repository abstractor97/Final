using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatTips : MonoBehaviour
{
    public RectTransform hud;

    public GameObject item;
    /// <summary>
    /// 出现坐标
    /// </summary>
    public float x;
    public float y;
    /// <summary>
    /// 上移速度
    /// </summary>
    public float speed;

    public float moveTime=1f;

    private float waitTime = 0.5f;

    private Queue<Tip> tips;

    private IEnumerator up;

    private int tjump;

    private bool canAdd=true;

    private int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        tips = new Queue<Tip>();
        tjump = (int)(item.GetComponent<RectTransform>().sizeDelta.y/speed)+1;
        waitTime = tjump * 0.1f+0.3f;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void AddTips(Sprite sprite,string tip)
    {
       
   
        tips.Enqueue(new Tip {sprite=sprite,tip=tip });
        if (up==null)
        {
            up = Up();
            StartCoroutine(up);
        }     
    }

    IEnumerator Up()
    {
        while (tips.Count>0)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < tips.Count; i++)
            {
                Tip t = tips.Dequeue();
                if (t.time >= moveTime + waitTime)
                {
                    Destroy(t.ui);
                    continue;
                }

                if (t.ui==null)
                {
                    if (canAdd)
                    {
                        GameObject o = GameObject.Instantiate<GameObject>(item);
                        o.transform.SetParent(hud, false);
                        o.transform.position = new Vector3(x, y, 0);
                        if (t.sprite != null)
                        {
                            o.GetComponentInChildren<Image>().sprite = t.sprite;
                        }
                        else
                        {
                            o.GetComponentInChildren<Image>().gameObject.SetActive(false);
                        }
                        o.GetComponentInChildren<Text>().text = t.tip;
                        t.ui = o;
                        canAdd = false;
                        tips.Enqueue(t);

                    }
                    else
                    {
                        tips.Enqueue(t);
                        continue;
                    }

                }
                t.time += 0.1f;               
                if (t.time >= moveTime)
                {
                    tips.Enqueue(t);
                    continue;
                }
                if (t.time>=moveTime*0.7)
                {
                    t.ui.transform.position = t.ui.transform.position + new Vector3(0, speed*0.7f);
                    tips.Enqueue(t);
                    continue;
                }
                t.ui.transform.position = t.ui.transform.position + new Vector3(0, speed);
                tips.Enqueue(t);
            }
            jumpCount++;
            if (jumpCount== tjump)
            {
                canAdd = true;
                jumpCount = 0;
            }
        }
        up = null;
      
    }

    public class Tip
    {
        public Sprite sprite;
        public string tip;
        public GameObject ui;
        public float time;

    }
}
