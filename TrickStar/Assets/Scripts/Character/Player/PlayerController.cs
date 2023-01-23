using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�p���N���X(�p���� : CharacterBase)
public class PlayerController : CharacterBase
{
    [SerializeField]
    private BelongItem belongItemsS;    //�v���C���[�̎��������Ǘ�����Script(Player����)

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
        //�N��������Ȃ��Ȃ珈���𒆒f����
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
            //�v���C���[���W�����v��Ԃ���Ȃ��Ƃ�
            if (Const.JUMP != (state & Const.JUMP))    
            {
                state |= Const.JUMP;
                Jump();
            }
            //�󒆂Ń_�u���W�����v���ł���Ƃ�
            else if (Const.JUMP == (state & Const.JUMP) && Const.DOUBLEJUMP == (itemstate & Const.DOUBLEJUMP))    
            {
                if (airjump < limitairjump)
                SkyJump();
                airjump++;
            }
        }

        //���N���b�N���������Ƃ�
        if (Input.GetMouseButtonDown(0))
        {
            if (Const.DIG == (itemstate & Const.DIG))
            {
                if (tilemapS.DeleteTile())
                {
                    itemsBoxS.DecreaseCreate(this);
                    belongItemsS.ChangeColor();
#if CHECK
                        Debug.Log("�^�C����j��");
#endif          
                    return;
                }
#if CHECK
                    Debug.Log("�폜���s�c");
#endif
            }

            else if (Const.CREATE == (itemstate & Const.CREATE))
            {

                if (tilemapS.CreateTile())
                {
                    itemsBoxS.DecreaseCreate(this);
                    belongItemsS.ChangeColor();
#if CHECK
                        Debug.Log("�^�C���𐶐�");
#endif
                    return;
                }
                #if CHECK
                    Debug.Log("�������s�c");
                #endif
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
    //�_�u���W�����v
    public override void SkyJump()
    {
        rigid.y = 0.0f;
        Jump();
    }
    //�n�ʂɐG������
    public override void HitWall()
    {
        state &= ~Const.JUMP;
        rigid.y = 0.0f;
        
        if (Const.DOUBLEJUMP == (itemstate & Const.DOUBLEJUMP))
        {
            airjump = 0;
        }
    }
    //�}�E�X�̈ʒu�Ƀ^�C����\�������鏈��
    public void SelectTile()
    {
        tilemapS.ShowSelectTile(itemstate);
    }

    //�����Ă��邩�m�F����
    public bool CheckAlive() { 
        if (Const.ALIVE == (state & Const.ALIVE)) { return true; }  //�����Ă�����true��Ԃ�
        return false;   //�����ĂȂ�������false��Ԃ�
    }

    //����������������
    public int GetMoney() { return money; }

    //�A�C�e�����g�p���郁�]�b�g
    public void UseItem(int equipIndex)
    {
        //�����C���f���g���ł܂������ĂȂ��A�C�e�����g�����Ƃ����Ƃ�
        if (EquipmentItem[equipIndex].MItemID == Const.NO_ITEM) {
            //�����𒆒f����
            #if CHECK
                Debug.Log("index��" + equipIndex + "�̃A�C�e���͂܂����������Ă��Ȃ�!");
            #endif
            return;
        }
        else if (EquipmentItem[equipIndex].MEndurance == 0)
        {
            //�����𒆒f����
            #if CHECK
                Debug.Log("index��" + equipIndex + "�̃A�C�e���͂����g���Ȃ�!");
            #endif
            return;
        }

        itemsBoxS.UseItems(this ,EquipmentItem[equipIndex]);
        EquipmentItem[equipIndex].MActive = !(EquipmentItem[equipIndex].MActive);
        #if CHECK
        Debug.Log("index��" + equipIndex + "��ID��" + EquipmentItem[equipIndex].MItemID + "�A�C�e�����g����");
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
