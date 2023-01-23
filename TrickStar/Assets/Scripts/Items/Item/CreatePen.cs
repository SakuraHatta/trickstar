using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePen : ItemBaseScript
{
    public CreatePen()
    {
        MitemType = 0b0100;
    }

    //アイテムを使用するメゾット
    public override void ActiveItem(CharacterBase Char)
    {
        Char.Itemstate |= Const.CREATE;
    }

    //アイテムの使用をやめるスクリプト
    public override void StopItem(CharacterBase Char)
    {
        Char.Itemstate &= ~Const.CREATE;
    }
}
