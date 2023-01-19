using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //タイルマップとタイルを所得するため使用

public class DigScript : MonoBehaviour
{
    [Header("TileMaps")]
    [SerializeField]
    Tilemap BlockTileMap;  //ブロックが置かれているタイルマップ
    [SerializeField]
    Tilemap UITileMap;     

    [Header("Tiles")]
    [SerializeField]
    RuleTile GroundTile;
    [SerializeField]
    Tile SelectTile;   //マウスの位置に置かれるタイル

    private Vector3Int oldTilePos;

    private Vector3 GetMousePosition()  //マウスの位置(ワールド座標)を取得するメゾット
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //スクリーンでのワールド座標を取得
        return mousePos;
    }

    private void ShowSelectTile()
    {
        var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //マウスの位置座標をタイルマップ上の位置座標に変換する

        if (oldTilePos == TilePos) { 
            return;
        }

        UITileMap.SetTile(oldTilePos, null);
        UITileMap.SetTile(TilePos, SelectTile);
        oldTilePos = TilePos;
    }

    void Update()
    {
        ShowSelectTile();

        if (Input.GetMouseButtonDown(0))    //左クリック
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); 
            if (BlockTileMap.GetTile(TilePos) != null)  //もしその位置にあるタイルが空じゃないとき
            {
                BlockTileMap.SetTile(TilePos, null);    //その位置にあるタイルを空っぽにする
            }
        }

        if (Input.GetMouseButton(1))    //右クリック
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //マウスの位置座標をタイルマップ上の位置座標に変換する

            if (BlockTileMap.GetTile(TilePos) == null)  //もしその位置のタイルが空っぽのとき
            {
                BlockTileMap.SetTile(TilePos, GroundTile);  //その場所のタイルをGroundTileに変更する
            }
        }
    }
}
