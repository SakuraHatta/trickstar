using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private List<ItemBaseScript> ItemBoxList = new List<ItemBaseScript>();
    
    //引数のキャラクター型のオブジェクトに、引数のidのアイテムを発動させる
    public void UseItems(CharacterBase Char, BelongItemData belongItemData)
    {
        //もしそのアイテムが使用中なら
        if (belongItemData.MActive)
        {
            //そのアイテムの使用を停止し、処理をちゅうだんする
            //belongItemData.MActive = false;
            ItemBoxList[belongItemData.MItemID].StopItem(Char);
            return;
        }

        //もしそのアイテムを使用してないなら
        //クリエイトタイプのアイテムを使用した時
        if (Const.CREATE_TYPE == (ItemBoxList[belongItemData.MItemID].GetItemType() & Const.CREATE_TYPE))
        {
            //キャラクターが持っている他のクリエイトタイプのアイテムを止める
            foreach(BelongItemData bData in Char.GetItems())  //プレイヤーの持っているアイテム数だけ繰り返す    
            {
                if (bData.MItemID == Const.NO_ITEM) { continue; }

                //もしクリエイトタイプのアイテムを持ってたら
                if (Const.CREATE_TYPE == (ItemBoxList[bData.MItemID].GetItemType() & Const.CREATE_TYPE)
                    && belongItemData.MItemID != bData.MItemID && bData.MActive)
                {
                    bData.MActive = false;
                    ItemBoxList[bData.MItemID].StopItem(Char); //そのアイテムの使用を止める
                }
            }
        }

        //belongItemData.MActive = true;
        ItemBoxList[belongItemData.MItemID].ActiveItem(Char); //引数のアイテムIDのアイテムを使用する
    }
    //引数のキャラクター型のオブジェクトに、引数のidのアイテムの効果を取り除く
    public void PassiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].StopItem(Char);
    }

    //耐久地を減らすメゾット
    public void DecreaseCreate(CharacterBase Char)
    {
        //プレイヤーのアイテムの数だけ繰り返す
        foreach (BelongItemData bData in Char.GetItems())
        {
            //もしアイテムIDが何も持ってないなら処理を中断して次に回す
            if (bData.MItemID == Const.NO_ITEM) { continue; }

            //アイテムのタイプが[クリエイト]で使用中なら
            if (Const.CREATE_TYPE == (ItemBoxList[bData.MItemID].GetItemType() & Const.CREATE_TYPE) &&
                bData.MActive)
            {
                bData.MEndurance--;　//耐久力を減らす
                //もし耐久力が0になったら
                if (bData.MEndurance == 0)  
                {
                    PassiveItems(Char, bData.MItemID);  //そのアイテムの効果を取り消して未使用にする
                    bData.MActive = false;
                    break;
                }
            }
        }
    }
}
