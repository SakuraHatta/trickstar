using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Text,Image���g�p���邽�߂ɒǉ�

public class ItemCard : MonoBehaviour
{
    [SerializeField]
    private Text[] cardTexts = new Text[3]; //�J�[�h�ɂ��镶�� 
    [SerializeField]
    private Image cardImage;    //�J�[�h�̊G

    enum CardTextsE     //�J�[�h�̕����̎��
    {
        nameE,
        priceE,
        infoE
    }

    private void SetTexts<T>(CardTextsE textE, T text)//�J�[�h�̃e�L�X�g��ύX���郁�]�b�g
    {
        cardTexts[(int)textE].text = "" + text;
    }   
    private void SetImage(Sprite image)//�J�[�h�̊G��ύX���郁�]�b�g
    {
        cardImage.sprite = image;
    }

    public void SetCard(ItemData data)
    {
        SetTexts(CardTextsE.nameE, data.name);
        SetTexts(CardTextsE.priceE, data.price);
        SetTexts(CardTextsE.infoE, data.info);
        SetImage(data.image);
    }
}
