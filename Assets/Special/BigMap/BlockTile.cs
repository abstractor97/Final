using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTile
{
    public int x;
    public int y;
    public float pos_x;
    public float pos_y;
    public bool isWall;
    /// <summary>
    /// 代表
    /// </summary>
    public Block6RangeMap.TileType type;
    /// <summary>
    /// 使用该类别的第几格地块
    /// </summary>
    public int index;

    public Block6RangeMap.InTileBulid build;

    public int buildLv;

    public float speedCoe;

   
}
