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
    private PlayerController playerS;   //PlayerControllerのScript

    void Start()
    {
        playerS.SetParameta(CListData.CharacterDataList[CharacterId]);
    }

    //何かにぶつかった時
    private void OnCollisionEnter2D(Collision2D hit)
    {
        playerS.HitWall();
    } 

    //プレイヤーの基本処理
    public void UpdatePlayer()
    {
        playerS.KeyController();  //プレイヤーのキー操作処理
        playerS.AdjustRigid();    //プレイヤーのrigid調整
    }
}
