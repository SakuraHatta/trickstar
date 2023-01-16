using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    //ゲーム進行に必要なScriptたち
    [SerializeField]
    private PlayerController plrControllerS;
    [SerializeField]
    private BelongItem belongItemS;
    [SerializeField]
    private ShopScript shopS;

    //ゲーム進行中のパラメーター
    private uint gameState = 0b0000;
    private const uint SHOP = 0b0001;  //買い物中かのフラグ
    private const uint ITEMS = 0b0010; //インベントリを開いているかのフラグ
    private const uint PAUSE = 0b0100;    //ポーズ中かのフラグ

    private void ShowGameState()
    {
        switch (gameState)
        {
            case SHOP:
                Debug.Log("ショップ中です");
                break;
            case ITEMS:
                Debug.Log("インベントリを開いている");
                break;
            case PAUSE:
                Debug.Log("ポーズ中");
                break;
            default:
                break;
        }
    }

    private void KeyEvents()//キーを押したときの処理
    {
        if (Input.GetKeyDown(KeyCode.Return))//Enterキーを押したときの処理   
        {
            if (PAUSE != (gameState & PAUSE))
            {
                gameState |= PAUSE;
                Debug.Log("ポーズ画面");
            }
            else
            {
                gameState &= ~PAUSE;
                Debug.Log("ポーズ解除");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PAUSE == (gameState & PAUSE)) { return; }

            if (SHOP != (gameState & SHOP))
            {
                gameState |= SHOP;
                EnterShop();
            }
            else
            {
                gameState &= ~SHOP;
                ExitShop();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PAUSE == (gameState & PAUSE)) { return; }

            if (ITEMS != (gameState & ITEMS))
                gameState |= ITEMS;
            else
                gameState &= ~ITEMS;
        }

        if (Input.GetKeyDown(KeyCode.Escape))//ESCキーを押したときの処理   
        {
            Application.Quit(); //ゲームを終わる
            Debug.Log("ゲーム終了");
        }

        ShowGameState();
    }

    private void EnterShop()//ショップに入った時の処理
    {
        shopS.OpenShop();
    }
    private void ExitShop()
    {
        shopS.ExitShop();
    }

    void Update()   //主なゲームループ
    {
        KeyEvents();

        if (SHOP == (gameState & SHOP) && PAUSE != (gameState & PAUSE))
        {
            shopS.UpdateShop();
        }
        else if (ITEMS == (gameState & ITEMS) && PAUSE != (gameState & PAUSE))
        {
            belongItemS.UpdateBelongItem();
        }
    }
}
