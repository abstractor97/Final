using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    public int x;
    public int y;

    public PieceController controller;

    public People1 people;

    public bool isEnemy;

    // Start is called before the first frame update
    void Start()
    {
        controller=new PieceController(people,isEnemy);
        controller.Start();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        controller.Collision(collision.gameObject.tag);
    }
}
