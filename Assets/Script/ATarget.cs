using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATarget : MonoBehaviour
{
    public LayerMask mask; 
    public IAstarAI ai;
    public bool use2D;



    public void Start()
    {

        // Slightly inefficient way of finding all AIs, but this is just an example script, so it doesn't matter much.
        // FindObjectsOfType does not support interfaces unfortunately.
       // ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
        useGUILayout = true;
    }

    public void OnGUI()
    {
        if ( Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
        {
         //   UpdateTargetPosition();
        }
    }

    /// <summary>Update is called once per frame</summary>
    void Update()
    {
      
    }

    public void WalkThis(Vector3 position)
    {
        UpdateTargetPosition(position);
    }

    private void UpdateTargetPosition(Vector3 position)
    {
        Vector3 newPosition = Vector3.zero;
        bool positionFound = false;

        if (use2D)
        {
            newPosition = Camera.main.ScreenToWorldPoint(position);
            newPosition.z = 0;
            positionFound = true;
        }
        else
        {
            // Fire a ray through the scene at the mouse position and place the target where it hits
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(position), out hit, Mathf.Infinity, mask))
            {
                newPosition = hit.point;
                positionFound = true;
            }
        }

        if (positionFound && newPosition != gameObject.transform.position)
        {
            gameObject.transform.position = newPosition;
            ai.destination = newPosition;
            ai.SearchPath();
        }
    }
}
