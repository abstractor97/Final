using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity.Example;

public class PlayerManager : MonoBehaviour
{

    public GameObject clock;

    public State state;

    public Bag[] bags;

    public Vehicles vehicles;
    /// <summary>
    /// 难度乘数
    /// </summary>
    public int DifficultyAmplifier = 1;


    public NPC talkTo; 
    // Start is called before the first frame update
    void Start()
    {

        // dayTime.StartDay(this);
        FindObjectOfType<DayTime>().callback += UpdateTime;
        gameObject.transform.position = new Vector3(ProcessManager.Instance.save.x, ProcessManager.Instance.save.y, 0);
        state = ProcessManager.Instance.cacheState;
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

    public void AddBuff(Buff buff)
    {
        foreach (var s in buff.state)
        {
            switch (s.type)
            {
                case Buff.BuffType.speed:
                    state.moveSpeed *= s.drgee;
                    break;
                case Buff.BuffType.state:
                    ChangeState(s.tag, s.drgee, buff.totalTime);
                    break;
            }
        }
        
        FindObjectOfType<BuffFarme>().AddBuff(buff);
    }

    public void ChangeState(StateTag tag,float drgee,float time)
    {
        StartCoroutine(RunChangeState(tag,drgee,time));
    }

    IEnumerator RunChangeState(StateTag tag, float drgee, float time)
    {
        while (time>0)
        {
            switch (tag)
            {
                case StateTag.h:
                    state.hp += drgee;
                    break;
                case StateTag.p:
                    state.power += drgee;
                    break;
                case StateTag.s:
                    state.satiety += drgee;
                    break;
                case StateTag.w:
                    state.water += drgee;
                    break;
            }
            yield return new WaitForSeconds(1f);
            time--;
        }
       
    }

    [System.Serializable]
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
        h,
        p,
        s,
        w
    }
}



