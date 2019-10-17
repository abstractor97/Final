using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDotAni : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Button>().onClick.AddListener(delegate () {
            Tween tween = transform.DOLocalMoveX(600, 2f);
            tween.SetEase(Ease.OutBounce);
            tween.OnComplete(delegate () {
                //  tween.PlayBackwards();
            });
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
