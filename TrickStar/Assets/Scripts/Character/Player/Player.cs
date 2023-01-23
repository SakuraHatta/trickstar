using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�L�����N�^�[�f�[�^�[
    [Header("Character")]
    [SerializeField]
    private int CharacterId;    //CharacterId
    [SerializeField]
    private CharacterListData CListData;

    [Space(10)]
    [Header("Scripts")]
    [SerializeField]
    private PlayerController playerCS;   //PlayerController��Script

    void Start()
    {
        playerCS.SetParameta(CListData.CharacterDataList[CharacterId]);
    }

    //�����ɂԂ�������
    private void OnCollisionEnter2D(Collision2D hit)
    {
        playerCS.HitWall();
    } 

    //�v���C���[�̊�{����
    public void Update()
    {
        if (!playerCS.GetActive()) { return; }

        playerCS.KeyController();  //�v���C���[�̃L�[���쏈��
        playerCS.AdjustRigid();    //�v���C���[��rigid����
        playerCS.SelectTile();
    }
}