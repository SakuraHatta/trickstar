using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text,Imageを使用するために追加


public class BelongItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;    //アイテムのイメージ画像

    [SerializeField]
    private Text enduranceT;    //耐久値のテキスト

    private RectTransform thisTransform;    //このオブジェクトの位置

    private Vector2 selectScale;    //選択中のサイズ
    private Vector2 baseScale;  //基本のサイズ

    private Vector2 basePos;    //元の位置
    private Vector2 activePos;  //表示中の位置

    public void StartPanel() //最初に実行されるメゾット
    {
        thisTransform = this.GetComponent<RectTransform>();
        basePos = thisTransform.anchoredPosition;
        activePos = thisTransform.anchoredPosition + new Vector2(0f, 150.0f);
        baseScale = thisTransform.localScale;
        selectScale = baseScale + new Vector2(-0.2f, -0.2f);
    }

    public void Choose()//選択されたときの処理
    {
        thisTransform.localScale = selectScale;
    }   
    public void UnChoose()//選択から外れた時の処理
    {
        thisTransform.localScale = baseScale;
    }   

    //パネルのアイテムイメージを変更するメゾット
    public void ChangeImage(Sprite image)
    {
        itemImage.sprite = image;
    }
    //パネルのアイテムの色を変えるメゾット
    public void ChangeColor(Color color1)
    {
        itemImage.color = color1;
    }   
    //アイテムの耐久値を表示するメゾット
    public void ChangeEndurance(int value)
    {
        //もしアイテムを持っていないか耐久値がないアイテムなら
        if (value == Const.NO_ITEM || value == Const.NO_ENDURANCE) {
            enduranceT.text = null; //テキストを透明にする
            return;
        }
        enduranceT.text = value.ToString();
    }   
}
