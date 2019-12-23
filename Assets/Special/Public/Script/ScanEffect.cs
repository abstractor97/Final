using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanEffect : MonoBehaviour
{
    //默认扫描线的宽
    [Range(0, 1)]
    public float _defaultLineW = 0.2f;
    //扫描的速度
    [Range(0, 1)]
    public float _showSpeed = 0.02f;

    private MeshRenderer _render;

    private void Awake()
    {
        _render = GetComponent<MeshRenderer>();
        SetX(0);
        SetLineWidth(0);
    }

    public void SetLineWidth(float val)
    {
        _render.material.SetFloat("_lineWidth", val);
    }
    public void SetX(float val)
    {
        _render.material.SetFloat("_rangeX", val);
    }

    public void Show()
    {
        StopCoroutine("Showing");
        StartCoroutine("Showing");
    }
    public void Hide()
    {
        StopCoroutine("Showing");

        SetX(0);
        SetLineWidth(0);
    }

    private IEnumerator Showing()
    {
        float deltaX = 0;
        float deltaWidth = _defaultLineW;

        SetX(deltaX);
        SetLineWidth(deltaWidth);

        while (true)
        {
            if (deltaX != 1)
            {
                deltaX = Mathf.Clamp01(deltaX + _showSpeed);
                SetX(deltaX);
            }
            else
            {
                if (deltaWidth != 0)
                {
                    deltaWidth = Mathf.Clamp01(deltaWidth - _showSpeed);
                    SetLineWidth(deltaWidth);
                }
                else
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }


    public void OnGUI()
    {
        if (GUILayout.Button("Show"))
        {
            Show();
        }
        if (GUILayout.Button("Hide"))
        {
            Hide();
        }
    }
}
