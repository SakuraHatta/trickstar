using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    //�Q�[���i�s�ɕK�v��Script����
    [SerializeField]
    private PlayerController plrControllerS;
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private ShopScript shopS;

    //�Q�[���i�s���̃p�����[�^�[
    private uint gameState = 0b0000;
    private const uint SHOP = 0b0001;  //�����������̃t���O
    private const uint ITEMS = 0b0010; //�C���x���g�����J���Ă��邩�̃t���O
    private const uint PAUSE = 0b0100;    //�|�[�Y�����̃t���O

    private void ShowGameState()
    {
        switch (gameState)
        {
            case SHOP:
                Debug.Log("�V���b�v���ł�");
                break;
            case ITEMS:
                Debug.Log("�C���x���g�����J���Ă���");
                break;
            case PAUSE:
                Debug.Log("�|�[�Y��");
                break;
            default:
                break;
        }
    }

    private void KeyEvents()//�L�[���������Ƃ��̏���
    {
        if (Input.GetKeyDown(KeyCode.Return))//Enter�L�[���������Ƃ��̏���   
        {
            if (PAUSE != (gameState & PAUSE))
            {
                gameState |= PAUSE;
                Debug.Log("�|�[�Y���");
            }
            else
            {
                gameState &= ~PAUSE;
                Debug.Log("�|�[�Y����");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PAUSE == (gameState & PAUSE)) { return; }

            if (SHOP != (gameState & SHOP))
            {
                gameState |= SHOP;
                EnterShop();
            }
            else
            {
                gameState &= ~SHOP;
                ExitShop();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PAUSE == (gameState & PAUSE)) { return; }

            if (ITEMS != (gameState & ITEMS))
                gameState |= ITEMS;
            else
                gameState &= ~ITEMS;
        }

        if (Input.GetKeyDown(KeyCode.Escape))//ESC�L�[���������Ƃ��̏���   
        {
            Application.Quit(); //�Q�[�����I���
            Debug.Log("�Q�[���I��");
        }

        ShowGameState();
    }

    private void EnterShop()//�V���b�v�ɓ��������̏���
    {
        shopS.OpenShop();
    }
    private void ExitShop()
    {
        shopS.ExitShop();
    }

    void Update()   //��ȃQ�[�����[�v
    {
        KeyEvents();

        if (SHOP == (gameState & SHOP) && PAUSE != (gameState & PAUSE))
        {
            shopS.UpdateShop();
        }
        else if (ITEMS == (gameState & ITEMS) && PAUSE != (gameState & PAUSE))
        {
            belongItemS.UpdateBelongItem();
        }
    }
}
