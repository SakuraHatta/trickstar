using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject shopSetObject;  //ショップのUIが入っているオブジェクト

    [SerializeField]
    private RectTransform[] rectTransforms;   //使用するRectTransfromのは配列
    private Vector2[] basePositions;        //元の位置
    private Vector2[] goalPositions;        //移動先

    private enum transformE
    {
        InfoTransform,  //情報パネルのtransform
        ItemsetTransform    //アイテムセットのtransform
    }   //Rtransform配列にある、要素のそれぞれの名前

    [SerializeField]
    private Text[] Texts;  //使用するテキストの配列
    private enum textE
    {
        NameT,  //アイテムの名前を書くテキスト
        InfoT   //アイテムの情報を描くテキスト
    }   //テキスト配列にある、要素のそれぞれ名前

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

    private int[] SellItemID = new int[Const.CARD_NUMBER];  //売っているアイテムのid配列

    [Space(10)]
    [Header("Data")]
    [SerializeField]
    private ItemListData ItemLD;    //ItemListDataのDataList

    [Space(10)]
    [Header("Animation")]
    [SerializeField]
    private AnimationCurve curve;

    private int Mselected;  //選択中のカードのindex
    private bool animation; //アニメーションをしているかどうか
    private float animTime; //アニメーションの時間経過

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

        ChangeText(textE.NameT, ItemLD.ItemDataList[SellItemID[Mselected]].Name);
        ChangeText(textE.InfoT, ItemLD.ItemDataList[SellItemID[Mselected]].Info);
    }
    //カードを選ぶ処理
    private void KeyController()   
    {
        //左に選択を移動するとき
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            ChangeSelect(-1);
        }
        //右に選択を移動するとき
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            ChangeSelect(1);
        }
        //選択するとき
        else if (Input.GetKeyDown(KeyCode.E))
        {
            BuyItem();
        }
    }

    //最初の処理
    public void StartShop()
    {
        basePositions = new Vector2[rectTransforms.Length];
        goalPositions = new Vector2[rectTransforms.Length];

        //rectTransform配列の要素数の分だけ繰り返す
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            //それぞれの元の位置を開始地点に設定する
            basePositions[i] = rectTransforms[i].anchoredPosition;
        }
        //それぞれの移動先の座標を決める
        goalPositions[(int)transformE.InfoTransform] =
            basePositions[(int)transformE.InfoTransform] + new Vector2(-280.0f, 0.0f);
        goalPositions[(int)transformE.ItemsetTransform] = 
            basePositions[(int)transformE.ItemsetTransform] + new Vector2(-140.0f, 0.0f);

        //マップにあるショップの数だけ繰り返す
        foreach (ShopObjectScript shop in ShopObjectList)
        {
            shop.StartShopObject(); //shopObjectの最初の処理を実行する
        }
        //ショップに売っている分の数だけ繰り返す
        foreach(ItemCard items in cardSList)
        {
            items.StartCard();
        }
    }

    //テキストを変更するスクリプト
    private void ChangeText(textE type, string s)
    {
        Texts[(int)type].text = s;
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
        shopSetObject.SetActive(true);

        animation = true;
        animTime = 0.0f;

        int index = 0;
        foreach (ItemCard cardS in cardSList)   //cardSListの要素数だけ繰り返す
        {
            cardS.DrawCard(ItemLD.ItemDataList[SellItemID[index]]); //カードにデータリストの情報を書かせる
            index++;
        }
    }
    //店を閉めるメゾット
    public void ExitShop()
    {
        shopSetObject.SetActive(false);

        cardSList[Mselected].UnChooseCard();
        foreach (ItemCard cardS in cardSList)   //cardSListの要素数だけ繰り返す
        {
            cardS.HideCard(); //カードを隠す
        }

        //テキストの中身を空にする
        ChangeText(textE.NameT, null);
        ChangeText(textE.InfoT, null);
    }

    //実際にアニメーション経過時間に合わせて移動させるメゾット
    private void MoveObjects()
    {
        for (int i = 0; i < 2; i++)
        {
            rectTransforms[i].anchoredPosition = Vector2.Lerp(basePositions[i], goalPositions[i], curve.Evaluate(animTime));
        }
    }
    //アニメーションをしているかを確認するメゾット
    private bool PlayAnim()
    {
        animTime += Time.deltaTime;
        MoveObjects();

        if (animTime > 1.0f || animTime < 0.0f)
        {
            return false;
        }
        return true;
    }

    //ショップ中なら処理をする関数
    public void UpdateShop()
    {
        if (animation)
        {
            if (!PlayAnim())
            {
                Mselected = 0;
                ChangeSelect(0);
                cardSList[Mselected].ChooseCard();

                animation = false;
            }
            return;
        }

        KeyController();
    }
}
