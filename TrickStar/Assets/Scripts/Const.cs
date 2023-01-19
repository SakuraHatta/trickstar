using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Const
{
    //ゲームステータス
    public const uint DEFAULT = 0b0001; //普通の状態
    public const uint ITEM = 0b0010;    //アイテム欄を表示
    public const uint SHOP = 0b0100;    //ショップ中
    public const uint PAUSE = 0b1000;   //ポーズ中

    //プレイヤ
    public const uint ALIVE = 0b0001;   //生存フラグ
    public const uint JUMP = 0b0010;    //ジャンプフラグ
    public const uint ATTACK = 0b0100;  //攻撃フラグ
    public const uint ACTIVE = 0b1000;  //起動フラグ

    //アイテム
    public const uint INVINCLEBLE = 0b0001; //無敵フラグ
    public const uint DOUBLEJUMP = 0b0010;  //ダブルジャンプフラグ
    public const uint DIG = 0b0100;         //掘れるフラグ
    public const uint CREATE = 0b1000;      //ブロックを生成できるフラグ

    public const int MAX_ITEMS = 3;     //持てるアイテムの最大数
    public const int TYPE_ITEMS = 5;    //アイテムの種類

    //ショップ
    public const int CARD_NUMBER = 3;   //ショップで売られているカードの枚数

    public const float ITEM_SCALE = 1.0f;   //ショップの大きさ
}
