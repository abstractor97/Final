using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public GameObject clock;

    public GameObject buffFrame;

    public State state;

    public Bag[] bags;

    public EquipBar equipBar;

    public Vehicles vehicles;
    /// <summary>
    /// 难度乘数
    /// </summary>
    public int DifficultyAmplifier = 1;

    [HideInInspector]
    public List<BuffControl> buffs;
    // Start is called before the first frame update
    void Start()
    {

        FindObjectOfType<DayTime>().callback += UpdateTime;
        foreach (var bag in bags)
        {
            bag.playerBag = true;
        }
        buffs = new List<BuffControl>();
    }

    // Update is called once per frame
    void Update()
    {


    }


    void UpdateTime(string time)
    {
        if (state.satiety < state.benchmarkSatiety)
        {
            state.hp -= state.hpEfflux;
        }
        state.power -= state.powerEfflux;
        state.satiety -= state.satietyEfflux;
        state.water -= state.waterEfflux;
        //额外状态
        foreach (var b in buffs)
        {
            b.Next(state);
        }
    }

    public float GetSpeed()
    {
        // y = xx + 10\left\{ y < 100\right\}\left\{ x > 0\right\}
        if (vehicles != null)
        {
            return vehicles.speed;
        }
        else
        {
            float speed = state.moveSpeed;
            foreach (var b in bags)
            {
                speed = speed * b.LoadSpeed();
            }
            return speed;
        }
    }
    /// <summary>
    /// 直接改变某个属性
    /// </summary>
    /// <param name=""></param>
    public void Attribute(StateTag tag,float value)
    {
        switch (tag)
        {
            case StateTag.m:
                state.moveSpeed += value;
                break;
            case StateTag.h:
                state.hp += value;
                break;
            case StateTag.p:
                state.power += value;
                break;
            case StateTag.s:
                state.satiety += value;
                break;
            case StateTag.w:
                state.water += value;
                break;
        }
    }

    public void Equip(Item item)
    {
        equipBar.Equip(item);
    }

    public void AddBuff(Buff buff)
    {
        foreach (var b in buffs)
        {
            if (b.ui.name.Equals(buff.name))
            {
                b.time += buff.totalTime;
                return;
            }
        }
        GameObject l = Resources.Load<GameObject>("UI/BuffLattice");
        l = GameObject.Instantiate<GameObject>(l);
        l.transform.SetParent(buffFrame.transform);
        l.GetComponent<Image>().sprite = buff.lowSprite;
        l.name = buff.name;
        ListItem li= l.AddComponent<ListItem>();
        li.panelPath = "UI/BuffTips";
        li.panelAction += delegate (GameObject panel) {
            Text[] texts= panel.GetComponentsInChildren<Text>();
            texts[0].text = buff.name;
            texts[1].text = buff.describe;
        };
        BuffControl bc = new BuffControl
        {
            time = buff.totalTime,
            ui = l,
            buffs = buffs,
            buff= buff
        };
        bc.Init(state);
    }



    public class BuffControl
    {
        public int time;

        public GameObject ui;

        public List<BuffControl> buffs;

        public Buff buff;

        public void Init(State state)
        {
            foreach (var b in buff.state)
            {
                if (b.tag==StateTag.m)
                {
                    state.moveSpeed += b.drgee;
                }
            }
            int hour = time / 60;
            int minus = time % 60;
            ui.transform.Find("Time").gameObject.GetComponent<Text>().text = hour + ":" + minus;

          
            buffs.Add(this);

        }


        public void Next(State state)
        {
            foreach (var b in buff.state)
            {
                switch (b.tag)
                {
                    case StateTag.h:
                        state.hp += b.drgee;
                        break;
                    case StateTag.p:
                        state.power += b.drgee;
                        break;
                    case StateTag.s:
                        state.satiety += b.drgee;
                        break;
                    case StateTag.w:
                        state.water += b.drgee;
                        break;
                }
            }        
            time--;
            if (time==0)
            {
                Remove(state);
            }
            else
            {
                int hour = time / 60;
                int minus = time % 60;
                ui.transform.Find("Time").gameObject.GetComponent<Text>().text = hour + ":" + minus;
            }
        }

        public void Remove(State state)
        {
            foreach (var b in buff.state)
            {
                if (b.tag == StateTag.m)
                {
                    state.moveSpeed -= b.drgee;
                }
            }
            Destroy(ui);
            buffs.Remove(this);
        }
    }

    [Serializable]
    public class State
    {
        public string other;
        public float hp;
        public float power;
        /// <summary>
        /// 体力消耗量
        /// </summary>
        public float powerLoop;


        public float moveSpeed;
        /// <summary>
        /// 重量
        /// </summary>
        public float weight;
        public float water;
        /// <summary>
        /// 饱腹度
        /// </summary>
        public float satiety;
        /// <summary>
        /// 基准饱腹度
        /// </summary>
        public float benchmarkSatiety;
        /// <summary>
        /// hp自然流逝率
        /// </summary>
        public float hpEfflux;
        public float powerEfflux;
        public float satietyEfflux;
        public float waterEfflux;
    }

    public enum StateTag
    {
        m,
        h,
        p,
        s,
        w
    }
}



