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

        public float weight = 55f;
        public float Hp;
        public float power;
        public float speedMultiplier;
        public float moveSpeed = 0.1f;
        public float v;
        public float satiety;
        public float benchmarkSatiety;
        public float hpEfflux;
        public float powerEfflux;
        public float satietyEfflux;
        public State state;

        public Vehicles vehicles;

        public int DifficultyAmplifier = 1;
        // Start is called before the first frame update
        void Start()
        {
            state = new State()
            {
                hp = Hp,
                power = power,
                speedMultiplier = speedMultiplier,
                moveSpeed = moveSpeed,
                weight = weight,
                satiety = satiety,
                benchmarkSatiety = benchmarkSatiety,
                hpEfflux = hpEfflux,
                powerEfflux = powerEfflux,
                satietyEfflux = satietyEfflux,
            };
            ai = gameObject.AddComponent<StupidAI>();
            ai.runCallBack += Run;

            dayTime = ProcessManager.Instance.dayTime;
            dayTime.StartDay(this);
            dayTime.callback += UpdateTime;
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
                return moveSpeed;
            }
        }


        public class State
        {
            public string other;
            public float hp;
            public float power;
            public float speedMultiplier;
            public float moveSpeed;
            public float weight;//重量
            public float water;
            /// <summary>
            /// 饱腹度
            /// </summary>
            public float satiety;
            /// <summary>
            /// 基准饱腹度
            /// </summary>
            public float benchmarkSatiety;//
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
