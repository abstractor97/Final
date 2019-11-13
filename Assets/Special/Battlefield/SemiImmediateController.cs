using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SemiImmediateController : MonoBehaviour
{
    public bool isStop;

    public int speed=2;//速度越快值越小

    public float sensitivetyKeyBoard = 0.1f;

    private PieceController[] allPiece;

    private float cameraSize;

    // Start is called before the first frame update
    void Start()
    {
        cameraSize = Camera.main.orthographicSize*2*100;
        StartCoroutine(TimeUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStop = !isStop;
            
        }

//        OffsetX = | (地图宽度 - 摄像机宽度) / 2 |
//        OffsetY = | (地图高度 - 摄像机高度) / 2 |
        if (Input.GetAxis("Horizontal") != 0)
        {
           Camera.main.transform.Translate(Input.GetAxis("Horizontal") * sensitivetyKeyBoard, 0, 0);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            Camera.main.transform.Translate(0, Input.GetAxis("Vertical") * sensitivetyKeyBoard, 0);
        }

    }



    public void ChangeSpeed(int speed)
    {
        this.speed = speed;
    }

    IEnumerator TimeUpdate() {
        while (true)
        {
            yield return new WaitForSeconds(0.1f * speed);
            if (!isStop)
            {
                foreach (var piece in allPiece)
                {
                    piece.Tree();
                }
            }
        }
    }

    public class Piece
    {
        public People1 people;

        public int id;

        public UnityAction Update;
    }

    public enum GearSpeed
    {
        stop,
        normal,
        fast,
    }
}
