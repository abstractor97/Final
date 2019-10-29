using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShadowFade : MonoBehaviour
{
    /// <summary>
    /// 图像显示或隐藏
    /// </summary>
    public bool inEffect = true;

    public bool showOrHide;

    public UnityAction<float> AniCallBack;
    private Material material;
    private float tick;
    private int factor;

    void Awake()
    {
        if (showOrHide)
        {

        }
        tick = -0.1f;
        factor = 1;
        material = GetComponent<Image>().material;
        if (inEffect)
            material.SetFloat("_InOut", 0);
        else
            material.SetFloat("_InOut", 1);
    }

    void Update()
    {
        tick += Time.deltaTime * factor;
        if (tick >= 1.5f)
        {
            tick = 1.5f;
            factor = -1;
            AniCallBack?.Invoke(tick);
        }
        else if (tick <= -0.1f)
        {
            tick = -0.1f;
            factor = 1;
            AniCallBack?.Invoke(tick);

        }
        material.SetFloat("_Offset", tick);
    }

}
