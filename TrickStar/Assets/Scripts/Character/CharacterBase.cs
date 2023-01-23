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

    protected int Mitems; //持っているアイテム数

    //パラメーター
    public int maxhp;          //最大体力
    public int hp;             //体力
    public int attack;         //攻撃力
    public float jumppower;    //ジャンプ力
    public float speed;        //移動速度

    public int airjump;        //空中でジャンプできる回数

    protected Vector2 rigid;      //動く力

    //状態管理
    protected uint state;            //プレイヤーのステータス
    public uint itemstate;        //アイテムのステータス

    public CharacterBase()
    {
        state = 0b1001;
        itemstate = 0b0000;
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
    
    public Vector2 GetPosition() { return this.transform.position; }
    public Vector2 GetRigid() { return rigid; }  //rigidを所得するメゾット
    public virtual void HitWall() {;}            //壁に当たった時の処理
    public void ChangeActive(bool b)
    {
        if (b && Const.ACTIVE != (state & Const.ACTIVE))
            state |= Const.ACTIVE;
        else if (Const.ACTIVE == (state & Const.ACTIVE))
            state &= ~Const.ACTIVE;
    }
}