using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            playerCS.SetActive(false);
        }
        else 
        {
            //�|�[�Y��ʂ̎�
            gameState &= ~Const.PAUSE;
            Time.timeScale = Const.DEFAULT_TIME_SCALE;
            Time.fixedDeltaTime = fixedTimeScale * Time.timeScale;
            playerCS.SetActive(true);
        }
    }
    //�V���b�v��ʂ̃C�x���g
    private void ShopEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE)) { return; }

        if (Const.SHOP != (gameState & Const.SHOP) && shopS.CheckNearShop())
        {
            shopS.OpenShop();
            gameState |= Const.SHOP;
            playerCS.SetActive(false);
        }
    }
    //�A�C�e�����̃C�x���g
    private void ItemsEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE) || Const.SHOP == (gameState & Const.SHOP)) { return; }

        if (Const.ITEM != (gameState & Const.ITEM))
        {
            gameState |= Const.ITEM;
            belongItemS.OpenItems();
            belongItemS.ChangeColor();
        }
    }
    
    private void BackEvent()
    {
        if (Const.SHOP == (gameState & Const.SHOP))     //SHOP���Ȃ�SHOP��Ԃ�����
        {
            gameState &= ~Const.SHOP;
            shopS.ExitShop();
            playerCS.SetActive(true);
        }
        if (Const.ITEM == (gameState & Const.ITEM))  //�A�C�e�������J���Ă�����A�C�e���������
        {
            gameState &= ~Const.ITEM;
            belongItemS.CloseItems();
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
        if (Input.GetKeyDown(KeyCode.Space))
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
        if (Input.GetKeyDown(KeyCode.Backspace)) 
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

    //��������
    void Start()
    {
        this.fixedTimeScale = Time.fixedDeltaTime;  //fixedUpdate�̎��Ԃ�ۑ�����
        shopS.StartShop();  //�V���b�v�̏�������������
        belongItemS.StartBelongItem();  //�A�C�e�����̏�������������
    }
    //��ȃQ�[�����[�v
    void Update()
    {
        if (KeyEvents())
        {
            //���������ꂩ�̑���L�[���������Ƃ�
#if CHECK
            ShowGameState();    
#endif
        }

        if (Const.PAUSE == (gameState & Const.PAUSE)) { return; }

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
