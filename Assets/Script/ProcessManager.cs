using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Map;
using System;
using System.Text;
using System.Threading;
using UnityEngine.Events;

/// <summary>
/// 通用进程控制
/// </summary>
public class ProcessManager:MonoBehaviour
{
    private const string SAVEFILENAME = "/byBin.dat";
    public bool isInGame;
    public Save save;
    public static LocalLanguage language = new LocalLanguage();

    private void Awake()
    {
       // language = new LocalLanguage();
        save = LoadByBin();
        if (save == null)
        {
            save = new Save();
        }
    }

    private void Start()
    {
       
        DontDestroyOnLoad(this);
    }

    public void CreateSaveData()
    {
        //player相关
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerManager playerMana = GameObject.FindObjectOfType<PlayerManager>();
        List<Buff> buffs = new List<Buff>();
        playerMana.buffs.ForEach(delegate (PlayerManager.BuffControl buffControl) {
            buffControl.buff.totalTime = buffControl.time;
            buffs.Add(buffControl.buff);
        });
        PlayerSave playerSave = new PlayerSave
        {
            x = player.transform.position.x,
            y = player.transform.position.y,
            state = playerMana.state,
            buffs= buffs.ToArray()
        };


        //points相关
        EventEmitter[] emitters = GameObject.FindObjectsOfType<EventEmitter>();

        Thread th1 = new Thread(delegate () { float l = emitters[0].gameObject.transform.position.x; });

        PointsSave[] pointsSaves = new PointsSave[emitters.Length];
        for (int i = 0; i < emitters.Length; i++)
        {
            PointsSave pointsSave = new PointsSave
            {
                x = emitters[i].gameObject.transform.position.x,
                y = emitters[i].gameObject.transform.position.y,
                position = emitters[i].position,               
            };

            if (emitters[i].points.isRandom)
            {
                if (emitters[i].points.maxPlace == emitters[i].points.places.Length)//相等时证明以被生成
                {
                    pointsSave.randomPlaces = new string[emitters[i].points.maxPlace];
                    for (int j = 0; j < pointsSave.randomPlaces.Length; j++)
                    {
                        pointsSave.randomPlaces[j] = emitters[i].points.places[j].name;
                    }
                  
                }
            }
            else
            {

                for (int j = 0; j < emitters[i].points.places.Length; j++)
                {
                    pointsSave.states[j] = emitters[i].points.places[j].state;
                }
            }
            pointsSaves[i] = pointsSave;
        }


    }
    [Serializable]
    public struct PointsSave
    {
        public float x;
        public float y;
        public float z;

        public int position;

        public Place.State[] states;

        public string[] randomPlaces;

    }

    [Serializable]
    public struct PlayerSave
    {
        public float x;
        public float y;
        public float z;

        public People state;

        public Buff[] buffs;

    }


    [Serializable]
    public struct ItemSave
    {
        public string ascription;

        public Item item;

        public int number;
    }
    /// <summary>
    /// 恢复数据
    /// </summary>
    public void LoadSave(UnityAction loadCallback)
    {
        GameObject.FindGameObjectWithTag("Player").transform.position= new Vector3(save.playerSave.x,save.playerSave.y,save.playerSave.z);
        PlayerManager playerMana = GameObject.FindObjectOfType<PlayerManager>();
        playerMana.state = save.playerSave.state;
        foreach (var buff in save.playerSave.buffs)
        {
            playerMana.AddBuff(buff);
        }


        EventEmitter[] emitters = GameObject.FindObjectsOfType<EventEmitter>();

        Thread th1 = new Thread(delegate () { float l = emitters[0].gameObject.transform.position.x; });


        for (int i = 0; i < emitters.Length; i++)
        {
            emitters[i].position = save.pointsSaves[i].position;

            if (emitters[i].points.isRandom)
            {
                if (save.pointsSaves[i].randomPlaces != null)
                {
                    List<Place> places = new List<Place>();
                    for (int j = 0; j < save.pointsSaves[i].randomPlaces.Length; j++)
                    {
                        foreach (var place in emitters[i].points.places)
                        {
                            if (save.pointsSaves[i].randomPlaces[j].Equals(place.name))
                            {
                                places.Add(place);
                                break;
                            }
                        }
                        emitters[i].points.places[j].state = save.pointsSaves[i].states[j];
                    }
                    emitters[i].points.places = places.ToArray();
                    //  emitters[i].points.isRandom = false;
                }
            }
            else
            {
                for (int j = 0; j < emitters[i].points.places.Length; j++)
                {
                    emitters[i].points.places[j].state = save.pointsSaves[i].states[j];
                }
              
            }


        }
    }


    /// <summary>
        /// 二进制方法：存档
        /// </summary>
    private void SaveByBin()
    {
       // Save save = LoadByBin();
        //序列化过程（将Save对象转换为字节流）
        //创建Save对象并保存当前游戏状态
        if (save == null)
        {
            save = new Save();
        }      
        //创建一个二进制格式化程序
        BinaryFormatter bf = new BinaryFormatter();
        //创建一个文件流
        //File.op;
        FileStream fileStream = File.Create(Application.dataPath + SAVEFILENAME);
        //用二进制格式化程序的序列化方法来序列化Save对象,参数：创建的文件流和需要序列化的对象
        bf.Serialize(fileStream, save);
        //关闭流
        fileStream.Close();
        //如果文件存在，则显示保存成功
        if (File.Exists(Application.dataPath + SAVEFILENAME))
        {
            // SaveSuccessCallBack(this, EventArgs.Empty);
        }
    }

    /// <summary>
    ///二进制方法： 读档
    /// </summary>
    private Save LoadByBin()
    {
        if (File.Exists(Application.dataPath + SAVEFILENAME))
        {
            //反序列化过程
            //创建一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            //打开一个文件流
            FileStream fileStream =File.Open(Application.dataPath + SAVEFILENAME, FileMode.Open);
            //调用格式化程序的反序列化方法，将文件流转换为一个Save对象
            Save save = (Save)bf.Deserialize(fileStream);
            //关闭文件流
            fileStream.Close();
            // SetGame(save);
            return save;
        }
        else
        {
            return null;
        }
    }

    private IEnumerator AutoSave()
    {
        while (isInGame)
        {
            yield return new WaitForSeconds(60 * 3);
            CreateSaveData();
        }

    }

 

    [System.Serializable]
    public class Save
    {

        public string dogTag;

        public PlayerSave playerSave;

        public PointsSave[] pointsSaves;

        public ItemSave[] itemSaves;
    }
}
