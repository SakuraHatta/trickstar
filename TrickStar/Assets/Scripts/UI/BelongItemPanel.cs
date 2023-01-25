using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text,Image���g�p���邽�߂ɒǉ�


public class BelongItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;    //�A�C�e���̃C���[�W�摜

    [SerializeField]
    private Text enduranceT;    //�ϋv�l�̃e�L�X�g

    private RectTransform thisTransform;    //���̃I�u�W�F�N�g�̈ʒu

    private Vector2 selectScale;    //�I�𒆂̃T�C�Y
    private Vector2 baseScale;  //��{�̃T�C�Y

    private Vector2 basePos;    //���̈ʒu
    private Vector2 activePos;  //�\�����̈ʒu

    public void StartPanel() //�ŏ��Ɏ��s����郁�]�b�g
    {
        thisTransform = this.GetComponent<RectTransform>();
        basePos = thisTransform.anchoredPosition;
        activePos = thisTransform.anchoredPosition + new Vector2(0f, 150.0f);
        baseScale = thisTransform.localScale;
        selectScale = baseScale + new Vector2(-0.2f, -0.2f);
    }

    public void Choose()//�I�����ꂽ�Ƃ��̏���
    {
        thisTransform.localScale = selectScale;
    }   
    public void UnChoose()//�I������O�ꂽ���̏���
    {
        thisTransform.localScale = baseScale;
    }   

    //�p�l���̃A�C�e���C���[�W��ύX���郁�]�b�g
    public void ChangeImage(Sprite image)
    {
        itemImage.sprite = image;
    }
    //�p�l���̃A�C�e���̐F��ς��郁�]�b�g
    public void ChangeColor(Color color1)
    {
        itemImage.color = color1;
    }   
    //�A�C�e���̑ϋv�l��\�����郁�]�b�g
    public void ChangeEndurance(int value)
    {
        //�����A�C�e���������Ă��Ȃ����ϋv�l���Ȃ��A�C�e���Ȃ�
        if (value == Const.NO_ITEM || value == Const.NO_ENDURANCE) {
            enduranceT.text = null; //�e�L�X�g�𓧖��ɂ���
            return;
        }
        enduranceT.text = value.ToString();
    }   
}
