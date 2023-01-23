using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X
public class CharacterBase : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    protected ItemBox itemsBoxS;  //���ׂẴA�C�e�����Ǘ�����Ă���X�N���v�g
    [SerializeField]
    protected RelationTilemapScript tilemapS;    //�^�C���}�b�v�֘A�̃X�N���v�g

    //�������Ă���A�C�e��ID��List
    [SerializeField]
    protected List<BelongItemData> EquipmentItem = new List<BelongItemData>();
    protected int Mitems; //�����Ă���A�C�e����

    [Header("Parametas")]
    //�p�����[�^�[
    protected int maxhp;          //�ő�̗�
    protected int hp;             //�̗�
    protected int power;         //�U����
    protected float jumppower;    //�W�����v��
    protected float speed;        //�ړ����x

    public int Maxhp { get { return maxhp; } set { maxhp = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int Power { get { return power; } set { power = value; } }
    public float Jumppower { get { return jumppower; } set { jumppower = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

    protected int airjump;        //�󒆂ŃW�����v�ł����
    protected int limitairjump;   //�󒆂ŃW�����v�ł����

    public int Airjump { get { return airjump; } set { airjump = value; } }
    public int Limitairjump { get { return limitairjump; } set { limitairjump = value; } }

    protected Vector2 rigid;      //������

    //��ԊǗ�
    protected uint state;            //�v���C���[�̃X�e�[�^�X
    protected uint itemstate;        //�A�C�e���̃X�e�[�^�X

    public uint Itemstate { get { return itemstate; } set { itemstate = value; } }

    public CharacterBase()
    {
        state = 0b1001;
        itemstate = 0b0000;
        for (int i = 0; i < Const.MAX_ITEMS; i++)
        {
            EquipmentItem.Add(new BelongItemData());
        }
    }

    //���]�b�g
    public void SetParameta(CharacterData charData)   //CharacterData�ɍ��킹�ăp�����[�^�[��ύX����
    {
        maxhp = charData.Maxhp;
        power = charData.Power;
        jumppower = charData.Jumppower;
        speed = charData.Speed;
    } 

    //�A�C�e����ǉ��ł��邩�m�F���Ă���ǉ����郁�]�b�g
    public bool AddItem(int id, int endurance)
    {
        //�A�C�e���̐������J��Ԃ�
        foreach(BelongItemData bData in EquipmentItem)
        {
            if (bData.MItemID == Const.NO_ITEM)
            {
                bData.MItemID = id;
                bData.MEndurance = endurance;
                return true;
            }
        }

        return false;
    }
    //�����Ă���A�C�e����id���m�F���郁�]�b�g
    public List<BelongItemData> GetItems() { return EquipmentItem; }
    //�����Ă���A�C�e����S�Ďg�p���郁�]�b�g
    public void ActiveItems()
    {
        foreach(BelongItemData bData in EquipmentItem)
        {
            itemsBoxS.UseItems(this, bData);
        }
    }

    public virtual void CharacterUpdate() {;}

    public virtual void KeyController() {;}      //�L�[���Ƃ̃A�N�V����
    public virtual void Jump() {;}               //�W�����v���郁�]�b�g
    public virtual void SkyJump() {;}            //�󒆂ŃW�����v���鏈��(�_�u���W�����v)
    public virtual void Walk(int direction) {;}  //�ړ����郁�]�b�g
    public virtual void Attack() {;}             //�U�����郁�]�b�g
    public virtual void HitWall() {;}            //�ǂɓ����������̏���
    
    public Vector2 GetPosition() { return this.transform.position; }
    public Vector2 GetRigid() { return rigid; }  //rigid���������郁�]�b�g

    public uint GetItemState() { return itemstate; }    //�L�����N�^�[�̃A�C�e���X�e�[�^�X����������

    //�L�����N�^�[��ACTIVE�t���O����������
    public bool GetActive() {

        if (Const.ACTIVE == (state & Const.ACTIVE))
            return true;

        return false;    
    }
    //�L�����N�^�[��ACTIVE�t���O��ύX����
    public void SetActive(bool b)
    {
        if (b && Const.ACTIVE != (state & Const.ACTIVE))
            state |= Const.ACTIVE;
        else if (Const.ACTIVE == (state & Const.ACTIVE))
            state &= ~Const.ACTIVE;
    }
}