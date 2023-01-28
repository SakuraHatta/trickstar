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

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Damage")
        {
            playerCS.TakeDamage();
        }
    }

    //�v���C���[�̊�{����
    public void Update()
    {
        playerCS.AnimationUpdate(); //�A�j���[�V�����̃A�b�v�f�[�g

        //�����v���C���[���@�\���ĂȂ��Ȃ炱���Œ��f����
        if (!playerCS.GetActive()) {
            Debug.Log("�v���C���[�͋@�\���Ă��Ȃ�!");
            return;
        }

        playerCS.KeyController();  //�v���C���[�̃L�[���쏈��
        playerCS.AdjustRigid();    //�v���C���[��rigid����
        playerCS.SelectTile();      //�I�𒆂̏ꏊ�Ƀ^�C����\������֐�
        playerCS.Jump();        //�W�����v����
    }
}