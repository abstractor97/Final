using NPBehave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{

    public People1 people;

    public PieceController target;

    public bool intercept;

    public Command command { set { behaviorTree.Blackboard["command"] = value; }
        get { return behaviorTree.Blackboard.Get<Command>("command"); } }

    private Blackboard sharedBlackboard;
    private Blackboard ownBlackboard;
    private Clock throttledClock;
    private Root behaviorTree;

    void Start()
    {
        // Node mainTree = new Service( () => { Debug.LogWarning("Test"); },
        //     new WaitUntilStopped()
        // );
        throttledClock = new Clock();
        //获取此类人工智能的共享黑板，此黑板由所有实例共享
        sharedBlackboard = UnityContext.GetSharedBlackboard("enemy_ai");
        ownBlackboard = new Blackboard(sharedBlackboard, throttledClock);
        command = Command.garrison;
        //     behaviorTree = new Root(new Blackboard(throttledClock), throttledClock, mainTree);
        behaviorTree = GetRoot();
        behaviorTree.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 每次执行树
    /// </summary>
    public void Tree()
    {
        UpdateState();
        throttledClock.Update(0f);
    }

    private Root GetRoot()
    {
        return new Root(ownBlackboard, throttledClock,
            //启动服务
            new Service(delegate () { },

                new Selector(

                    // 检查命令。
                    //当条件改变时，我们希望立即跳入或跳出此路径，因此使用IMMEDIATE_RESTART
                    new BlackboardCondition("command", Operator.IS_EQUAL, Command.garrison, Stops.IMMEDIATE_RESTART,
                        new Sequence(
                            // 判断任务结果返回FAILED跳出
                            new Action((bool _shouldCancel) =>
                            {
                                if (!_shouldCancel && !behaviorTree.Blackboard.Get<bool>("fight"))
                                {
                                    BuildFortification();
                                    return Action.Result.PROGRESS;
                                }
                                else
                                {
                                    return Action.Result.FAILED;
                                }
                            })
                            ,
                            //fight 
                             new Action(delegate (bool _shouldCancel)
                             {
                                 if (!_shouldCancel && behaviorTree.Blackboard.Get<bool>("fight"))
                                 {
                                     Attick();
                                     return Action.Result.PROGRESS;
                                 }
                                 else
                                 {
                                     return Action.Result.FAILED;
                                 }

                             })
                        )
                    ),
                    //突击
                      new BlackboardCondition("command", Operator.IS_EQUAL, Command.assault, Stops.IMMEDIATE_RESTART,
                        new Sequence(

                            new Action((bool _shouldCancel) =>
                            {
                                if (!_shouldCancel && !behaviorTree.Blackboard.Get<bool>("fight"))
                                {
                                    return Action.Result.PROGRESS;
                                }
                                else
                                {
                                    return Action.Result.FAILED;
                                }
                            })
                            { Label = "Follow" },
                             //fight 
                             new Action(delegate (bool _shouldCancel)
                             {
                                 if (!_shouldCancel && behaviorTree.Blackboard.Get<bool>("fight"))
                                 {
                                     Attick();
                                     return Action.Result.PROGRESS;
                                 }
                                 else
                                 {
                                     return Action.Result.FAILED;
                                 }

                             })
                        )
                    ),

                    // park until playerDistance does change
                    new Sequence(
                        new Action(delegate () { Debug.LogWarning("autonomy"); }) { Label = "Change to Gray" },
                        new WaitUntilStopped()
                    )
                )
            )
        );

    }

    private void UpdateState()
    {
        
    }

    private void BuildFortification()
    {

    }

    private void Move()
    {

    }

    private void Attick()
    {

    }

    private void Pursuit()
    {

    }

    public enum Command
    {
        garrison,
        assault,
        autonomy,
    }
}
