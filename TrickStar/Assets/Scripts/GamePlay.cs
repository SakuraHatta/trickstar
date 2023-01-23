using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [Header("Scripts")]
    //ゲーム進行に必要なScriptたち
    [SerializeField]
    private PlayerController playerCS; //PlayerContollerのスクリプト
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private ShopScript shopS;

    //ゲーム進行中のパラメーター
    private uint gameState = 0b0001;

    private float fixedTimeScale;   //FixedUpdateの時間

    private void ShowGameState() //今のゲームの状況を表示するメゾット 
    {
#if CHECK
        Debug.Log("ゲームの状態 : " + gameState);
#endif
    }

    //ポーズ画面のイベント
    private void pauseEvent() {
        //ポーズ画面じゃないとき
        if (Const.PAUSE != (gameState & Const.PAUSE))   
        {
            gameState |= Const.PAUSE;
            Time.timeScale = 0.0f;
            playerCS.SetActive(false);
        }
        else 
        {
            //ポーズ画面の時
            gameState &= ~Const.PAUSE;
            Time.timeScale = Const.DEFAULT_TIME_SCALE;
            Time.fixedDeltaTime = fixedTimeScale * Time.timeScale;
            playerCS.SetActive(true);
        }
    }
    //ショップ画面のイベント
    private void ShopEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE)) { return; }

        if (Const.SHOP != (gameState & Const.SHOP) && shopS.CheckNearShop())
        {
            shopS.OpenShop();
            gameState |= Const.SHOP;
            playerCS.SetActive(false);
        }
    }
    //アイテム欄のイベント
    private void ItemsEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE) || Const.SHOP == (gameState & Const.SHOP)) { return; }

        if (Const.ITEM != (gameState & Const.ITEM))
        {
            gameState |= Const.ITEM;
            belongItemS.OpenItems();
            belongItemS.ChangeColor();
        }
    }
    
    private void BackEvent()
    {
        if (Const.SHOP == (gameState & Const.SHOP))     //SHOP中ならSHOP状態を解除
        {
            gameState &= ~Const.SHOP;
            shopS.ExitShop();
            playerCS.SetActive(true);
        }
        if (Const.ITEM == (gameState & Const.ITEM))  //アイテム欄を開いていたらアイテム欄を閉じる
        {
            gameState &= ~Const.ITEM;
            belongItemS.CloseItems();
        }
    }
    //キーを押したときの処理
    private bool KeyEvents()
    {
        //Enterキーを押したときの処理 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pauseEvent();
            return true;
        }
        //スペースキーを押した時の処理
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShopEvent();
            return true;
        }
        //Eを押したときの処理
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            ItemsEvent();
            return true;
        }
        //バックスペースを押したとき
        if (Input.GetKeyDown(KeyCode.Backspace)) 
        {
            BackEvent();
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

    //初期処理
    void Start()
    {
        this.fixedTimeScale = Time.fixedDeltaTime;  //fixedUpdateの時間を保存する
        shopS.StartShop();  //ショップの初期処理をする
        belongItemS.StartBelongItem();  //アイテム欄の初期処理をする
    }
    //主なゲームループ
    void Update()
    {
        if (KeyEvents())
        {
            //もしいずれかの操作キーを押したとき
#if CHECK
            ShowGameState();    
#endif
        }

        if (Const.PAUSE == (gameState & Const.PAUSE)) { return; }

        //ショップ場面を開いている場合
        if (Const.SHOP == (gameState & Const.SHOP))
        {
            shopS.UpdateShop();
        }

        //アイテム画面を開いている場合
        if (Const.ITEM == (gameState & Const.ITEM))
        {
            belongItemS.UpdateBelongItem();
        }
    }
}
