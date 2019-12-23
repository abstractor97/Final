using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapHold : MonoBehaviour
{
    public Block6RangeMap rangeMap;

    public Shader highlight;
    public Shader defShader;
    //地块的UI
    public GameObject tileUI;

    private GameObject cacheTile;
    // Start is called before the first frame update
    void Start()
    {
        rangeMap.clickTile = ClickTile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void ClickTile(GameObject objTile, BlockTile tile)
    {
        objTile.GetComponent<Renderer>().material.shader = highlight;
        cacheTile.GetComponent<Renderer>().material.shader = defShader;

       
        switch (tile.type)
        {
            case Block6RangeMap.TileType.land:
                break;
            case Block6RangeMap.TileType.water:
                break;
            case Block6RangeMap.TileType.mountain:
                break;
            case Block6RangeMap.TileType.res:
                break;
            case Block6RangeMap.TileType.wall:
                break;
            default:
                break;
        }
        cacheTile = objTile;
    }


    private void ShowTileUI(GameObject objTile)
    {

    }
}
