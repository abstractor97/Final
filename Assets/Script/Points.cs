using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Points : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnOpen();
    }

    private void OnMouseEnter()
    {
        GameObject.FindObjectOfType<PublicManager>().ShowPointsUIThis(transform.position);
    }

    public abstract void OnOpen();
}
