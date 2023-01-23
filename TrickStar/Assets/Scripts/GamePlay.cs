using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    //ゲーム進行に必要なScriptたち
    [SerializeField]
    private Player playerS;
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private ShopScript shopS;

    //ゲーム進行中のパラメーター
    private uint gameState = 0b0001;
    private const uint DEFAULT = 0b0001;    //普通の状態
    private const uint SHOP = 0b0010;  //買い物中かのフラグ
    private const uint ITEMS = 0b0100; //インベントリを開いているかのフラグ
    private const uint PAUSE = 0b1000;    //ポーズ中かのフラグ

    private float fixedTimeScale;   //FixedUpdateの時間

    private void ShowGameState() //今のゲームの状況を表示するメゾット 
    {
        Debug.Log("ゲームの状態 : " + gameState);
    }

    private bool KeyEvents()//キーを押したときの処理
    {
        //Enterキーを押したときの処理 
        if (Input.GetKeyDown(KeyCode.Return))  
        {
            if (PAUSE != (gameState & PAUSE))
            {
                gameState |= PAUSE;
                Time.timeScale = 0.0f;
            }
            else
            {
                gameState &= ~PAUSE;
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = fixedTimeScale * Time.timeScale;
            }

            return true;
        }
        //スペースキーを押した時の処理
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PAUSE == (gameState & PAUSE)) { return false; }

            if (SHOP != (gameState & SHOP) && shopS.CheckNearShop())
            {
                shopS.OpenShop();
                gameState |= SHOP;
            }

            return true;
        }
        //Eを押したときの処理
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (PAUSE == (gameState & PAUSE) || SHOP == (gameState & SHOP)) { return false; }

            if (ITEMS != (gameState & ITEMS))
            {
                gameState |= ITEMS;
                belongItemS.OpenItems();
            }

            return true;
        }
        //バックスペースを押したとき
        if (Input.GetKeyDown(KeyCode.Backspace)) 
        {
            if (SHOP == (gameState & SHOP))     //SHOP中ならSHOP状態を解除
            {
                gameState &= ~SHOP;
                shopS.ExitShop();
            }
            if (ITEMS == (gameState & ITEMS))  //アイテム欄を開いていたらアイテム欄を閉じる
            {
                gameState &= ~ITEMS;
                belongItemS.CloseItems();
            }
            return true;
        }
        //ESCキーを押したときの処理 
        if (Input.GetKeyDown(KeyCode.Escape))  
        {
            Application.Quit(); //ゲームを終わる
            Debug.Log("ゲーム終了");
            return true;
        }

        return false;
    }

    void Start()
    {
        this.fixedTimeScale = Time.fixedDeltaTime;  //fixedUpdateの時間を保存する
        shopS.StartShop();  //ショップの初期処理をする
        belongItemS.StartBelongItem();  //アイテム欄の初期処理をする
    }

    void Update()   //主なゲームループ
    {
        if (KeyEvents())
        {
            //もしいずれかの操作キーを押したとき
            #if CHECK
            ShowGameState();    
            #endif
        }

        if (PAUSE == (gameState & PAUSE)) { return; }

        //通常場面とアイテム場面ならプレイヤーを操作できるようにする
        if (SHOP != (gameState & SHOP))
        {
            playerS.UpdatePlayer();
        }
        //ショップ場面の場合
        if (SHOP == (gameState & SHOP))
        {
            shopS.UpdateShop();
        }
        //アイテム画面の場合
        else if (ITEMS == (gameState & ITEMS))
        {
            belongItemS.UpdateBelongItem();
        }
    }
}
