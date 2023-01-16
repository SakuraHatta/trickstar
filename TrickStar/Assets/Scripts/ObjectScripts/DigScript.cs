using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigScript : MonoBehaviour
{

    [SerializeField]
    UnityEngine.Tilemaps.Tilemap BlockTileMap;
    [SerializeField]
    UnityEngine.Tilemaps.Tile GroundTile;

    private Vector2 GetMousePosition()  //�}�E�X�̈ʒu���擾���郁�]�b�g
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //�X�N���[���ł̃��[���h���W���擾
        return mousePos;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //���N���b�N
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //�}�E�X�̈ʒu���W���^�C���}�b�v��̈ʒu���W�ɕϊ�����
            if (BlockTileMap.GetTile(TilePos) != null)  //�������̈ʒu�ɂ���^�C�����󂶂�Ȃ��Ƃ�
            {
                BlockTileMap.SetTile(TilePos, null);    //���̈ʒu�ɂ���^�C��������ۂɂ���
            }
        }

        if (Input.GetMouseButtonDown(1))    //�E�N���b�N
        {
            var TilePos = BlockTileMap.WorldToCell(GetMousePosition()); //�}�E�X�̈ʒu���W���^�C���}�b�v��̈ʒu���W�ɕϊ�����

            if (BlockTileMap.GetTile(TilePos) == null)  //�������̈ʒu�̃^�C��������ۂ̂Ƃ�
            {
                BlockTileMap.SetTile(TilePos, GroundTile);  //���̏ꏊ�̃^�C����GroundTile�ɕύX����
            }
        }
    }
}
