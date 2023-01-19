using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X
public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected ItemBox itemsBoxS;  //���ׂẴA�C�e�����Ǘ�����Ă���X�N���v�g
    [SerializeField]
    protected List<int> EquipmentItem = new List<int>();

    protected int Mitems; //�����Ă���A�C�e����

    //�p�����[�^�[
    public int maxhp;          //�ő�̗�
    public int hp;             //�̗�
    public int attack;         //�U����
    public float jumppower;    //�W�����v��
    public float speed;        //�ړ����x

    public int airjump;        //�󒆂ŃW�����v�ł����

    protected Vector2 rigid;      //������

    //��ԊǗ�
    protected uint state;            //�v���C���[�̃X�e�[�^�X
    public uint itemstate;        //�A�C�e���̃X�e�[�^�X

    public CharacterBase()
    {
        state = 0b1001;
        itemstate = 0b0000;
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
    
    public Vector2 GetPosition() { return this.transform.position; }
    public Vector2 GetRigid() { return rigid; }  //rigid���������郁�]�b�g
    public virtual void HitWall() {;}            //�ǂɓ����������̏���
    public void ChangeActive(bool b)
    {
        if (b && Const.ACTIVE != (state & Const.ACTIVE))
            state |= Const.ACTIVE;
        else if (Const.ACTIVE == (state & Const.ACTIVE))
            state &= ~Const.ACTIVE;
    }
}