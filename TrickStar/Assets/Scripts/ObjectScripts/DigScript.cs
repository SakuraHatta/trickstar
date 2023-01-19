using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //�^�C���}�b�v�ƃ^�C�����������邽�ߎg�p

public class DigScript : MonoBehaviour
{
    [Header("TileMaps")]
    [SerializeField]
    Tilemap BlockTileMap;  //�u���b�N���u����Ă���^�C���}�b�v
    [SerializeField]
    Tilemap UITileMap;     

    [Header("Tiles")]
    [SerializeField]
    RuleTile GroundTile;
    [SerializeField]
    Tile SelectTile;   //�}�E�X�̈ʒu�ɒu�����^�C��

    private Vector3Int oldTilePos;

    private Vector3 GetMousePosition()  //�}�E�X�̈ʒu(���[���h���W)���擾���郁�]�b�g
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //�X�N���[���ł̃��[���h���W���擾
        return mousePos;
    }

    private void ShowSelectTile()
    {
        var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //�}�E�X�̈ʒu���W���^�C���}�b�v��̈ʒu���W�ɕϊ�����

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

        if (Input.GetMouseButtonDown(0))    //���N���b�N
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); 
            if (BlockTileMap.GetTile(TilePos) != null)  //�������̈ʒu�ɂ���^�C�����󂶂�Ȃ��Ƃ�
            {
                BlockTileMap.SetTile(TilePos, null);    //���̈ʒu�ɂ���^�C��������ۂɂ���
            }
        }

        if (Input.GetMouseButton(1))    //�E�N���b�N
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //�}�E�X�̈ʒu���W���^�C���}�b�v��̈ʒu���W�ɕϊ�����

            if (BlockTileMap.GetTile(TilePos) == null)  //�������̈ʒu�̃^�C��������ۂ̂Ƃ�
            {
                BlockTileMap.SetTile(TilePos, GroundTile);  //���̏ꏊ�̃^�C����GroundTile�ɕύX����
            }
        }
    }
}
