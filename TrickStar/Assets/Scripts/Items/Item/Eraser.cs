using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : ItemBaseScript
{
    public override void SetActive(CharacterBase Char)
    {
        if (!Mactive)
        {
            Char.itemstate |= Const.DIG;
        }
        else
        {
            Char.itemstate &= ~Const.DIG;
        }

        Mactive = !Mactive;
    }
    public override void SetPassive(CharacterBase Char)
    {
        Mactive = false;
        Char.itemstate &= ~Const.DIG;
    }
}
