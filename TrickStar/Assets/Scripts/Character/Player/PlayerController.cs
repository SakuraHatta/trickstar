using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//継承クラス(継承元 : CharacterBase)
public class PlayerController : CharacterBase
{
    [Space(10)]
    [Header("Physics")]
    [SerializeField]
    private BoxCollider2D colliderC;  //BodyCollider2Dのコンポーネント
    [SerializeField]
    private Rigidbody2D rigidC;  //RigidBody2Dのコンポーネント

    private int money;  //所持金

    //キー入力
    public override void KeyController()
    {
        //Aキーを押したとき
        if (Input.GetKey(KeyCode.A))//右に移動
        {
            Walk(-1);
        }
        //Dキーを押したとき
        if (Input.GetKey(KeyCode.D))//左に移動
        {
            Walk(1);
        }
        //スペースキーを押したとき
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JUMP != (state & JUMP))    //プレイヤーがジャンプ状態じゃないとき
            {
                state |= JUMP;
                Jump();
            }
            else if (DOUBLEJUMP == (state & DOUBLEJUMP))
            {
                Jump();
            }
        }
    }
    //歩く処理
    public override void Walk(int direction)
    {
        Vector2 walk = new Vector2(speed * direction, 0.0f);
        rigidC.AddForce(walk, ForceMode2D.Force);        
    }
    //ジャンプ
    public override void Jump()
    {
        Vector2 jump = new Vector2(0.0f, jumppower);
        rigidC.AddForce(jump, ForceMode2D.Impulse);
    }
    //地面に触った時
    public override void HitWall()
    {
        state &= ~JUMP;
        rigid.y = 0.0f;
    }

    //生きているか確認する
    public bool CheckAlive() { 
        if (ALIVE == (state & ALIVE)) { return true; }  //生きていたらtrueを返す
        return false;   //生きてなかったらfalseを返す
    }
    //所持金を所得する
    public int GetMoney() { return money; }

    //持っているアイテムのidを確認するメゾット
    public List<int> GetItems(){ return EquipmentItem; }

    //アイテムを追加するメゾット
    public void AddItem(int id)
    {
        EquipmentItem.Add(id);
    }
    //アイテムを使用するメゾット
    public void UseItem(int equipIndex)
    {
        itemsBoxS.ActiveItems(this ,EquipmentItem[equipIndex]);
        Debug.Log(equipIndex + "のアイテムを使った");
    }
    //rigidを調整する関数
    public void AdjustRigid()
    {
        rigid = rigidC.velocity;    //実際のrigidをRigidBody2Dコンポーネントから持ってくる

        //rigid.xがspeedより大きくならないようにする
        if (rigid.x * rigid.x > speed * speed)
        {
            if (rigid.x > 0.0f) //rigidが正の数の時
                rigid.x = speed;    
            else                //rigidが負の数の時
                rigid.x = -speed;
        }

        rigidC.velocity = rigid;    //調整したrigidを設定する
    }
}
