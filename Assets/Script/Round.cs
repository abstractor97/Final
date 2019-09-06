using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Round : MonoBehaviour
{
  
    [HideInInspector]
    public bool isRun{ get; private set; }
    [HideInInspector]
    public float distance;

    private bool lockRound;

    private Queue<RoundAction> queue;

    public UnityAction roundOver;
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<RoundAction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddAction(RoundAction action) {

        if (!lockRound)
        {
            action.finishAction = delegate () { Next(); };
            queue.Enqueue(action);

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
        if (queue.Count>0)
        {
            RoundAction action = queue.Dequeue();
            action.startAction.Invoke();
           
        }
        else
        {
            //todo 队列执行完成
            roundOver?.Invoke();
            lockRound = false;
        }
     
    }

    public class RoundAction
    {
        public RoundType type;


        public UnityAction startAction;
        /// <summary>
        /// 延时手动调用
        /// </summary>
        public UnityAction finishAction;


        
    }

    public enum RoundType
    {
        move,
        att,
        take,
    }
}
