using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePen : ItemBaseScript
{
    public override void UseItem(CharacterBase Char)
    {
        if (!Mactive)
        {
            Char.itemstate |= Const.CREATE;
        }
        else
        {
            Char.itemstate &= ~Const.CREATE;
        }

        Mactive = !Mactive;
    }
    public override void SetPassive(CharacterBase Char)
    {
        Mactive = false;
        Char.itemstate &= ~Const.CREATE;
    }
}
