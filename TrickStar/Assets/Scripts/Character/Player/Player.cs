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

    //プレイヤーの基本処理
    public void Update()
    {
        if (!playerCS.GetActive()) { return; }

        playerCS.KeyController();  //プレイヤーのキー操作処理
        playerCS.AdjustRigid();    //プレイヤーのrigid調整
        playerCS.SelectTile();
    }
}