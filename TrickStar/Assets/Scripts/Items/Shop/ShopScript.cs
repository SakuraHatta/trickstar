using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    //Referense
    [Header("Scripts")]
    [SerializeField]
    private BelongItem belongItemS; //belongitemのScript
    [SerializeField]
    private PlayerController playerCS;  //PlayerControllerのScript

    [Space(10)]
    [Header("Lists")]
    [SerializeField]
    private List<ItemCard> cardSList = new List<ItemCard>();    //アイテムカードのリスト
    [SerializeField]
    private List<ShopObjectScript> ShopObjectList = new List<ShopObjectScript>();   //ショップオブジェクトのリスト

    private int[] SellItemID = new int[Const.CARD_NUMBER];

    [Space(10)]
    [Header("Data")]
    [SerializeField]
    private ItemListData ItemLD;    //ItemListDataのDataList

    private int Mselected;  //選択中のカードのindex

    public ShopScript() //ShopScriptのコンストラクタ
    {
        Mselected = 0;
    }   

    //アイテムを買う処理
    private void BuyItem()  
    {
        //これ以上アイテムが持てないときは処理を中断する
        //(第一引数 : アイテムID, 第二引数 : アイテムの耐久地)
        if (!(playerCS.AddItem(SellItemID[Mselected],
            ItemLD.ItemDataList[SellItemID[Mselected]].Endurance))) {
        #if CHECK
            Debug.Log("これ以上アイテムは持てない!");
        #endif 
            return;
        }

        belongItemS.OpenItems();    //アイテムのイメージを更新する
        belongItemS.ChangeColor();  //アイテム欄の色を変える

        #if CHECK
                Debug.Log(ItemLD.ItemDataList[SellItemID[Mselected]].Name + "を購入した");
        #endif
    }
    //Mselectedを引数の値だけずらす処理
    private void ChangeSelect(int change) 
    {
        cardSList[Mselected].UnChooseCard();    //既に選択されているカード元に戻す

        Mselected += change;
        if (Mselected > Const.CARD_NUMBER - 1)   //Mselectedがカードの枚数を超えてるとき
        {
            Mselected = 0;  //選択カードを0にする
        }
        else if (Mselected < 0) //Eselectedが0より下の時
        {
            Mselected = Const.CARD_NUMBER - 1;   //Eselectedを最大値にする;
        }

        cardSList[Mselected].ChooseCard();  //新しく選択されたカードを動かす
    }
    //カードを選ぶ処理
    private void KeyController()   
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

    //最初の処理
    public void StartShop()
    {
        //マップにあるショップの数だけ繰り返す
        foreach(ShopObjectScript shop in ShopObjectList)
        {
            shop.StartShopObject(); //shopObjectの最初の処理を実行する
        }
    }

    //近くに店があるか
    public bool CheckNearShop()
    {
        foreach(ShopObjectScript shop in ShopObjectList)
        {
            if (shop.CheckDistance(playerCS.GetPosition()))
            {
                SetSellItemID(shop);
                return true;
            }
        }
        return false;
    }
    //売るアイテムのIDを変更する
    public void SetSellItemID(ShopObjectScript shopObj)
    {
        int index = 0;
        foreach(int sellId in shopObj.GetIDArray())
        {
            SellItemID[index] = sellId;
            index++;
        }
    }
    //店を開くメゾット
    public void OpenShop()
    {
        int index = 0;
        Mselected = 0;
        cardSList[Mselected].ChooseCard();

        foreach (ItemCard cardS in cardSList)   //cardSListの要素数だけ繰り返す
        {
            cardS.DrawCard(ItemLD.ItemDataList[SellItemID[index]]); //カードにデータリストの情報を書かせる
            index++;
        }

    }
    //店を閉めるメゾット
    public void ExitShop()
    {
        cardSList[Mselected].UnChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSListの要素数だけ繰り返す
        {
            cardS.HideCard(); //カードを隠す
        }
    }
    //ショップ中なら処理をする関数
    public void UpdateShop()
    {
        KeyController();
    }
}
