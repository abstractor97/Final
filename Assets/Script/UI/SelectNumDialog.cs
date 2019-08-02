using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectNumDialog : MonoBehaviour
{
    public int max=100;
    public int min = 0;
    private int num;
    public UnityAction<int> action;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = min.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add() {
        if (num<max)
        {
            num++;
            text.text = num.ToString();
        }
       
    }

    public void Reduce()
    {
        if (num> min)
        {
            num--;
            text.text = num.ToString();
        }
    }

   
    public void Enter()
    {
        action(num);
    }
}
