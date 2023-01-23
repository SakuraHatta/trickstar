using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelongItemData
{
    private int MitemID;    //アイテムのId
    private int Mendurance;   //アイテムの耐久地
    private bool Mactive;   //アイテムを使用しているかのフラグ


    public BelongItemData()
    {
        MitemID = Const.NO_ITEM;
        Mendurance = Const.NO_ITEM;
        Mactive = false;
    }

    public bool MActive { get { return Mactive; } set { Mactive = value; } }
    public int MItemID { get { return MitemID; } set { MitemID = value; } }
    public int MEndurance { get { return Mendurance; } set { Mendurance = value; } }
}
