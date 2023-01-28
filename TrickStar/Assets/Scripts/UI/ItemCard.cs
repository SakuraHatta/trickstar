using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Text,Image���g�p���邽�߂ɒǉ�

public class ItemCard : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image cardImage;    //�J�[�h�̊G

    private Image thisImage;

    //���̃T�C�Y
    private Vector2 baseScale;
    //���̐F
    private Color baseColor;
    //�I���������̃T�C�Y
    private readonly Vector2 CHOOSE_SCALE = new Vector2(1.5f, 1.5f);
    //�I���������̐F
    private readonly Color CHOOSE_COLOR = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private void SetImage(Sprite image)//�J�[�h�̊G��ύX���郁�]�b�g
    {
        cardImage.sprite = image;
    }

    //��������
    public void StartCard()
    {
        thisImage = this.GetComponent<Image>();
        baseColor = thisImage.color;
        baseScale = rectTransform.localScale;
    }

    //�J�[�h��\������Ƃ��̃��]�b�g
    public void DrawCard(ItemData data)
    {
        SetImage(data.Image);
        //rectTransform.anchoredPosition += new Vector2(0.0f, 200.0f);
    }   
    //�J�[�h���\���ɂ���Ƃ��̃��]�b�g
    public void HideCard()
    {
        //rectTransform.anchoredPosition += new Vector2(0.0f, -200.0f);
    }                
    //�J�[�h���I������Ă���Ƃ��̏���
    public void ChooseCard()    
    {
        rectTransform.localScale = CHOOSE_SCALE;
        thisImage.color = CHOOSE_COLOR;
    }
    //�J�[�h���I������������ꂽ�Ƃ��̏���
    public void UnChooseCard()  
    {
        rectTransform.localScale = baseScale;
        thisImage.color = baseColor;
    }
}
