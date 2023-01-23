using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Const
{
//�Q�[���X�e�[�^�X
    //���Ԋ֌W
    public const float DEFAULT_TIME_SCALE = 1.0f;   //��{�̎��ԊԊu
    //�Q�[���̃X�e�[�^�X
    public const uint DEFAULT = 0b0001; //���ʂ̏��
    public const uint ITEM = 0b0010;    //�A�C�e������\��
    public const uint SHOP = 0b0100;    //�V���b�v��
    public const uint PAUSE = 0b1000;   //�|�[�Y��

//�v���C��
    //�v���C���[�̃X�e�[�^�X
    public const uint ALIVE = 0b0001;   //�����t���O
    public const uint JUMP = 0b0010;    //�W�����v�t���O
    public const uint ATTACK = 0b0100;  //�U���t���O
    public const uint ACTIVE = 0b1000;  //�N���t���O

//�A�C�e��
    //�A�C�e���̃X�e�[�^�X
    public const uint INVINCLEBLE = 0b0001; //���G�t���O
    public const uint DOUBLEJUMP = 0b0010;  //�_�u���W�����v�t���O
    public const uint DIG = 0b0100;         //�@���t���O
    public const uint CREATE = 0b1000;      //�u���b�N�𐶐��ł���t���O
    public const int NO_ENDURANCE = -1;     //�ϋv�n���Ȃ����̑ϋv�n
    //�A�C�e���̃^�C�v
    public const uint PASSIVE_TYPE = 0b0001;     //��ɔ����n
    public const uint ACTIVE_TYPE = 0b0010;      //�{�^���Ŕ����n
    public const uint CREATE_TYPE = 0b0100;      //�}�b�v��������n
    public const uint OUTGAME_TYPE = 0b1000;     //�Q�[���ɒ��ډe����^����n
    //�C���x���g��
    public const int MAX_ITEMS = 3;     //���Ă�A�C�e���̍ő吔
    public const int NO_ITEM = -1;      //���̎����ĂȂ��Ƃ��̃A�C�e��ID
    //�A�C�e���̐�
    public const int TYPE_ITEMS = 5;    //�A�C�e���̎��

//�V���b�v
    public const int CARD_NUMBER = 3;   //�V���b�v�Ŕ����Ă���J�[�h�̖���

    public const float ITEM_SCALE = 1.0f;   //�V���b�v�̑傫��

//�^�C���}�b�v
    //�^�C��
    public const float TILE_SIZE = 1.0f;
}
