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
    public Typewriter playerWriter;
    /// <summary>
    /// 左右的 ui
    /// </summary>
    public Image[] lefts;
    public Image[] rights;
    /// <summary>
    /// 第几个输入指令
    /// </summary>
    private int leftIndex;
    /// <summary>
    /// 最大信息条数
    /// </summary>
    public int maxPlayerAction=1;
    /// <summary>
    /// 是否允许接受指令
    /// </summary>
    private bool allowAction;
    /// <summary>
    /// 最大对方信息条数
    /// </summary>
    private int maxEnemyAction = 1;
    /// <summary>
    /// 选中的目标下标
    /// </summary>
    [HideInInspector]
    public int rightIndex=1;
    private List<Teammate> players = new List<Teammate>();
    private List<Teammate> enemys = new List<Teammate>();
    /// <summary>
    /// 距离需传入
    /// </summary>
    [HideInInspector]
    public float distance;
    private bool lockRound;
    private Queue<UnityAction> queue;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<UnityAction>();
        for (int i = 0; i < lefts.Length; i++)
        {
            lefts[i].sprite = players[i].sprite;
        }
        for (int i = 0; i < rights.Length; i++)
        {
            rights[i].sprite = enemys[i].sprite;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    /// <summary>
    /// 屏幕左边队列添加
    /// </summary>
    /// <param name="people"></param>
    public void AddPlayerLeft(People people,bool controlled)
    {
        players.Add(new Teammate
        {
            name = people.name,
            posture = Posture.stand,
            people = people,
             controlled= controlled,
        });
    }


    public void AddPlayerRight(People people)
    {
        enemys.Add(new Teammate
        {
            name = people.name,
            posture = Posture.stand,
            people = people,
           // controller=this,
        });
    }

    public void Attick(int pos,int tagre) {
        queue.Enqueue(delegate () {

            //todo 击中几率
            float hit = 0f;
            string describe = "";
            if (distance <= enemys[pos].people.equipped.arm.equip.range)
            {
                float totalProtect = enemys[tagre].people.equipped.head.equip.protect + enemys[tagre].people.equipped.body.equip.protect + enemys[tagre].people.equipped.shot.equip.protect;
                float xs = enemys[pos].people.attribute.str / enemys[pos].people.equipped.arm.equip.needStr;
                if (xs > 2)
                {
                    xs = 2;
                }
                float totalAttack = enemys[pos].people.equipped.arm.equip.attack * xs + enemys[pos].people.attribute.str;
                hit = totalAttack - totalProtect;
                enemys[tagre].people.state.hp -= hit;
                if (hit == 0)
                {
                    describe = "";
                }
                if (hit > 0 && hit < 10)
                {
                    describe = "";
                }
                if (hit >= 10 && hit < 30)
                {
                    describe = "";
                }
                if (hit >= 30 && hit < 70)
                {
                    describe = "";
                }
                if (hit >= 70)
                {
                    describe = "";
                }

            }
            else
            {
                describe = "距离太远";
            }
            //if (pos==0)
            //{
            //    playerWriter.AddQueue(enemys[pos].name + ProcessManager.Instance.language.Text("攻击") + enemys[tagre].name);
            //    playerWriter.AddQueue(ProcessManager.Instance.language.Text(describe));
            //    playerWriter.AddQueue("-----");
            //}
            //else
            //{
            //    enemyWriter.AddQueue(enemys[pos].name + ProcessManager.Instance.language.Text("攻击") + enemys[tagre].name);
            //    enemyWriter.AddQueue(ProcessManager.Instance.language.Text(describe));
            //    enemyWriter.AddQueue("-----");
            //}          
        });
        //  totalAttack
    }


    public void Defense(int pos)
    {

    }

    public void Move(int pos, float drg) {

        queue.Enqueue(delegate () {
            string describe = "";
            if (drg>0)
            {
                describe = "前进";
            }
            else
            {
                describe = "逃跑";
            }

            //if (pos == 0)
            //{
            //    playerWriter.AddQueue(ProcessManager.Instance.language.Text(describe) + drg);
            //    playerWriter.AddQueue("-----");
            //}
            //else
            //{
            //    enemyWriter.AddQueue(ProcessManager.Instance.language.Text(describe) + drg);
            //    enemyWriter.AddQueue("-----");
            //}
            distance += drg;
        });


    }

    public void CreateAction(InputActionButton input)
    {     

        if (!lockRound)
        {
            switch (input.ia)
            {
                case InputAction.att:
                    Attick(leftIndex, rightIndex);
                    break;
                case InputAction.move:
                    Move(leftIndex, input.move);
                    break;
                case InputAction.hide:
                    break;

            }
            if (queue.Count== maxPlayerAction){
                leftIndex++;
                if (leftIndex<lefts.Length-1)
                {
                    //todo 切换
                }
                else
                {
                    StartRound();
                }
            }
        }
    }

    /// <summary>
    /// 锁定指令，开始运行
    /// </summary>
    public void StartRound()
    {
        lockRound = true;
        CreateAiAction();
        Next();
    }
    /// <summary>
    /// 创建ai的指令
    /// </summary>
    private void CreateAiAction()
    {

    }

    private void TestFightAni()
    {
        Tween tween = transform.DOLocalMoveX(300, 0.5f);
        tween.OnComplete(delegate() {
            tween.PlayBackwards();
        });
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


    public class Teammate
    {
        public string name;
        public Sprite sprite;

        public Posture posture;

        public People people;

        public bool controlled;
    }

    public enum Posture
    {
        none,
        stand,
        liedown,
        crouch,
    }
    public enum InputAction
    {
        att,
        move,
         hide,
         take
    }
}
