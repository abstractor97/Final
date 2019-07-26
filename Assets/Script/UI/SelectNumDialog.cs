using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectNumDialog : MonoBehaviour
{
    public int max=100;
    private int num;
    public UnityAction<int> action;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add() {
        if (num<max)
        {
            num++;
        }
       
    }

    public void Reduce()
    {
        if (num>0)
        {
            num--;
        }
    }

   
    public void Enter()
    {
        action(num);
    }
}
