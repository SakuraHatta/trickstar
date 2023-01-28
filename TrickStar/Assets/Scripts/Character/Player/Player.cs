using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //キャラクターデーター
    [Header("Character")]
    [SerializeField]
    private int CharacterId;    //CharacterId
    [SerializeField]
    private CharacterListData CListData;

    [Space(10)]
    [Header("Scripts")]
    [SerializeField]
    private PlayerController playerCS;   //PlayerControllerのScript

    void Start()
    {
        playerCS.SetParameta(CListData.CharacterDataList[CharacterId]);
    }

    //何かにぶつかった時
    private void OnCollisionEnter2D(Collision2D hit)
    {
        playerCS.HitWall();
    } 

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Damage")
        {
            playerCS.TakeDamage();
        }
    }

    //プレイヤーの基本処理
    public void Update()
    {
        playerCS.AnimationUpdate(); //アニメーションのアップデート

        //もしプレイヤーが機能してないならここで中断する
        if (!playerCS.GetActive()) {
            Debug.Log("プレイヤーは機能していない!");
            return;
        }

        playerCS.KeyController();  //プレイヤーのキー操作処理
        playerCS.AdjustRigid();    //プレイヤーのrigid調整
        playerCS.SelectTile();      //選択中の場所にタイルを表示する関数
        playerCS.Jump();        //ジャンプ処理
    }
}