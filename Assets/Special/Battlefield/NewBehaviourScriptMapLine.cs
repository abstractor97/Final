using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScriptMapLine : MonoBehaviour
{
    float foundationSpeed = 1f;//基础速度
    float rotateSpeed = 0.1f;//基础速度倍率
    float mapTop = 50;
    float mapBottom = -50;
    float mapLeft = -50;
    float mapRight = 50;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Camera.main.transform.right + "__" + Camera.main.transform.up + ""
            + Camera.main.transform.position + "" + Camera.main.transform.localPosition);
        //相机移动
        moveMap();
    }


    public void moveMap()
    {
        float moveX = 0;//移动X
        float moveY = 0;//移动Y

        float X = Input.mousePosition.x;
        float Y = Input.mousePosition.y;
        float top = 0;
        float left = 0;
        float right = Screen.width;
        float bottom = Screen.height;
        //对X方向进行判断
        if (left < X && X < left + 50)//鼠标在左并由50的冗余
        {
            moveX = -foundationSpeed;
        }
        else if (right - 50 < X && X < right)//鼠标在右并由50的冗余
        {
            moveX = foundationSpeed;
        }
        else
        {
            //鼠标不在移动区域内时
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -foundationSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveX = foundationSpeed;
            }
            else
            {

                moveX = Input.GetAxis("Horizontal") * foundationSpeed;
            }

        }
        //对Y方向进行判断
        if (top < Y && Y < top + 50)
        {
            moveY = -foundationSpeed;
        }
        else if (bottom - 50 < Y && Y < bottom)
        {
            moveY = foundationSpeed;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveY = foundationSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveY = -foundationSpeed;
            }
            else
            {
                moveY = Input.GetAxis("Vertical") * foundationSpeed;
            }

        }

        if (moveX > 0)
        {
            if (Camera.main.transform.position.x >= mapRight)
            {
                moveX = 0;
            }
        }
        else if (moveX < 0)
        {
            if (Camera.main.transform.position.x <= mapLeft)
            {
                moveX = 0;
            }
        }
        if (moveY > 0)
        {
            if (Camera.main.transform.position.y >= mapTop)
            {
                moveY = 0;
            }
        }
        else if (moveY < 0)
        {
            if (Camera.main.transform.position.y <= mapBottom)
            {
                moveY = 0;
            }
        }

        //移动坐标不为0时进行移动
        if (moveX != 0 || moveY != 0)
        {
            Camera.main.transform.position = Camera.main.transform.position + (new Vector3(rotateSpeed * moveX, rotateSpeed * moveY, 0));
        }
    }

}
