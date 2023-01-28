using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//継承クラス(継承元 : CharacterBase)
public class PlayerController : CharacterBase
{
    [SerializeField]
    private BelongItem belongItemsS;    //プレイヤーの持ち物を管理するScript(Playerから)

    [Space(10)]
    [Header("Physics")]
    [SerializeField]
    private BoxCollider2D colliderC;  //BodyCollider2Dのコンポーネント
    [SerializeField]
    private Rigidbody2D rigidC;  //RigidBody2Dのコンポーネント

    private float jumptime;
    private int money;  //所持金

    //アニメーション
    public override void AnimationUpdate()
    {
        string playerstate = "none";

        //アイテム欄を開いているかのフラグ
        if (Const.INVENTORY == (state & Const.INVENTORY))
        {
            playerstate = "Items";
            animator.SetBool("Item", true);
            return;
        }
        else
        {
            animator.SetBool("Item", false);
        }

        //飛んでいるかを確認するメゾット
        if (Const.JUMP == (state & Const.JUMP))
        {
            playerstate = "Jump";
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        //歩いているかを確認するメゾット
        if (Const.WALK == (state & Const.WALK))
        {
            playerstate = "Walk";
            animator.SetBool("Walk", true);
        }
        else
        {
            playerstate = "Idel";
            animator.SetBool("Walk", false);
        }

#if CHECK
        Debug.Log("プレイヤーの状態は : " + playerstate + "です。");
#endif
    }
    //キー入力
    public override void KeyController()
    {
        //起動中じゃないなら処理を中断する
        if (Const.ACTIVE != (state & Const.ACTIVE)) { return; }

        //Aキーを押したとき
        if (Input.GetKey(KeyCode.A))//右に移動
        {
            Walk(-1);
        }
        //Dキーを押したとき
        else if (Input.GetKey(KeyCode.D))//左に移動
        {
            Walk(1);
        }
        //移動キーを押していないとき
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //もし移動フラグが立っていたらおろす
            if (Const.WALK == (state & Const.WALK)) { state &= ~Const.WALK; }
        }

        //スペースキーを押したとき
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //プレイヤーがジャンプ状態じゃないとき
            if (Const.JUMP != (state & Const.JUMP))    
            {
                state |= Const.JUMP;
                state |= Const.JUMPING;
            }
            //空中でダブルジャンプができるとき
            else if (Const.JUMP == (state & Const.JUMP) && Const.DOUBLEJUMP == (itemstate & Const.DOUBLEJUMP))    
            {
                if (airjump < limitairjump)
                SkyJump();
                airjump++;
            }
        }

        //スペースキーを離したとき
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Const.JUMPING != (state & Const.JUMPING)) { return; }
            Resetjump();
        }

        //左クリックを押したとき
        if (Input.GetMouseButtonDown(0))
        {
            if (Const.DIG == (itemstate & Const.DIG))
            {
                if (tilemapS.DeleteTile())
                {
                    itemsBoxS.DecreaseCreate(this);
                    belongItemsS.ChangeColor();
#if CHECK
                        Debug.Log("タイルを破壊");
#endif
                    return;
                }
#if CHECK
                    Debug.Log("削除失敗…");
#endif
            }

            else if (Const.CREATE == (itemstate & Const.CREATE))
            {

                if (tilemapS.CreateTile())
                {
                    itemsBoxS.DecreaseCreate(this);
                    belongItemsS.ChangeColor();
#if CHECK
                        Debug.Log("タイルを生成");
#endif
                    return;
                }
#if CHECK
                    Debug.Log("生成失敗…");
#endif
            }
        }
    }
    //歩く処理
    public override void Walk(int direction)
    {
        //もし移動フラグが立っていないなら立てる
        if (Const.WALK != (state & Const.WALK)) { state |= Const.WALK; }
        
        //もし最大速度より早かったら処理を中断
        if (rigid.x * rigid.x > speed * speed)
        {
            return;
            //if (rigid.x > 0.0f) //rigidが正の数の時
            //    rigid.x = speed;
            //else                //rigidが負の数の時
            //    rigid.x = -speed;
        }

        //移動する力のベクトルをつくる
        Vector2 walk = new Vector2(speed * direction * Time.deltaTime, 0.0f);
        walk *= Const.RIGID_TIMES;

        //プレイヤーの向きを変更
        this.transform.localScale = new Vector2(Const.PLAYER_SCALE * direction, Const.PLAYER_SCALE);

        //実際に力を加える
        rigidC.AddForce(walk, ForceMode2D.Force);
    }
    //ジャンプ
    public override void Jump()
    {
        if (Const.JUMPING != (state & Const.JUMPING)) { return; }

        Vector2 jump = new Vector2(0.0f, jumppower * Time.deltaTime);
        jump *= Const.RIGID_TIMES;
        rigidC.AddForce(jump, ForceMode2D.Force);

        if (jumptime > Const.MAX_JUMP_TIME)
        {
            Resetjump();
            return;
        }

        jumptime += Time.deltaTime;
    }
    //ダブルジャンプ
    public override void SkyJump()
    {
        rigid.y = 0.0f;
        Vector2 jump = new Vector2(0.0f, jumppower);
        jump *= Const.RIGID_TIMES;
        rigidC.AddForce(jump, ForceMode2D.Force);
    }
    //地面に触った時
    public override void HitWall()
    {
        state &= ~Const.JUMP;
        rigid.y = 0.0f;
        
        if (Const.DOUBLEJUMP == (itemstate & Const.DOUBLEJUMP))
        {
            airjump = 0;
        }
    }

    //ジャンプ時間をリセットする
    private void Resetjump()
    {
        state &= ~Const.JUMPING;
        jumptime = 0.0f;
        rigid.y = 0.0f;
    }

    //マウスの位置にタイルを表示させる処理
    public void SelectTile()
    {
        tilemapS.ShowSelectTile(itemstate);
    }

    //生きているか確認する処理
    public bool CheckAlive() { 
        if (Const.ALIVE == (state & Const.ALIVE)) { return true; }  //生きていたらtrueを返す
        return false;   //生きてなかったらfalseを返す
    }
    //ダメージを与える処理
    public void TakeDamage()
    {
        if (Const.INVINCLEBLE == (itemstate & Const.INVINCLEBLE)) {
#if CHECK
            Debug.Log("無敵状態なのでダメージを食らわない!");
#endif
            return;
        }
#if CHECK
        Debug.Log("ダメージを受けた!");
#endif
        //hpを1ずつ減らす
        hp--;   
        if (hp <= 0)
        {
            state &= ~Const.ALIVE;  //もしhpが0以下なら生存フラグを取り消す
        }
    }
    //最初に戻った時の処理
    public void Restart()
    {
        state |= Const.ALIVE + Const.ACTIVE;    //生存フラグをonにする
        itemstate = 0b0000;     //アイテム状態をデフォルトにする
        //持っているアイテムの効果を消して、全てリセットする
        foreach(BelongItemData bData in EquipmentItem)
        {
            itemsBoxS.PassiveItems(this, bData);
            bData.ResetData();
        }
    }

    //所持金を所得する
    public int GetMoney() { return money; }

    //アイテムを使用するメゾット
    public void UseItem(int equipIndex)
    {
        //もしインデントリでまだ持ってないアイテムを使おうとしたとき
        if (EquipmentItem[equipIndex].MItemID == Const.NO_ITEM) {
            //処理を中断する
#if CHECK
                Debug.Log("indexが" + equipIndex + "のアイテムはまだ何も持っていない!");
#endif
            return;
        }
        else if (EquipmentItem[equipIndex].MEndurance == 0)
        {
            //処理を中断する
#if CHECK
                Debug.Log("indexが" + equipIndex + "のアイテムはもう使えない!");
#endif
            return;
        }

        itemsBoxS.UseItems(this ,EquipmentItem[equipIndex]);
        EquipmentItem[equipIndex].MActive = !(EquipmentItem[equipIndex].MActive);
#if CHECK
        Debug.Log("indexが" + equipIndex + "のIDが" + EquipmentItem[equipIndex].MItemID + "アイテムを使った");
#endif
    }

    //rigidを調整する関数
    public void AdjustRigid()
    {
        rigid = rigidC.velocity;    //実際のrigidをRigidBody2Dコンポーネントから持ってくる

        rigidC.velocity = rigid;    //調整したrigidを設定する
    }

    //itemを開いた時の処理
    public void OpenItem()
    {
        //アイテムを開いてないとき
        if (Const.INVENTORY != (state & Const.INVENTORY))
        {
            state |= Const.INVENTORY;
        }
        //アイテムを開いているとき
        else if (Const.INVENTORY == (state & Const.INVENTORY))
        {
            state &= ~Const.INVENTORY;
        }
    }
}
