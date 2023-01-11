using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    //�V���b�v�ɔ����Ă���J�[�h�̖��� 
    private const int CardNumbers = 3;  

    //�Q�[���i�s�ɕK�v��Script����
    [SerializeField]
    private ShopScript shopS;

    //�Q�[���i�s���̃p�����[�^�[
    private bool shopping = false;  //�����������̃t���O
    private bool paused = false;    //�|�[�Y�����̃t���O

    private void KeyEvents()//�L�[���������Ƃ��̏���
    {
        if (Input.GetKeyDown(KeyCode.Return))//Enter�L�[���������Ƃ��̏���   
        {
            if (!paused)
            {
                Debug.Log("�|�[�Y���");
            }
            else
            {
                Debug.Log("�|�[�Y����");
            }

            paused = !paused;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnterShop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))//ESC�L�[���������Ƃ��̏���   
        {
            Application.Quit(); //�Q�[�����I���
            Debug.Log("�Q�[���I��");
        }
    }

    private void EnterShop()//�V���b�v�ɓ��������̏���
    {
        shopS.Card();
    }

    void Update()   //��ȃQ�[�����[�v
    {
        KeyEvents();
    }
}
