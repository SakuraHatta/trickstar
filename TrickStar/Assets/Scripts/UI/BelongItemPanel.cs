using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text,Image���g�p���邽�߂ɒǉ�


public class BelongItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;    //�A�C�e���̃C���[�W�摜

    private RectTransform thisTransform;    //���̃I�u�W�F�N�g�̈ʒu

    private Vector2 selectScale;            //�I�𒆂̃T�C�Y
    private Vector2 baseScale;                //��{�̃T�C�Y

    public void StartPanel() //�ŏ��Ɏ��s����郁�]�b�g
    {
        thisTransform = this.GetComponent<RectTransform>();
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

    public void ChangeImage(Sprite image)//�p�l���̃A�C�e���C���[�W��ύX���郁�]�b�g
    {
        itemImage.sprite = image;
    }   
}
