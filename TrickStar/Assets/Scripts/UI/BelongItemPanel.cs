using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text,Imageを使用するために追加


public class BelongItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;    //アイテムのイメージ画像

    public void ChangeImage(Sprite image)
    {
        itemImage.sprite = image;
    }   //アイテムのイメージを変更っするメゾット
}
