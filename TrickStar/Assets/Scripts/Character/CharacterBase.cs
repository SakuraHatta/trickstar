using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //パラメーター
    protected int maxhp;          //最大体力
    protected int hp;             //体力
    protected int attack;         //攻撃力
    protected float jumppower;    //ジャンプ力
    protected float speed;        //移動速度

    protected Vector2 rigid;      //動く力

    //状態管理
    protected int State = 0b00000; //(未定、攻撃、ジャンプ、移動、地面)

    //メゾット
    public void SetParameta(CharacterData charData)   //CharacterDataに合わせてパラメーターを変更する
    {
        maxhp = charData.maxhp;
        attack = charData.attack;
        jumppower = charData.jumppower;
        speed = charData.speed;
    }

    public virtual void KeyController() {; }     //キーごとのアクション
    public virtual void Jump() {; }              //ジャンプするメゾット
    public virtual void Walk() {; }              //移動するメゾット
    public virtual void Attack() {; }            //攻撃するメゾット
}