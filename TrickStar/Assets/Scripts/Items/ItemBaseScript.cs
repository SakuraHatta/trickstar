using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseScript : MonoBehaviour
{
    protected uint MitemType;    //アイテムの種類

    ////アイテム使用中なら常に処理するメゾット
    //public virtual void UpdateItem() {; }
    //引数のキャラクターにアイテムの効果を発動させるメゾット
    public virtual void ActiveItem(CharacterBase Char) {; }
    //引数のキャラクターのアイテムの効果を消すメゾット
    public virtual void StopItem(CharacterBase Char) {;}   

    //アイテムを使った後、耐久地を減らすメゾット
    public virtual void Decrease() {; }

    public uint GetItemType() { return MitemType; } //アイテムのタイプを所得するメゾット
}
