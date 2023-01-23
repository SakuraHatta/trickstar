using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Text,Imageを使用するために追加

public class ItemCard : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Text[] cardTexts = new Text[3]; //カードにある文字 
    [SerializeField]
    private Image cardImage;    //カードの絵

    private enum CardTextsE     //カードの文字の種類
    {
        nameE,
        priceE,
        infoE
    }
    private readonly Vector2 chooseOffset = new Vector2(0.0f, 20.0f); 

    private void SetTexts<T>(CardTextsE textE, T text)//カードのテキストを変更するメゾット
    {
        cardTexts[(int)textE].text = "" + text;
    }   
    private void SetImage(Sprite image)//カードの絵を変更するメゾット
    {
        cardImage.sprite = image;
    }
    //カードを表示するときのメゾット
    public void DrawCard(ItemData data)
    {
        SetTexts(CardTextsE.nameE, data.Name);
        SetTexts(CardTextsE.priceE, data.Price);
        SetTexts(CardTextsE.infoE, data.Info);
        SetImage(data.Image);
        rectTransform.anchoredPosition += new Vector2(0.0f, 200.0f);
    }   
    //カードを非表示にするときのメゾット
    public void HideCard()
    {
        rectTransform.anchoredPosition += new Vector2(0.0f, -200.0f);
    }                
    //カードが選択されているときの処理
    public void ChooseCard()    
    {
        rectTransform.anchoredPosition += chooseOffset;
    }
    //カードが選択から解除されたときの処理
    public void UnChooseCard()  
    {
        rectTransform.anchoredPosition -= chooseOffset;
    }
}
