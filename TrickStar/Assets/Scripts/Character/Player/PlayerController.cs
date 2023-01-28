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

    private float jumptime;
    private int money;  //������

    //�A�j���[�V����
    public override void AnimationUpdate()
    {
        string playerstate = "none";

        //�A�C�e�������J���Ă��邩�̃t���O
        if (Const.INVENTORY == (state & Const.INVENTORY))
        {
            playerstate = "Items";
            animator.SetBool("Item", true);
            return;
        }
        else
        {
            animator.SetBool("Item", false);
        }

        //���ł��邩���m�F���郁�]�b�g
        if (Const.JUMP == (state & Const.JUMP))
        {
            playerstate = "Jump";
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        //�����Ă��邩���m�F���郁�]�b�g
        if (Const.WALK == (state & Const.WALK))
        {
            playerstate = "Walk";
            animator.SetBool("Walk", true);
        }
        else
        {
            playerstate = "Idel";
            animator.SetBool("Walk", false);
        }

#if CHECK
        Debug.Log("�v���C���[�̏�Ԃ� : " + playerstate + "�ł��B");
#endif
    }
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
        else if (Input.GetKey(KeyCode.D))//���Ɉړ�
        {
            Walk(1);
        }
        //�ړ��L�[�������Ă��Ȃ��Ƃ�
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //�����ړ��t���O�������Ă����炨�낷
            if (Const.WALK == (state & Const.WALK)) { state &= ~Const.WALK; }
        }

        //�X�y�[�X�L�[���������Ƃ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�v���C���[���W�����v��Ԃ���Ȃ��Ƃ�
            if (Const.JUMP != (state & Const.JUMP))    
            {
                state |= Const.JUMP;
                state |= Const.JUMPING;
            }
            //�󒆂Ń_�u���W�����v���ł���Ƃ�
            else if (Const.JUMP == (state & Const.JUMP) && Const.DOUBLEJUMP == (itemstate & Const.DOUBLEJUMP))    
            {
                if (airjump < limitairjump)
                SkyJump();
                airjump++;
            }
        }

        //�X�y�[�X�L�[�𗣂����Ƃ�
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Const.JUMPING != (state & Const.JUMPING)) { return; }
            Resetjump();
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
        //�����ړ��t���O�������Ă��Ȃ��Ȃ痧�Ă�
        if (Const.WALK != (state & Const.WALK)) { state |= Const.WALK; }
        
        //�����ő呬�x��葁�������珈���𒆒f
        if (rigid.x * rigid.x > speed * speed)
        {
            return;
            //if (rigid.x > 0.0f) //rigid�����̐��̎�
            //    rigid.x = speed;
            //else                //rigid�����̐��̎�
            //    rigid.x = -speed;
        }

        //�ړ�����͂̃x�N�g��������
        Vector2 walk = new Vector2(speed * direction * Time.deltaTime, 0.0f);
        walk *= Const.RIGID_TIMES;

        //�v���C���[�̌�����ύX
        this.transform.localScale = new Vector2(Const.PLAYER_SCALE * direction, Const.PLAYER_SCALE);

        //���ۂɗ͂�������
        rigidC.AddForce(walk, ForceMode2D.Force);
    }
    //�W�����v
    public override void Jump()
    {
        if (Const.JUMPING != (state & Const.JUMPING)) { return; }

        Vector2 jump = new Vector2(0.0f, jumppower * Time.deltaTime);
        jump *= Const.RIGID_TIMES;
        rigidC.AddForce(jump, ForceMode2D.Force);

        if (jumptime > Const.MAX_JUMP_TIME)
        {
            Resetjump();
            return;
        }

        jumptime += Time.deltaTime;
    }
    //�_�u���W�����v
    public override void SkyJump()
    {
        rigid.y = 0.0f;
        Vector2 jump = new Vector2(0.0f, jumppower);
        jump *= Const.RIGID_TIMES;
        rigidC.AddForce(jump, ForceMode2D.Force);
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

    //�W�����v���Ԃ����Z�b�g����
    private void Resetjump()
    {
        state &= ~Const.JUMPING;
        jumptime = 0.0f;
        rigid.y = 0.0f;
    }

    //�}�E�X�̈ʒu�Ƀ^�C����\�������鏈��
    public void SelectTile()
    {
        tilemapS.ShowSelectTile(itemstate);
    }

    //�����Ă��邩�m�F���鏈��
    public bool CheckAlive() { 
        if (Const.ALIVE == (state & Const.ALIVE)) { return true; }  //�����Ă�����true��Ԃ�
        return false;   //�����ĂȂ�������false��Ԃ�
    }
    //�_���[�W��^���鏈��
    public void TakeDamage()
    {
        if (Const.INVINCLEBLE == (itemstate & Const.INVINCLEBLE)) {
#if CHECK
            Debug.Log("���G��ԂȂ̂Ń_���[�W��H���Ȃ�!");
#endif
            return;
        }
#if CHECK
        Debug.Log("�_���[�W���󂯂�!");
#endif
        //hp��1�����炷
        hp--;   
        if (hp <= 0)
        {
            state &= ~Const.ALIVE;  //����hp��0�ȉ��Ȃ琶���t���O��������
        }
    }
    //�ŏ��ɖ߂������̏���
    public void Restart()
    {
        state |= Const.ALIVE + Const.ACTIVE;    //�����t���O��on�ɂ���
        itemstate = 0b0000;     //�A�C�e����Ԃ��f�t�H���g�ɂ���
        //�����Ă���A�C�e���̌��ʂ������āA�S�ă��Z�b�g����
        foreach(BelongItemData bData in EquipmentItem)
        {
            itemsBoxS.PassiveItems(this, bData);
            bData.ResetData();
        }
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

        rigidC.velocity = rigid;    //��������rigid��ݒ肷��
    }

    //item���J�������̏���
    public void OpenItem()
    {
        //�A�C�e�����J���ĂȂ��Ƃ�
        if (Const.INVENTORY != (state & Const.INVENTORY))
        {
            state |= Const.INVENTORY;
        }
        //�A�C�e�����J���Ă���Ƃ�
        else if (Const.INVENTORY == (state & Const.INVENTORY))
        {
            state &= ~Const.INVENTORY;
        }
    }
}
