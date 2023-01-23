using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Text,Image���g�p���邽�߂ɒǉ�

public class ItemCard : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Text[] cardTexts = new Text[3]; //�J�[�h�ɂ��镶�� 
    [SerializeField]
    private Image cardImage;    //�J�[�h�̊G

    private enum CardTextsE     //�J�[�h�̕����̎��
    {
        nameE,
        priceE,
        infoE
    }
    private readonly Vector2 chooseOffset = new Vector2(0.0f, 20.0f); 

    private void SetTexts<T>(CardTextsE textE, T text)//�J�[�h�̃e�L�X�g��ύX���郁�]�b�g
    {
        cardTexts[(int)textE].text = "" + text;
    }   
    private void SetImage(Sprite image)//�J�[�h�̊G��ύX���郁�]�b�g
    {
        cardImage.sprite = image;
    }
    //�J�[�h��\������Ƃ��̃��]�b�g
    public void DrawCard(ItemData data)
    {
        SetTexts(CardTextsE.nameE, data.Name);
        SetTexts(CardTextsE.priceE, data.Price);
        SetTexts(CardTextsE.infoE, data.Info);
        SetImage(data.Image);
        rectTransform.anchoredPosition += new Vector2(0.0f, 200.0f);
    }   
    //�J�[�h���\���ɂ���Ƃ��̃��]�b�g
    public void HideCard()
    {
        rectTransform.anchoredPosition += new Vector2(0.0f, -200.0f);
    }                
    //�J�[�h���I������Ă���Ƃ��̏���
    public void ChooseCard()    
    {
        rectTransform.anchoredPosition += chooseOffset;
    }
    //�J�[�h���I������������ꂽ�Ƃ��̏���
    public void UnChooseCard()  
    {
        rectTransform.anchoredPosition -= chooseOffset;
    }
}
