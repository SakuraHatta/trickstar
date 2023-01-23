using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Const
{
    //�Q�[���X�e�[�^�X
    public const uint DEFAULT = 0b0001; //���ʂ̏��
    public const uint ITEM = 0b0010;    //�A�C�e������\��
    public const uint SHOP = 0b0100;    //�V���b�v��
    public const uint PAUSE = 0b1000;   //�|�[�Y��

    //�v���C��
    public const uint ALIVE = 0b0001;   //�����t���O
    public const uint JUMP = 0b0010;    //�W�����v�t���O
    public const uint ATTACK = 0b0100;  //�U���t���O
    public const uint ACTIVE = 0b1000;  //�N���t���O

    //�A�C�e��
    public const uint INVINCLEBLE = 0b0001; //���G�t���O
    public const uint DOUBLEJUMP = 0b0010;  //�_�u���W�����v�t���O
    public const uint DIG = 0b0100;         //�@���t���O
    public const uint CREATE = 0b1000;      //�u���b�N�𐶐��ł���t���O

    public const int MAX_ITEMS = 3;     //���Ă�A�C�e���̍ő吔
    public const int TYPE_ITEMS = 5;    //�A�C�e���̎��

    //�V���b�v
    public const int CARD_NUMBER = 3;   //�V���b�v�Ŕ����Ă���J�[�h�̖���

    public const float ITEM_SCALE = 1.0f;   //�V���b�v�̑傫��
}
