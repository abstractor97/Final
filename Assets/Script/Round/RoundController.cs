using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    [HideInInspector]
    public int roundNumber { get; private set; }
    public int maxSize=100;
    /// <summary>
    /// 信息版
    /// </summary>
    public Typewriter typewriter;
    public int playPos;
    public int rightIndex;
    List<RoundPlayer> right = new List<RoundPlayer>();
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

   

    public void AddPlayerRight()
    {
        right.Add(new RoundPlayer
        {
            posture = Posture.stand,
            controller=this,
        });
    }

    public void Attick(int pos,int tagre) {
        queue.Enqueue(delegate () {

        });
    }

    public void Move(int pos, int drg) { }

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

        }
    }

    /// <summary>
    /// 锁定指令，开始运行
    /// </summary>
    public void StartRound()
    {
        lockRound = true;


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
