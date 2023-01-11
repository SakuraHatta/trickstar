using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Text,Imageを使用するために追加

public class ItemCard : MonoBehaviour
{
    [SerializeField]
    private Text[] cardTexts = new Text[3]; //カードにある文字 
    [SerializeField]
    private Image cardImage;    //カードの絵

    enum CardTextsE     //カードの文字の種類
    {
        nameE,
        priceE,
        infoE
    }

    private void SetTexts<T>(CardTextsE textE, T text)//カードのテキストを変更するメゾット
    {
        cardTexts[(int)textE].text = "" + text;
    }   
    private void SetImage(Sprite image)//カードの絵を変更するメゾット
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
