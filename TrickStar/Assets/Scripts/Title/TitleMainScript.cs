using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMainScript : MonoBehaviour
{
    [Header("TextControllers")]
    [SerializeField]
    private List<UIController> TextCArray = new List<UIController>();//�^�C�g���̃e�L�X�g����
    //�^�C�g����ʂ̏��
    private uint titleState = Const.DEFAULT_T;  //�ʏ��Ԃ�����
    //�F
    private readonly Color NULL_COLOR = new Color(1.0f, 1.0f, 1.0f, 0.0f);  //�����F

    private float timeValue = 0.0f;

    //enter�L�[�����������̏���
    private void EnterEvent()
    {
        if (Const.FADE_T == (titleState & Const.FADE_T)) { return; }
        TextCArray[0].StartTween(TextCArray[0].GetPosition() + new Vector2(0.0f, 3000.0f));
        titleState |= Const.FADE_T;
    }
    //�L�[�̏�Ԕ���
    private void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterEvent();
        }
    }

    //��������
    private void Start()
    {
        foreach(UIController texts in TextCArray)
        {
            texts.StartText();
        }
    }
    //��{����
    private void Update()
    {
        KeyEvent();

        foreach (UIController texts in TextCArray)
        {
            texts.UpdateText();
        }

        if (Const.FADE_T == (titleState & Const.FADE_T))
        {
            timeValue += Time.deltaTime;
            if (timeValue > 2.0f)
            {
                SceneManager.LoadScene("MapScene");
            }
        }
    }
}
