using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    [HideInInspector]
    public int roundNumber { get; private set; }
    public int maxSize=100;
    /// <summary>
    /// 信息版
    /// </summary>
    public Typewriter typewriter;
    /// <summary>
    /// 前后的enemy ui
    /// </summary>
    public Image ahead;
    public Image rear;
    /// <summary>
    /// 最大信息条数
    /// </summary>
    public int maxPlayerAction=1;
    public int playPos;
    /// <summary>
    /// 目标下标
    /// </summary>
    [HideInInspector]
    public int rightIndex=1;
    List<RoundPlayer> peoples = new List<RoundPlayer>();
    [HideInInspector]
    public float distance;
    private bool lockRound;
    private Queue<UnityAction> queue;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<UnityAction>();
        typewriter.lineCallBack = delegate (string text)
        {
            if (text.Equals("-----"))
            {
                Next();
            }
        };
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Forward()
    {
        rightIndex--;
        if (rightIndex==0)
        {
            rightIndex = peoples.Count - 1;
        }
        rear.sprite = peoples[rightIndex].sprite;
        if (rightIndex==peoples.Count-1)
        {
            ahead.sprite = peoples[1].sprite;
        }
        else
        {
            ahead.sprite = peoples[rightIndex+1].sprite;
        }
        rear.transform.DOMove(new Vector3(6, 6, 0), 0.8f).SetRelative();
        ahead.transform.DOMove(new Vector3(6, -6, 0), 0.8f).SetRelative();
        //todo 切换
    }

    public void Backward()
    {
        rightIndex++;
        if (rightIndex==peoples.Count)
        {
            rightIndex = 1;
        }

    }
   

    public void AddPlayerRight(People people)
    {
        if (peoples.Count==0)
        {
            //todo 添加player
        }
        peoples.Add(new RoundPlayer
        {
            name = people.name,
            posture = Posture.stand,
            controller=this,
        });
    }

    public void Attick(int pos,int tagre) {
        queue.Enqueue(delegate () {
 
            typewriter.AddQueue(peoples[pos].name + ProcessManager.Instance.language.Text("攻击")+ peoples[tagre].name);
            typewriter.AddQueue("-----");
        });
    }

    public void Move(int pos, int drg) {
        queue.Enqueue(delegate () {
            if (drg>0)
            {
                typewriter.AddQueue(peoples[pos].name + ProcessManager.Instance.language.Text("前进")+drg);
            }
            else
            {
                typewriter.AddQueue(peoples[pos].name + ProcessManager.Instance.language.Text("逃跑") + drg);
            }

            typewriter.AddQueue("-----");
        });
    }

    public void CAction(InputActionButton input)
    {     

        if (!lockRound)
        {
            switch (input.ia)
            {
                case InputAction.att:
                    Attick(0, rightIndex);
                    break;
                case InputAction.move:
                    Move(0, input.move);
                    break;
                case InputAction.hide:
                    break;

            }
            if (queue.Count== maxPlayerAction)
            {
                lockRound = true;
                StartRound();
            }
        }
    }

    /// <summary>
    /// 锁定指令，开始运行
    /// </summary>
    public void StartRound()
    {
        lockRound = true;

        if (peoples.Count==2)
        {
            rear.gameObject.SetActive(false);
        }
        else
        {
            rear.gameObject.SetActive(true);

        }
        Next();
    }

    private void Next()
    {
        if (queue.Count > 0)
        {
            queue.Dequeue().Invoke();

        }
        else
        {
            //todo 队列执行完成
            lockRound = false;
        }

    }



    public class RoundPlayer
    {
        public string name;
        public Sprite sprite;

        public Posture posture;

        public RoundController controller;
    }

    public enum Posture
    {
        stand,
        liedown,
    }
    public enum InputAction
    {
        att,
        move,
         hide,
    }
}
