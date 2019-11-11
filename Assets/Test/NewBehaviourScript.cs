using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
/// <summary>
/// 浮动放置-黑
/// </summary>
public class NewBehaviourScript : MonoBehaviour
{
    private bool isMove;
    GameObject gameNew = null;
    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
    }
    private void OnMouseDown()
    {

        Debug.Log("This is OnMouseDown");
    }
    private void OnMouseUpAsButton()
    {
        if (isMove)
        {
            foreach (GameObject gameObjecttt in GameObject.FindGameObjectsWithTag("frame"))
            {
                if (gameObjecttt.Equals(this.gameNew))
                {
                    continue;
                }
                Vector3 vectorNewObjectBoundsSize = gameNew.GetComponent<SpriteRenderer>().sprite.bounds.size;
                Vector3 vectorNewObjectPosition = gameNew.transform.position;
                Bounds gameObjecetttBounds = gameObjecttt.GetComponent<SpriteRenderer>().sprite.bounds;
                Vector3 vectorNewObjectVru = new Vector3(vectorNewObjectPosition.x + (vectorNewObjectBoundsSize.x / 2), vectorNewObjectPosition.y + (vectorNewObjectBoundsSize.y / 2));
                Vector3 vectorNewObjectVrb = new Vector3(vectorNewObjectPosition.x + (vectorNewObjectBoundsSize.x / 2), vectorNewObjectPosition.y - (vectorNewObjectBoundsSize.y / 2));
                Vector3 vectorNewObjectVlu = new Vector3(vectorNewObjectPosition.x - (vectorNewObjectBoundsSize.x / 2), vectorNewObjectPosition.y + (vectorNewObjectBoundsSize.y / 2));
                Vector3 vectorNewObjectVlb = new Vector3(vectorNewObjectPosition.x - (vectorNewObjectBoundsSize.x / 2), vectorNewObjectPosition.y - (vectorNewObjectBoundsSize.y / 2));
                Debug.Log("gameObjecetttBounds" + gameObjecetttBounds);
                Debug.Log("gameObjecetttBounds" + gameObjecttt.transform.position);
                Debug.Log("vectorNewObjectVru" + vectorNewObjectVru);
                Debug.Log("vectorNewObjectVrb" + vectorNewObjectVrb);
                Debug.Log("vectorNewObjectVlu" + vectorNewObjectVlu);
                Debug.Log("vectorNewObjectVlb" + vectorNewObjectVlb);
                if (gameObjecetttBounds.Contains(vectorNewObjectVru))
                {
                    return;
                }
                if (gameObjecetttBounds.Contains(vectorNewObjectVrb))
                {
                  //  return;
                }
                if (gameObjecetttBounds.Contains(vectorNewObjectVlu))
                {
                   // return;
                }
                if (gameObjecetttBounds.Contains(vectorNewObjectVlb))
                {
                  //  return;
                }

                GameObject.Destroy(gameObjecttt);
            }
            GameObject.Destroy(gameNew);
            transform.DOLocalMoveZ(0, 2);
        }
        else
        {
            foreach (GameObject gameObjecttt in GameObject.FindGameObjectsWithTag("goods"))
            {
                if (gameObjecttt.Equals(this.gameObject))
                {
                    continue;
                }
                GameObject gameObjectFrame = new GameObject("123")
                {
                    tag = "frame"
                };
                Vector3 gamePositionFrame = gameObjecttt.transform.position;
                gamePositionFrame.y = gamePositionFrame.y - 2;
                gameObjectFrame.transform.position = gamePositionFrame;
                SpriteRenderer spriteRendererFrame = gameObjectFrame.AddComponent<SpriteRenderer>();
                spriteRendererFrame.sprite = Resources.Load("icon_3", typeof(Sprite)) as Sprite;
            }
            gameNew = new GameObject("123");
            gameNew.tag = "frame";
            Vector3 gamePosition = transform.position;
            gamePosition.y = gamePosition.y - 2;
            gameNew.transform.position = gamePosition;
            SpriteRenderer spriteRenderer = gameNew.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load("icon_3", typeof(Sprite)) as Sprite;
            transform.DOLocalMoveZ(-1, 2);
        }
        isMove = !isMove;
    }
    private void OnMouseUp()
    {
    }
    private void OnMouseOver()
    {
        if (isMove)
        {
            MouseFollow();
        }
    }
    Vector3 screenPosition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionInWorld;
    void Update()
    {
    }
    void MouseFollow()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenPosition.z;
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        transform.position = mousePositionInWorld;
        mousePositionInWorld.y = mousePositionInWorld.y - 2;
        mousePositionInWorld.z = 0;
        gameNew.transform.position = mousePositionInWorld;
    }
}
