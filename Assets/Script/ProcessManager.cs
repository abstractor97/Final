using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Map;
using Stronghold;
using System;

public class ProcessManager 
{
    private static readonly ProcessManager _instance = new ProcessManager();
    private const string SAVEFILENAME = "/byBin.dat";
    public static ProcessManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public MapPlayer.State cacheState;
    //public DayTime dayTime;
    public bool isInGame;
    public Save save;
    public StrongholdControl.Type loadHold;

    private ProcessManager() {
        save = LoadByBin();
    }

    public void SavePlayerState(MapPlayer.State state) {
        cacheState = state;
    }

    public void StartGame()
    {

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
            SaveByBin();
        }

    }

 

    [System.Serializable]
    public class Save
    {

        public string dogTag;
        public float x;
        public float y;
        /// <summary>
        /// 格式name|name
        /// </summary>
        public string bagItems;
        /// <summary>
        /// 格式name|name
        /// </summary>
        public string packItems;
    }
}
