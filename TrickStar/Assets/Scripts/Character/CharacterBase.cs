using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X
public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected ItemBox itemsBoxS;  //���ׂẴA�C�e�����Ǘ�����Ă���X�N���v�g
    protected List<int> EquipmentItem = new List<int>();

    //�p�����[�^�[
    public int maxhp;          //�ő�̗�
    public int hp;             //�̗�
    public int attack;         //�U����
    public float jumppower;    //�W�����v��
    public float speed;        //�ړ����x

    public int airjump;        //�󒆂ŃW�����v�ł����

    protected Vector2 rigid;      //������

    //��ԊǗ�
    protected int state;            //�v���C���[�̃X�e�[�^�X

    protected readonly int ALIVE;   //�����t���O
    protected readonly int JUMP;    //�W�����v�t���O
    protected readonly int ATTACK;  //�U���t���O

    public int itemstate;        //�A�C�e���̃X�e�[�^�X

    public readonly int INVINCLEBLE; //���G�t���O
    public readonly int DOUBLEJUMP;  //�_�u���W�����v�t���O
    public readonly int DIG;         //�@���t���O
    public readonly int CREATE;      //�u���b�N�𐶐��ł���t���O

    public CharacterBase()
    {
        state = 0b0001;
        ALIVE = 0b0001;
        JUMP = 0b0010;
        ATTACK = 0b0011;

        itemstate = 0b0000;
        INVINCLEBLE = 0b0001;
        DOUBLEJUMP = 0b0010;
        DIG = 0b0011;
        CREATE = 0b0100;
    }

    //���]�b�g
    public void SetParameta(CharacterData charData)   //CharacterData�ɍ��킹�ăp�����[�^�[��ύX����
    {
        maxhp = charData.maxhp;
        attack = charData.attack;
        jumppower = charData.jumppower;
        speed = charData.speed;
    }

    public void ActiveItems()
    {
        foreach(int id in EquipmentItem)
        {
            itemsBoxS.ActiveItems(this, id);
        }
    }

    public virtual void CharacterUpdate() {;}

    public virtual void KeyController() {;}      //�L�[���Ƃ̃A�N�V����
    public virtual void Jump() {;}               //�W�����v���郁�]�b�g
    public virtual void Walk(int direction) {;}  //�ړ����郁�]�b�g
    public virtual void Attack() {;}             //�U�����郁�]�b�g

    public Vector2 GetRigid() { return rigid; }  //rigid���������郁�]�b�g
    public virtual void HitWall() {;}            //�ǂɓ����������̏���
}