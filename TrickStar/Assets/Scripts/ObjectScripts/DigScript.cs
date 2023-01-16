using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigScript : MonoBehaviour
{

    [SerializeField]
    UnityEngine.Tilemaps.Tilemap BlockTileMap;
    [SerializeField]
    UnityEngine.Tilemaps.Tile GroundTile;

    private Vector2 GetMousePosition()  //マウスの位置を取得するメゾット
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //スクリーンでのワールド座標を取得
        return mousePos;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //左クリック
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //マウスの位置座標をタイルマップ上の位置座標に変換する
            if (BlockTileMap.GetTile(TilePos) != null)  //もしその位置にあるタイルが空じゃないとき
            {
                BlockTileMap.SetTile(TilePos, null);    //その位置にあるタイルを空っぽにする
            }
        }

        if (Input.GetMouseButtonDown(1))    //右クリック
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //マウスの位置座標をタイルマップ上の位置座標に変換する

            if (BlockTileMap.GetTile(TilePos) == null)  //もしその位置のタイルが空っぽのとき
            {
                BlockTileMap.SetTile(TilePos, GroundTile);  //その場所のタイルをGroundTileに変更する
            }
        }
    }
}
