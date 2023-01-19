using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text,Imageを使用するために追加


public class BelongItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;    //アイテムのイメージ画像

    private RectTransform thisTransform;    //このオブジェクトの位置

    private Vector2 selectScale;            //選択中のサイズ
    private Vector2 baseScale;                //基本のサイズ

    public void StartPanel() //最初に実行されるメゾット
    {
        thisTransform = this.GetComponent<RectTransform>();
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

    public void ChangeImage(Sprite image)//パネルのアイテムイメージを変更するメゾット
    {
        itemImage.sprite = image;
    }   
}
