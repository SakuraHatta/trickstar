using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    [Header("Scripts")]
    //�Q�[���i�s�ɕK�v��Script����
    [SerializeField]
    private PlayerController playerCS; //PlayerContoller�̃X�N���v�g
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private ShopScript shopS;

    //�Q�[���i�s���̃p�����[�^�[
    private uint gameState = 0b0001;

    private float fixedTimeScale;   //FixedUpdate�̎���
    private Vector2 startPos;       //�ŏ��̃X�^�[�g�n�_�̍��W

    private void ShowGameState() //���̃Q�[���̏󋵂�\�����郁�]�b�g 
    {
#if CHECK
        Debug.Log("�Q�[���̏�� : " + gameState);
#endif
    }

    //�|�[�Y��ʂ̃C�x���g
    private void pauseEvent() {
        //�|�[�Y��ʂ���Ȃ��Ƃ�
        if (Const.PAUSE != (gameState & Const.PAUSE))   
        {
            gameState |= Const.PAUSE;
            Time.timeScale = 0.0f;
            ChangePlayerActive(false);
        }
        else 
        {
            //�|�[�Y��ʂ̎�
            gameState &= ~Const.PAUSE;
            Time.timeScale = Const.DEFAULT_TIME_SCALE;
            Time.fixedDeltaTime = fixedTimeScale * Time.timeScale;
            ChangePlayerActive(false);
        }
    }
    //�V���b�v��ʂ̃C�x���g
    private void ShopEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE) || Const.ITEM == (gameState & Const.ITEM)) { return; }

        if (Const.SHOP != (gameState & Const.SHOP) && shopS.CheckNearShop())
        {
            shopS.OpenShop();
            gameState |= Const.SHOP;
            ChangePlayerActive(false);
        }
    }
    //�A�C�e�����̃C�x���g
    private void ItemsEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE) || Const.SHOP == (gameState & Const.SHOP)) { return; }

        if (Const.ITEM != (gameState & Const.ITEM) && Const.JUMP != (playerCS.State & Const.JUMP))
        {
            gameState |= Const.ITEM;
            belongItemS.OpenItems();
            belongItemS.ChangeColor();
            playerCS.OpenItem();
            ChangePlayerActive(false);
        }
    }
    //�o�b�N�X�y�[�X�������Ƃ��̃C�x���g
    private void BackEvent()
    {
        if (Const.SHOP == (gameState & Const.SHOP))     //SHOP���Ȃ�SHOP��Ԃ�����
        {
            gameState &= ~Const.SHOP;
            shopS.ExitShop();
            ChangePlayerActive(true);
        }
        if (Const.ITEM == (gameState & Const.ITEM))  //�A�C�e�������J���Ă�����A�C�e���������
        {
            gameState &= ~Const.ITEM;
            playerCS.OpenItem();
            ChangePlayerActive(true);
        }
    }
    //�L�[���������Ƃ��̏���
    private bool KeyEvents()
    {
        //Enter�L�[���������Ƃ��̏��� 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pauseEvent();
            return true;
        }
        //�X�y�[�X�L�[�����������̏���
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShopEvent();
            return true;
        }
        //E���������Ƃ��̏���
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            ItemsEvent();
            return true;
        }
        //�o�b�N�X�y�[�X���������Ƃ�
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BackEvent();
            return true;
        }
        //ESC�L�[���������Ƃ��̏��� 
        if (Input.GetKeyDown(KeyCode.Escape))  
        {
            Application.Quit(); //�Q�[�����I���
            Debug.Log("�Q�[���I��");
            return true;
        }

        return false;
    }

    //�v���C���[�̋N���t���O�������̒l�ɕύX���郁�]�b�g
    private void ChangePlayerActive(bool b)
    {
        playerCS.SetActive(b);
    }
    //�v���C���[�����S�������ɂ��鏈��
    private void RestartGame()
    {
        playerCS.transform.position = startPos;
        playerCS.Restart();
        belongItemS.OpenItems();
        belongItemS.ChangeColor();
        belongItemS.CloseItems();
    }

    //��������
    private void Start()
    {
        this.fixedTimeScale = Time.fixedDeltaTime;  //fixedUpdate�̎��Ԃ�ۑ�����
        shopS.StartShop();  //�V���b�v�̏�������������
        belongItemS.StartBelongItem();  //�A�C�e�����̏�������������
        startPos = playerCS.transform.position; //�X�^�[�g�n�_���n�܂�̈ʒu�ɂ���
    }
    //��ȃQ�[�����[�v
    private void Update()
    {
        if (KeyEvents())
        {
            //���������ꂩ�̑���L�[���������Ƃ�
#if CHECK
            ShowGameState();    
#endif
        }

        if (Const.PAUSE == (gameState & Const.PAUSE)) { return; }

        //�v���C���[�����S�����ꍇ
        if (!(playerCS.CheckAlive()))
        {
            //RestartGame();
            SceneManager.LoadScene("MapScene");
        }

        //�V���b�v��ʂ��J���Ă���ꍇ
        if (Const.SHOP == (gameState & Const.SHOP))
        {
            shopS.UpdateShop();
        }

        //�A�C�e����ʂ��J���Ă���ꍇ
        if (Const.ITEM == (gameState & Const.ITEM))
        {
            belongItemS.UpdateBelongItem();
        }
    }
}
