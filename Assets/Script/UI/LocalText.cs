﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("文本增强/本地化文本")]
public class LocalText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = gameObject.GetComponent<Text>();
#if UNITY_EDITOR

#else
         text.text = ProcessManager.language.Text(text.text);
#endif

        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
