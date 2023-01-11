using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    //ショップに売られているカードの枚数 
    private const int CardNumbers = 3;  

    //ゲーム進行に必要なScriptたち
    [SerializeField]
    private ShopScript shopS;

    //ゲーム進行中のパラメーター
    private bool shopping = false;  //買い物中かのフラグ
    private bool paused = false;    //ポーズ中かのフラグ

    private void KeyEvents()//キーを押したときの処理
    {
        if (Input.GetKeyDown(KeyCode.Return))//Enterキーを押したときの処理   
        {
            if (!paused)
            {
                Debug.Log("ポーズ画面");
            }
            else
            {
                Debug.Log("ポーズ解除");
            }

            paused = !paused;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnterShop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))//ESCキーを押したときの処理   
        {
            Application.Quit(); //ゲームを終わる
            Debug.Log("ゲーム終了");
        }
    }

    private void EnterShop()//ショップに入った時の処理
    {
        shopS.Card();
    }

    void Update()   //主なゲームループ
    {
        KeyEvents();
    }
}
