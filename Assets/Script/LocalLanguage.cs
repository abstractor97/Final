using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLanguage
{
    private TextAsset textasset;
    public readonly string ITEMTABLE_PATH = "Table/TextTable.xlsx";
    private string localLangId = "zh_CN";
    private JsonData jd;
    private string localFlag;

    public LocalLanguage()
    {
        localLangId = System.Globalization.CultureInfo.InstalledUICulture.Name;
        textasset = Resources.Load<TextAsset>(ITEMTABLE_PATH);
        jd = JsonMapper.ToObject(textasset.text);
        localFlag = localLangId.Substring(0, 2);
    }

   

    public string Text(string baseText)
    {
        if (textasset != null)
        {         
            if (localFlag.Equals("zh") )
            {
                return baseText;
            }
            else
            {
                if (IsNumeric(baseText))
                {
                    baseText.Contains(@"\d");
                    string zht = System.Text.RegularExpressions.Regex.Replace(baseText, @"\d", "{d}");
                    string ent = jd[zht]["en"].ToString();
                    return System.Text.RegularExpressions.Regex.Replace(baseText,"{d}", "{d}");
                }
                else
                {
                    return jd[baseText]["en"].ToString();
                }
              
               
            }
           
        }
        return "";
        //几个特殊路径
        //		Debug.Log ("dataPath ：" + Application.dataPath);
        //		Debug.Log ("persistentDataPath ：" + Application.persistentDataPath);
        //		Debug.Log ("temporaryCachePath ：" + Application.temporaryCachePath);
        //		Debug.Log ("streamingAssetsPath ：" + Application.streamingAssetsPath); 		
        //第二种读取读取本地文件方法
        //		FileStream fs = File.OpenRead (Application.dataPath+ "/Resources/book.txt");
        //		StreamReader sr = new StreamReader (fs);
        //		string wholeText = sr.ReadToEnd ();
        //		sr.Close ();
        //		fs.Close ();
        //		fs.Dispose ();
        //		JsonData jd = JsonMapper.ToObject (wholeText); 		
        //用WWW方法读取网络文件（加入协程）（可以做一个进度条）
        //		WWW www = new WWW("http://9214193.s21d-9.faiusrd.com/68/ABUIABBEGAAg7YXmuAUohKPUtQY.txt");
        //		Debug.Log ("start to load file.....");
        //		yield return www;
        //		Debug.Log ("end of load .");
        //		JsonData jd = JsonMapper.ToObject (www.text);  	
        //		读入json数据，并转化为程序中的数据结构		


    }



        private bool IsNumeric(string str)
        {
            
            foreach (char c in str)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
           return true;
        }
}
