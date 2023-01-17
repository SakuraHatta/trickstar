using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基底クラス
public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected ItemBox itemsBoxS;  //すべてのアイテムが管理されているスクリプト
    [SerializeField]
    protected List<int> EquipmentItem = new List<int>();

    //パラメーター
    public int maxhp;          //最大体力
    public int hp;             //体力
    public int attack;         //攻撃力
    public float jumppower;    //ジャンプ力
    public float speed;        //移動速度

    public int airjump;        //空中でジャンプできる回数

    protected Vector2 rigid;      //動く力

    //状態管理
    protected int state;            //プレイヤーのステータス

    protected readonly int ALIVE;   //生存フラグ
    protected readonly int JUMP;    //ジャンプフラグ
    protected readonly int ATTACK;  //攻撃フラグ
    protected readonly int ACTIVE;  //起動フラグ   

    public int itemstate;        //アイテムのステータス

    public readonly int INVINCLEBLE; //無敵フラグ
    public readonly int DOUBLEJUMP;  //ダブルジャンプフラグ
    public readonly int DIG;         //掘れるフラグ
    public readonly int CREATE;      //ブロックを生成できるフラグ

    public CharacterBase()
    {
        state = 0b1001;
        ALIVE = 0b0001;
        JUMP = 0b0010;
        ATTACK = 0b0100;
        ACTIVE = 0b1000;

        itemstate = 0b0000;
        INVINCLEBLE = 0b0001;
        DOUBLEJUMP = 0b0010;
        DIG = 0b0100;
        CREATE = 0b1000;
    }

    //メゾット
    public void SetParameta(CharacterData charData)   //CharacterDataに合わせてパラメーターを変更する
    {
        maxhp = charData.maxhp;
        attack = charData.attack;
        jumppower = charData.jumppower;
        speed = charData.speed;
    }

    public void ActiveItems()
    {
        foreach(int id in EquipmentItem)
        {
            itemsBoxS.ActiveItems(this, id);
        }
    }

    public virtual void CharacterUpdate() {;}

    public virtual void KeyController() {;}      //キーごとのアクション
    public virtual void Jump() {;}               //ジャンプするメゾット
    public virtual void Walk(int direction) {;}  //移動するメゾット
    public virtual void Attack() {;}             //攻撃するメゾット

    public Vector2 GetRigid() { return rigid; }  //rigidを所得するメゾット
    public virtual void HitWall() {;}            //壁に当たった時の処理
    public void ChangeActive(bool b)
    {
        if (b && ACTIVE != (state & ACTIVE))
            state |= ACTIVE;
        else if (ACTIVE == (state & ACTIVE))
            state &= ~ACTIVE;
    }
}