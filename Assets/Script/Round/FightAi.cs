using NPBehave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAi : MonoBehaviour
{
    public RoundController roundController;
    /// <summary>
    /// 引用的人物状态
    /// </summary>
    public People people;
    /// <summary>
    /// 引用的player状态
    /// </summary>
    private PlayerManager playerManager;
    private Blackboard blackboard;
    private Root behaviorTree;

    void Start()
    {
        // create our behaviour tree and get it's blackboard
        behaviorTree = CreateBehaviourTree();
        blackboard = behaviorTree.Blackboard;
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        Init();

        // attach the debugger component if executed in editor (helps to debug in the inspector) 
#if UNITY_EDITOR
        Debugger debugger = (Debugger)this.gameObject.AddComponent(typeof(Debugger));
        debugger.BehaviorTree = behaviorTree;
#endif

        // start the behaviour tree
        behaviorTree.Start();
    }

    private Root CreateBehaviourTree()
    {
        // we always need a root node
        return new Root(

            //启动服务，每125毫秒更新一次“playerdistance”和“playerlocalpos”黑板值
            new Service(0.125f, UpdatePlayerDistance,

                new Selector(

                    // 检查“playerdistance”黑板值。
                    //当条件改变时，我们希望立即跳入或跳出此路径，因此使用IMMEDIATE_RESTART
                    new BlackboardCondition("playerDistance", Operator.IS_SMALLER, 7.5f, Stops.IMMEDIATE_RESTART,

                        // the player is in our range of 7.5f
                        new Sequence(

                            // set color to 'red'
                            new Action(() => SetColor(Color.red)) { Label = "Change to Red" },

                            // 向玩家靠近，直到playerdistance大于7.5（在这种情况下，应该取消将变为真）
                            new Action((bool _shouldCancel) =>
                            {
                                if (!_shouldCancel)
                                {
                                    MoveTowards(blackboard.Get<Vector3>("playerLocalPos"));
                                    return Action.Result.PROGRESS;
                                }
                                else
                                {
                                    return Action.Result.FAILED;
                                }
                            })
                            { Label = "Follow" }
                        )
                    ),

                    // park until playerDistance does change
                    new Sequence(
                        new Action(() => SetColor(Color.grey)) { Label = "Change to Gray" },
                        new WaitUntilStopped()
                    )
                )
            )
        );
    }

    private void UpdatePlayerDistance()
    {
        if (people.state.hp<50) {

        }
    
        
        //Vector3 playerLocalPos = this.transform.InverseTransformPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
        //behaviorTree.Blackboard["playerLocalPos"] = playerLocalPos;
        //behaviorTree.Blackboard["playerDistance"] = playerLocalPos.magnitude;
    }


    private void Init()
    {
        if (playerManager.state.state.lv > people.state.lv * 2)
        {

        }
        if (playerManager.state.state.lv < people.state.lv / 2)
        {

        }
    }

    private void MoveTowards(Vector3 localPosition)
    {
        transform.localPosition += localPosition * 0.5f * Time.deltaTime;
    }

    private void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
}
