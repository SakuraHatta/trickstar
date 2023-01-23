using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //タイルマップとタイルを所得するため使用

public class RelationTilemapScript : MonoBehaviour
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
    AnimatedTile SelectTile;   //マウスの位置に置かれるタイル

    [Header("Physics")]
    [SerializeField]
    TilemapCollider2D tilemapCollider;

    private Vector3Int oldTilePos;  //前のマウス位置

    private Vector3 GetMousePosition()  //マウスの位置(ワールド座標)を取得するメゾット
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //スクリーンでのワールド座標を取得
        return mousePos;
    }

    //マウスの位置にタイルを表示する関数
    public void ShowSelectTile(uint itemstate)
    {
        if (Const.DIG == (itemstate & Const.DIG) || Const.CREATE == (itemstate & Const.CREATE))
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //マウスの位置座標をタイルマップ上の位置座標に変換する

            if (oldTilePos == TilePos) { 
                return;
            }

            UITileMap.SetTile(oldTilePos, null);
            UITileMap.SetTile(TilePos, SelectTile);
            oldTilePos = TilePos;
        }
        else
        {
            if (UITileMap.GetTile(oldTilePos) == null) { return; }
            UITileMap.SetTile(oldTilePos, null);
        }
    }
    //引数の位置のタイルを削除する関数
    public bool DeleteTile()
    {
        var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //マウスの位置座標をタイルマップ上の位置座標に変換する
        if (BlockTileMap.GetTile(TilePos) != null)  //もしその位置にあるタイルが空白じゃないとき
        {
            BlockTileMap.SetTile(TilePos, null);    //その位置にあるタイルを空白にする
            return true;
        }

        return false;
    } 
    //引数の位置に新しくタイルを設置する関数
    public bool CreateTile()
    {
        var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //マウスの位置座標をタイルマップ上の位置座標に変換する
        if (BlockTileMap.GetTile(TilePos) == null)  //もしその位置のタイルが空白のとき
        {
            BlockTileMap.SetTile(TilePos, GroundTile);  //その場所のタイルをGroundTileに変更する
            return true;
        }
        return false;
    }
}
