using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    private List<ItemCard> cardSList = new List<ItemCard>();
    [SerializeField]
    private ItemListData ItemLD;

    private void Test1(int index)    //引数のアイテムのステータスを表示する
    {
        Debug.Log("アイテムID[" + ItemLD.ItemDataList[index].id + "]の名前は["
            + ItemLD.ItemDataList[index].name + "]です。"
            );

        Debug.Log("値段は[" + ItemLD.ItemDataList[index].price + "]です。");
        Debug.Log("説明文 : " + ItemLD.ItemDataList[index].info);
    }

    public void Card()
    {
        foreach (ItemCard cardS in cardSList)
        {
            cardS.SetCard(ItemLD.ItemDataList[0]);
        }
    }
}
