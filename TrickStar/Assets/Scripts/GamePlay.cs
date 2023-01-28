using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Vector2 startPos;       //最初のスタート地点の座標

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
            ChangePlayerActive(false);
        }
        else 
        {
            //ポーズ画面の時
            gameState &= ~Const.PAUSE;
            Time.timeScale = Const.DEFAULT_TIME_SCALE;
            Time.fixedDeltaTime = fixedTimeScale * Time.timeScale;
            ChangePlayerActive(false);
        }
    }
    //ショップ画面のイベント
    private void ShopEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE) || Const.ITEM == (gameState & Const.ITEM)) { return; }

        if (Const.SHOP != (gameState & Const.SHOP) && shopS.CheckNearShop())
        {
            shopS.OpenShop();
            gameState |= Const.SHOP;
            ChangePlayerActive(false);
        }
    }
    //アイテム欄のイベント
    private void ItemsEvent() 
    {
        if (Const.PAUSE == (gameState & Const.PAUSE) || Const.SHOP == (gameState & Const.SHOP)) { return; }

        if (Const.ITEM != (gameState & Const.ITEM) && Const.JUMP != (playerCS.State & Const.JUMP))
        {
            gameState |= Const.ITEM;
            belongItemS.OpenItems();
            belongItemS.ChangeColor();
            playerCS.OpenItem();
            ChangePlayerActive(false);
        }
    }
    //バックスペース押したときのイベント
    private void BackEvent()
    {
        if (Const.SHOP == (gameState & Const.SHOP))     //SHOP中ならSHOP状態を解除
        {
            gameState &= ~Const.SHOP;
            shopS.ExitShop();
            ChangePlayerActive(true);
        }
        if (Const.ITEM == (gameState & Const.ITEM))  //アイテム欄を開いていたらアイテム欄を閉じる
        {
            gameState &= ~Const.ITEM;
            playerCS.OpenItem();
            ChangePlayerActive(true);
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
        if (Input.GetKeyDown(KeyCode.W))
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
        if (Input.GetKeyDown(KeyCode.Q))
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

    //プライヤーの起動フラグを引数の値に変更するメゾット
    private void ChangePlayerActive(bool b)
    {
        playerCS.SetActive(b);
    }
    //プレイヤーが死亡した時にする処理
    private void RestartGame()
    {
        playerCS.transform.position = startPos;
        playerCS.Restart();
        belongItemS.OpenItems();
        belongItemS.ChangeColor();
        belongItemS.CloseItems();
    }

    //初期処理
    private void Start()
    {
        this.fixedTimeScale = Time.fixedDeltaTime;  //fixedUpdateの時間を保存する
        shopS.StartShop();  //ショップの初期処理をする
        belongItemS.StartBelongItem();  //アイテム欄の初期処理をする
        startPos = playerCS.transform.position; //スタート地点を始まりの位置にする
    }
    //主なゲームループ
    private void Update()
    {
        if (KeyEvents())
        {
            //もしいずれかの操作キーを押したとき
#if CHECK
            ShowGameState();    
#endif
        }

        if (Const.PAUSE == (gameState & Const.PAUSE)) { return; }

        //プレイヤーが死亡した場合
        if (!(playerCS.CheckAlive()))
        {
            //RestartGame();
            SceneManager.LoadScene("MapScene");
        }

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
