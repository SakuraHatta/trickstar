using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    //Referense
    [Header("Scripts")]
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private PlayerController playerCS;

    [Space(10)]
    [Header("Data")]
    [SerializeField]
    private List<ItemCard> cardSList = new List<ItemCard>();
    [SerializeField]
    private ItemListData ItemLD;

    private readonly int CARD_NUMBER;  //ショップのカードの数
    private int Mselected;  //選択中のカードのindex

    public ShopScript() //ShopScriptのコンストラクタ
    {
        CARD_NUMBER = 3 - 1;
        Mselected = 0;
    }   

    //private void Test1(int index)    //引数のアイテムのステータスを表示する
    //{
    //    Debug.Log("アイテムID[" + ItemLD.ItemDataList[index].id + "]の名前は["
    //        + ItemLD.ItemDataList[index].name + "]です。"
    //        );

    //    Debug.Log("値段は[" + ItemLD.ItemDataList[index].price + "]です。");
    //    Debug.Log("説明文 : " + ItemLD.ItemDataList[index].info);
    //}

    private void BuyItem()  //アイテムを買う処理
    {
        playerCS.AddItem(0);
    }

    private void ChangeSelect(int change) //Mselectedを引数の値だけずらす処理
    {
        cardSList[Mselected].UnChooseCard();    //既に選択されているカード元に戻す

        Mselected += change;
        if (Mselected > CARD_NUMBER)   //Mselectedがカードの枚数を超えてるとき
        {
            Mselected = 0;  //選択カードを0にする
        }
        else if (Mselected < 0) //Eselectedが0より下の時
        {
            Mselected = CARD_NUMBER;   //Eselectedを最大値にする;
        }

        cardSList[Mselected].ChooseCard();  //新しく選択されたカードを動かす
    }
    private void KeyController()   //カードを選ぶ処理
    {
        //左に選択を移動するとき
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSelect(-1);
        }
        //右に選択を移動するとき
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeSelect(1);
        }
        //選択するとき
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuyItem();
        }
    }

    public void OpenShop()//店を開くメゾット
    {
        Mselected = 0;
        cardSList[Mselected].ChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSListの要素数だけ繰り返す
        {
            cardS.DrawCard(ItemLD.ItemDataList[0]); //カードにデータリストの情報を書かせる
        }
    }
    public void ExitShop()//店を閉めるメゾット
    {
        cardSList[Mselected].UnChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSListの要素数だけ繰り返す
        {
            cardS.HideCard(); //カードを隠す
        }
    }

    public void UpdateShop()//ショップ中なら処理をする関数
    {
        KeyController();
    }
}
