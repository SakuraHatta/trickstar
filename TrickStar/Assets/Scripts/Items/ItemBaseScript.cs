using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseScript : MonoBehaviour
{
    protected int MitemID;   //アイテムのID(ItemDataListのIndexのこと)

    protected bool Mactive;    //アイテムを動かしているかのフラグ

    public ItemBaseScript()
    {
        Mactive = false;
    }

    public virtual void SetActive(CharacterBase Char) {;}    //アイテムの効果を発動させるメゾット
    public virtual void SetPassive(CharacterBase Char) {;}   //アイテムの効果を消すメゾット

    public bool CheckActive() { return Mactive; }   //アイテムが起動してるか確認するメゾット
}
