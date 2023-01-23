using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //�^�C���}�b�v�ƃ^�C�����������邽�ߎg�p

public class RelationTilemapScript : MonoBehaviour
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
    AnimatedTile SelectTile;   //�}�E�X�̈ʒu�ɒu�����^�C��

    [Header("Physics")]
    [SerializeField]
    TilemapCollider2D tilemapCollider;

    private Vector3Int oldTilePos;  //�O�̃}�E�X�ʒu

    private Vector3 GetMousePosition()  //�}�E�X�̈ʒu(���[���h���W)���擾���郁�]�b�g
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //�X�N���[���ł̃��[���h���W���擾
        return mousePos;
    }

    //�}�E�X�̈ʒu�Ƀ^�C����\������֐�
    public void ShowSelectTile(uint itemstate)
    {
        if (Const.DIG == (itemstate & Const.DIG) || Const.CREATE == (itemstate & Const.CREATE))
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //�}�E�X�̈ʒu���W���^�C���}�b�v��̈ʒu���W�ɕϊ�����

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
    //�����̈ʒu�̃^�C�����폜����֐�
    public bool DeleteTile()
    {
        var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //�}�E�X�̈ʒu���W���^�C���}�b�v��̈ʒu���W�ɕϊ�����
        if (BlockTileMap.GetTile(TilePos) != null)  //�������̈ʒu�ɂ���^�C�����󔒂���Ȃ��Ƃ�
        {
            BlockTileMap.SetTile(TilePos, null);    //���̈ʒu�ɂ���^�C�����󔒂ɂ���
            return true;
        }

        return false;
    } 
    //�����̈ʒu�ɐV�����^�C����ݒu����֐�
    public bool CreateTile()
    {
        var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //�}�E�X�̈ʒu���W���^�C���}�b�v��̈ʒu���W�ɕϊ�����
        if (BlockTileMap.GetTile(TilePos) == null)  //�������̈ʒu�̃^�C�����󔒂̂Ƃ�
        {
            BlockTileMap.SetTile(TilePos, GroundTile);  //���̏ꏊ�̃^�C����GroundTile�ɕύX����
            return true;
        }
        return false;
    }
}
