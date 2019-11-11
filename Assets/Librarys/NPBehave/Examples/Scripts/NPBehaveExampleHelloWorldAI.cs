using UnityEngine;
using NPBehave;
using System.Collections;

public class NPBehaveExampleHelloWorldAI : MonoBehaviour
{
    private Root behaviorTree;

    void Start()
    {
        behaviorTree = new Root(
            new Action(() => Debug.LogWarning("Hello World!"))
        );
        behaviorTree.Start();
    }

}
