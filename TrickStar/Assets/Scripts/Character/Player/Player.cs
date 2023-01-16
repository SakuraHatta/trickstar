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
        Debug.Log(hit.gameObject.transform.position);
        playerS.HitWall();
    } 

    void Update()
    {
        playerS.KeyController();  //プレイヤーのキー操作処理
        playerS.AdjustRigid();
    }
}
