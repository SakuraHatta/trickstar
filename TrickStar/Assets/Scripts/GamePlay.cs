using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    //�Q�[���i�s�ɕK�v��Script����
    [SerializeField]
    private Player playerS;
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private ShopScript shopS;

    //�Q�[���i�s���̃p�����[�^�[
    private uint gameState = 0b0001;
    private const uint DEFAULT = 0b0001;    //���ʂ̏��
    private const uint SHOP = 0b0010;  //�����������̃t���O
    private const uint ITEMS = 0b0100; //�C���x���g�����J���Ă��邩�̃t���O
    private const uint PAUSE = 0b1000;    //�|�[�Y�����̃t���O

    private float fixedTimeScale;   //FixedUpdate�̎���

    private void ShowGameState() //���̃Q�[���̏󋵂�\�����郁�]�b�g 
    {
        Debug.Log(gameState);
    }

    private bool KeyEvents()//�L�[���������Ƃ��̏���
    {
        if (Input.GetKeyDown(KeyCode.Return))//Enter�L�[���������Ƃ��̏���   
        {
            if (PAUSE != (gameState & PAUSE))
            {
                gameState |= PAUSE;
                Time.timeScale = 0.0f;
            }
            else
            {
                gameState &= ~PAUSE;
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = fixedTimeScale * Time.timeScale;
            }

            return true;
        }

        if (Input.GetKeyDown(KeyCode.Space))//�X�y�[�X�L�[�����������̏���
        {
            if (PAUSE == (gameState & PAUSE)) { return false; }

            if (SHOP != (gameState & SHOP))
            {
                gameState |= SHOP;
                EnterShop();
            }

            return true;
        }

        if (Input.GetKeyDown(KeyCode.E)) //E���������Ƃ��̏���
        {
            if (PAUSE == (gameState & PAUSE) || SHOP == (gameState & SHOP)) { return false; }

            if (ITEMS != (gameState & ITEMS))
            {
                gameState |= ITEMS;
                belongItemS.OpenItems();
            }

            return true;
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) //�o�b�N�X�y�[�X���������Ƃ�
        {
            if (SHOP == (gameState & SHOP))     //SHOP���Ȃ�SHOP��Ԃ�����
            {
                gameState &= ~SHOP;
                ExitShop();
            }
            if (ITEMS == (gameState & ITEMS))  //�A�C�e�������J���Ă�����A�C�e���������
            {
                gameState &= ~ITEMS;
                belongItemS.CloseItems();
            }
            return true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))//ESC�L�[���������Ƃ��̏���   
        {
            Application.Quit(); //�Q�[�����I���
            Debug.Log("�Q�[���I��");
            return true;
        }

        return false;
    }

    private void EnterShop()//�V���b�v�ɓ��������̏���
    {
        shopS.OpenShop();
    }
    private void ExitShop()//�V���b�v����o���Ƃ��̏���
    {
        shopS.ExitShop();
    }

    void Start()
    {
        this.fixedTimeScale = Time.fixedDeltaTime;
        belongItemS.StartBelongItem();
    }

    void Update()   //��ȃQ�[�����[�v
    {
        if (KeyEvents())
        {
            //���������ꂩ�̑���L�[���������Ƃ�
            ShowGameState();
        }

        if (PAUSE == (gameState & PAUSE)) { return; }

        //�ʏ��ʂƃA�C�e����ʂȂ�v���C���[�𑀍�ł���悤�ɂ���
        if (SHOP != (gameState & SHOP))
        {
            playerS.UpdatePlayer();
        }

        //�V���b�v��ʂ̏ꍇ
        if (SHOP == (gameState & SHOP))
        {
            shopS.UpdateShop();
        }
        //�A�C�e����ʂ̏ꍇ
        else if (ITEMS == (gameState & ITEMS))
        {
            belongItemS.UpdateBelongItem();
        }
    }
}
