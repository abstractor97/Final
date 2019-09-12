using UnityEngine;
using NPBehave;

/// <summary>
///此示例显示如何使用时钟实例来完全控制树接收更新的方式。
///例如，这允许您限制对远离播放器的ai实例的更新。
///如果愿意，还可以通过多个树共享时钟实例。
/// </summary>
public class NPBehaveExampleClockThrottling : MonoBehaviour
{
    // tweak this value to control how often your tree is ticked
    public float updateFrequency = 1.0f; // 1.0f = every second

    private Clock myThrottledClock;
    private Root behaviorTree;
    private float accumulator = 0.0f;

    void Start()
    {
        Node mainTree = new Service(() => { Debug.Log("Test"); },
            new WaitUntilStopped()
        );
        myThrottledClock = new Clock();
        behaviorTree = new Root(new Blackboard(myThrottledClock), myThrottledClock, mainTree);
        behaviorTree.Start();
    }

    void Update()
    {
        accumulator += Time.deltaTime;
        if (accumulator > updateFrequency)
        {
            accumulator -= updateFrequency;
            myThrottledClock.Update(updateFrequency);
        }
    }
}
