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
    private PlayerController playerS;   //PlayerController��Script

    void Start()
    {
        playerS.SetParameta(CListData.CharacterDataList[CharacterId]);
    }

    //�����ɂԂ�������
    private void OnCollisionEnter2D(Collision2D hit)
    {
        playerS.HitWall();
    } 

    //�v���C���[�̊�{����
    public void UpdatePlayer()
    {
        playerS.KeyController();  //�v���C���[�̃L�[���쏈��
        playerS.AdjustRigid();    //�v���C���[��rigid����
    }
}
