using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Text,Imageを使用するために追加

public class ItemCard : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image cardImage;    //カードの絵

    private Image thisImage;

    //元のサイズ
    private Vector2 baseScale;
    //元の色
    private Color baseColor;
    //選択した時のサイズ
    private readonly Vector2 CHOOSE_SCALE = new Vector2(1.5f, 1.5f);
    //選択した時の色
    private readonly Color CHOOSE_COLOR = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private void SetImage(Sprite image)//カードの絵を変更するメゾット
    {
        cardImage.sprite = image;
    }

    //初期処理
    public void StartCard()
    {
        thisImage = this.GetComponent<Image>();
        baseColor = thisImage.color;
        baseScale = rectTransform.localScale;
    }

    //カードを表示するときのメゾット
    public void DrawCard(ItemData data)
    {
        SetImage(data.Image);
        //rectTransform.anchoredPosition += new Vector2(0.0f, 200.0f);
    }   
    //カードを非表示にするときのメゾット
    public void HideCard()
    {
        //rectTransform.anchoredPosition += new Vector2(0.0f, -200.0f);
    }                
    //カードが選択されているときの処理
    public void ChooseCard()    
    {
        rectTransform.localScale = CHOOSE_SCALE;
        thisImage.color = CHOOSE_COLOR;
    }
    //カードが選択から解除されたときの処理
    public void UnChooseCard()  
    {
        rectTransform.localScale = baseScale;
        thisImage.color = baseColor;
    }
}
