using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block6RangeMap : MonoBehaviour
{
    //利用脚本生成地图。 

    public GameObject[] OutWallArray;
    public GameObject[] FloorArray;

    public int rows = 10;  //定义地图的行列,宽。
    public int cols = 10; //定义地图的行列,高。
    public bool cWall;
    public TileSet[] otherSets;

    public UnityAction<GameObject,BlockTile> clickTile;

    private Transform mapHolder;
    private List<List<BlockTile>> tiles = new List<List<BlockTile>>();//保存这个
    private List<GameObject> objTiles = new List<GameObject>();//地块的对象引用
    private Dictionary<TileType, TileSet> ttSets = new Dictionary<TileType, TileSet>();

    [System.Serializable]
    public class TileSet
    {
        public TileType type;
        public int minCount = 2;
        public int maxCount = 8;
        [Tooltip("是否参与去噪点")]
        public bool isSmooth;
        public GameObject[] TileArray;
    }

    // Use this for initialization
    void Start()
    {
        foreach (var set in otherSets)
        {
            ttSets.Add(set.type, set);
        }
        otherSets = null;
        InitMap_6();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FindClickTile(Input.mousePosition);
        }
    }
    /// <summary>
    /// 排列次序为左下角开始，已列为单位
    /// </summary>
    private void InitMap_6()
    {
        //   mapHolder = new GameObject("Map").transform;// 设置一个父类管理生成的地图
        mapHolder = transform;
        for (int x = 0; x < rows; x++)
        {
            tiles.Add(new List<BlockTile>());
            for (int y = 0; y < cols; y++)
            {

                float lx = x * Mathf.Cos(41.5f * Mathf.Deg2Rad);
                float ly = y * Mathf.Cos(30 * Mathf.Deg2Rad) - (x % 2) * Mathf.Cos(30 * Mathf.Deg2Rad) / 2;

                if ((x == 0 || y == 0 || x == cols - 1 || y == rows - 1) && cWall)
                {
                    tiles[x].Add(new BlockTile
                    {
                        x=x,
                        y=y,
                        pos_x = lx,
                        pos_y=ly,
                        type = TileType.wall,
                        isWall = true,
                        index = Random.Range(0, OutWallArray.Length)
                  
                    });
                }
                else
                {
                    tiles[x].Add(new BlockTile
                    {
                        x = x,
                        y = y,
                        pos_x = lx,
                        pos_y = ly,
                        type = TileType.land ,
                        isWall =false,
                        index = Random.Range(0, FloorArray.Length)
                    });
                }
            
            }
        }
        //创建障碍物及其他
        foreach (var set in ttSets)
        {
            for (int i = 0; i < Random.Range(set.Value.minCount, set.Value.maxCount); i++)
            {
                int x = Random.Range(1, rows - 1);
                int y = Random.Range(1, cols - 1);
                //随机取得位置
                BlockTile tile = tiles[x][y];
                tile.type = set.Value.type;
                tile.index= Random.Range(0, set.Value.TileArray.Length);
            } 
        }

        //绘制地图
        DrawTile();
        //地图聚合(去噪点)
        SmoothMap();
    }


    private void DrawTile()
    {
        foreach (var tiles in tiles)
        {

            foreach (var tile in tiles)
            {
                GameObject go = null;
                switch (tile.type)
                {
                    case TileType.land:
                        go = GameObject.Instantiate<GameObject>(FloorArray[tile.index], new Vector3(tile.pos_x, tile.pos_y, 0), FloorArray[tile.index].transform.rotation);
                        break;
                    case TileType.wall:
                        go = GameObject.Instantiate<GameObject>(OutWallArray[tile.index], new Vector3(tile.pos_x, tile.pos_y, 0), OutWallArray[tile.index].transform.rotation);
                        break;
                    default:
                        go = ttSets[tile.type].TileArray[Random.Range(0, ttSets[tile.type].TileArray.Length)];
                        go = GameObject.Instantiate<GameObject>(go, new Vector3(tile.pos_x, tile.pos_y, 0), go.transform.rotation);
                        break;
                }

                go.transform.SetParent(mapHolder, false);
                objTiles.Add(go);

            }
        }
    }

    /// <summary>
    /// 将随机生成的数据集群化
    /// </summary>
    void SmoothMap()
    {
        foreach (var tilesi in tiles)
        {
            foreach (var tile in tilesi)
            {
                if (ttSets.ContainsKey(tile.type) &&ttSets[tile.type].isSmooth)
                {
                    tile.type = Get6BlockSurround(tile, tiles);
                    tile.index = Random.Range(0, ttSets[tile.type].TileArray.Length);
                }
            }
        }
    }

    /// <summary>
    /// 去噪点
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    TileType Get6BlockSurround(BlockTile tile, List<List<BlockTile>> allTiles)
    {
        //x,y+1
        //x-1,y; x+1,y
        //x-1,y-1;x,y-1;x+1,y-1
        Dictionary<TileType, int> count = new Dictionary<TileType, int>();
        Count(count,allTiles[tile.x][tile.y + 1].type);
        Count(count, allTiles[tile.x-1][tile.y].type);
        Count(count, allTiles[tile.x+1][tile.y].type);
        Count(count, allTiles[tile.x-1][tile.y - 1].type);
        Count(count, allTiles[tile.x][tile.y - 1].type);
        Count(count, allTiles[tile.x+1][tile.y - 1].type);

        foreach (var item in count)
        {
            if (item.Value > 3)
            {
                return item.Key;
            }
            else
            {
                return tile.type;
            }
        }
        return tile.type;
    }

    private void Count(Dictionary<TileType, int> count,TileType type)
    {
        if (count.ContainsKey(type))
        {
            count[type] += 1;
        }
        else
        {
            count.Add(type, 1);
        }
    }


   private void FindClickTile(Vector3 pos)
    {
        if (transform.position.x<pos.x&& pos.x < transform.position.x+rows * Mathf.Cos(41.5f * Mathf.Deg2Rad) && transform.position.y<pos.y&&pos.y<transform.position.y+cols)
        {

          int x= (int) (pos.x / Mathf.Cos(41.5f * Mathf.Deg2Rad));
          int y = (int)(pos.y / (Mathf.Cos(30 * Mathf.Deg2Rad) - (x % 2) * Mathf.Cos(30 * Mathf.Deg2Rad) / 2));
            clickTile?.Invoke(objTiles[x*y-(cols-y)], tiles[x][y]);

        }
    }

    public enum TileType
    {
        land,
        water,
        mountain,
        res,
        wall
    }

    public enum InTileBulid
    {
        hold,
        bastion,
        city,
        village
    }

}
