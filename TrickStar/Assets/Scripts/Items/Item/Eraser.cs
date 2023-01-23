using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : ItemBaseScript
{
    public Eraser()
    {
        MitemType = 0b0100;
    }

    public override void ActiveItem(CharacterBase Char)
    {
        Char.Itemstate |= Const.DIG;
    }

    public override void StopItem(CharacterBase Char)
    {
        Char.Itemstate &= ~Const.DIG;
    }
}
