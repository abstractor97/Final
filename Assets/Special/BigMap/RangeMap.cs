using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PR_sc todo 地图生成
/// </summary>
public class RangeMap : MonoBehaviour
{
    //利用脚本生成地图。 10*10的像素地图

    public GameObject[] OutWallArray;
    public GameObject[] FloorArray;
    public GameObject[] WallArray;

    public int rows = 10;  //定义地图的行列。
    public int cols = 10;

    public int minCountWall = 2;
    public int maxCountWall = 8;

    private Transform mapHolder;
    private List<Vector2> positionList = new List<Vector2>();

    // Use this for initialization
    void Start()
    {
        InitMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitMap()
    {
        //   mapHolder = new GameObject("Map").transform;// 设置一个父类管理生成的地图
        mapHolder = transform;
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {

                if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)//地图最外面一圈是围墙
                {
                    int index = Random.Range(0, OutWallArray.Length);
                    GameObject go = GameObject.Instantiate(OutWallArray[index], new Vector3(x, y, 0), OutWallArray[index].transform.rotation) as GameObject;
               
                    go.transform.SetParent(mapHolder,false);
                }
                else// 其余是地板
                {
                    int index = Random.Range(0, FloorArray.Length);
                    GameObject go = GameObject.Instantiate(FloorArray[index], new Vector3(x, y, 0), FloorArray[index].transform.rotation) as GameObject;
                    go.transform.SetParent(mapHolder,false);
                }
            }
        }
        positionList.Clear();
        for (int x = 2; x < cols - 2; x++)
        {
            for (int y = 2; y < rows - 2; y++)
            {
                positionList.Add(new Vector2(x, y));
            }
        }
        //创建障碍物 食物 敌人
        //创建障碍物
        int WallCount = Random.Range(minCountWall, maxCountWall + 1);//随机生成障碍物个数的范围
        for (int i = 0; i < WallCount; i++)
        {
            //随机取得位置
            int positionIndex = Random.Range(0, positionList.Count);
            Vector2 pos = positionList[positionIndex];
            positionList.RemoveAt(positionIndex);
            //随机取得障碍物
            int WallIndex = Random.Range(0, WallArray.Length);
            GameObject go = GameObject.Instantiate(WallArray[WallIndex], pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapHolder,false);
        }
    }

}