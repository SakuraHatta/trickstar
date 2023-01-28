using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{

    private RectTransform rectTransform;

    private Vector2 BASE_POSITION;  //���̈ʒu

    [Header("Parameta")]
    //[SerializeField]
    private Vector2 SCALE;  //�摜�̑傫��
    [SerializeField]
    private Vector2 moveSpeed;    //�ړ����鑬��

    private Vector2 movelarge;    //�ړ���������

    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        BASE_POSITION = rectTransform.anchoredPosition;
        SCALE.x = Screen.width;
        SCALE.y = Screen.height;
    }

    private void FixedUpdate()
    {
        //�ړ����x�ɍ��킹�Ĉړ����Ă���
        rectTransform.Translate(moveSpeed.x, moveSpeed.y, 0.0f);
        //xy���ꂼ��̈ړ����������𑪂�
        movelarge.x += moveSpeed.x;
        movelarge.y += moveSpeed.y;

        //x�̈ړ��������T�C�Yx���傫���Ƃ�
        if (movelarge.x * movelarge.x > SCALE.x * SCALE.x)
        {
            rectTransform.anchoredPosition = new Vector2(BASE_POSITION.x, rectTransform.anchoredPosition.y);
            movelarge.x = 0.0f;
        }
        //y�̈ړ��������T�C�Yy���傫���Ƃ�
        else if (movelarge.y * movelarge.y > SCALE.y * SCALE.y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, BASE_POSITION.y);
            movelarge.y = 0.0f;
        }
    }
}
