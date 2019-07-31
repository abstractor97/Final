using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class MapPlayer : MonoBehaviour
    {
        [HideInInspector]
        public StupidAI ai;

        public GameObject clock;

        public DayTime dayTime;

        public State state;

        public Vehicles vehicles;
        /// <summary>
        /// 难度乘数
        /// </summary>
        public int DifficultyAmplifier = 1;
        // Start is called before the first frame update
        void Start()
        {            
            ai = gameObject.AddComponent<StupidAI>();
            ai.runCallBack += Run;

            dayTime = FindObjectOfType<DayTime>();
           // dayTime.StartDay(this);
            dayTime.callback += UpdateTime;
            gameObject.transform.position = new Vector3(ProcessManager.Instance.save.x, ProcessManager.Instance.save.y,0);
        }

        // Update is called once per frame
        void Update()
        {


        }

        public void Run()
        {
            dayTime.ChangeSpeed(DayTime.TimeSpeed.walk);
        }
        public void Stop()
        {
            dayTime.ChangeSpeed(DayTime.TimeSpeed.wait);
        }

        public void JumpTime(string time) {
            dayTime.JumpTime(time);
        }

        void UpdateTime(string time)
        {
            clock.GetComponentInChildren<Text>().text = time;
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
                return state.moveSpeed;
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
    }

}
