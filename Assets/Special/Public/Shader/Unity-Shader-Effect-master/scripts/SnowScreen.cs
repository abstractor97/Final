﻿using UnityEngine;

[ExecuteInEditMode]
    public Texture2D SnowTexture;

    public Color SnowColor = Color.white;

    public float SnowTextureScale = 0.1f;

    [Range(0, 1)]

    public Material _Material;

    void OnEnable()

        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // set shader properties
        _Material.SetMatrix("_CamToWorld", GetComponent<Camera>().cameraToWorldMatrix);//摄像机到世界坐标系的转换矩阵
        _Material.SetColor("_SnowColor", SnowColor);//雪的颜色

        Graphics.Blit(src, dest, _Material);