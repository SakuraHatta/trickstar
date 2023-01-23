using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�p���N���X(�p���� : CharacterBase)
public class PlayerController : CharacterBase
{
    [Space(10)]
    [Header("Physics")]
    [SerializeField]
    private BoxCollider2D colliderC;  //BodyCollider2D�̃R���|�[�l���g
    [SerializeField]
    private Rigidbody2D rigidC;  //RigidBody2D�̃R���|�[�l���g

    private int money;  //������

    //�L�[����
    public override void KeyController()
    {
        if (Const.ACTIVE != (state & Const.ACTIVE)) { return; }

        //A�L�[���������Ƃ�
        if (Input.GetKey(KeyCode.A))//�E�Ɉړ�
        {
            Walk(-1);
        }
        //D�L�[���������Ƃ�
        if (Input.GetKey(KeyCode.D))//���Ɉړ�
        {
            Walk(1);
        }
        //�X�y�[�X�L�[���������Ƃ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Const.JUMP != (state & Const.JUMP))    //�v���C���[���W�����v��Ԃ���Ȃ��Ƃ�
            {
                state |= Const.JUMP;
                Jump();
            }
            else if (Const.DOUBLEJUMP == (state & Const.DOUBLEJUMP))
            {
                Jump();
            }
        }
    }
    //��������
    public override void Walk(int direction)
    {
        Vector2 walk = new Vector2(speed * direction, 0.0f);
        rigidC.AddForce(walk, ForceMode2D.Force);        
    }
    //�W�����v
    public override void Jump()
    {
        Vector2 jump = new Vector2(0.0f, jumppower);
        rigidC.AddForce(jump, ForceMode2D.Impulse);
    }
    //�n�ʂɐG������
    public override void HitWall()
    {
        state &= ~Const.JUMP;
        rigid.y = 0.0f;
    }

    //�����Ă��邩�m�F����
    public bool CheckAlive() { 
        if (Const.ALIVE == (state & Const.ALIVE)) { return true; }  //�����Ă�����true��Ԃ�
        return false;   //�����ĂȂ�������false��Ԃ�
    }
    //����������������
    public int GetMoney() { return money; }

    //�����Ă���A�C�e����id���m�F���郁�]�b�g
    public List<int> GetItems(){ return EquipmentItem; }

    //�A�C�e����ǉ����郁�]�b�g
    public void AddItem(int id)
    {
        Mitems++;
        EquipmentItem.Add(id);
    }
    //�A�C�e�����g�p���郁�]�b�g
    public void UseItem(int equipIndex)
    {
        //�����C���f���g���ł܂������ĂȂ��A�C�e�����g�����Ƃ����Ƃ�
        if (equipIndex > Mitems - 1) {
            //�����𒆒f����
#if CHECK
            Debug.Log("index��" + equipIndex + "�̃A�C�e���͂܂����������Ă��Ȃ�!");
#endif
            return;
        }    
        itemsBoxS.ActiveItems(this ,EquipmentItem[equipIndex]);
#if CHECK
        Debug.Log("index��" + equipIndex + "��ID��" + EquipmentItem[equipIndex] + "�A�C�e�����g����");
#endif
    }
    //rigid�𒲐�����֐�
    public void AdjustRigid()
    {
        rigid = rigidC.velocity;    //���ۂ�rigid��RigidBody2D�R���|�[�l���g���玝���Ă���

        //rigid.x��speed���傫���Ȃ�Ȃ��悤�ɂ���
        if (rigid.x * rigid.x > speed * speed)
        {
            if (rigid.x > 0.0f) //rigid�����̐��̎�
                rigid.x = speed;    
            else                //rigid�����̐��̎�
                rigid.x = -speed;
        }

        rigidC.velocity = rigid;    //��������rigid��ݒ肷��
    }
}
