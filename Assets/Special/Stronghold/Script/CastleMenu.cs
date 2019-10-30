using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CastleMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

      //  transform.DOScale(transform.position,1f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpLevel(Attribute attribute)
    {

    }

    public enum Attribute
    {
        str,
        agi,
        mint,
        end,

    }
}
