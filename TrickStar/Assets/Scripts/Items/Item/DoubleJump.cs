using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : ItemBaseScript
{
    public override void UseItem(CharacterBase Char)
    {
        if (!Mactive)
        {
            Char.itemstate |= Const.DOUBLEJUMP;
            Char.airjump += 1;
        }
        else
        {
            Char.itemstate &= ~Const.DOUBLEJUMP;
            Char.airjump -= 1;
        }

        Mactive = !Mactive;
    }
    public override void SetPassive(CharacterBase Char) 
    {
        Mactive = false;
        Char.itemstate &= ~Const.DOUBLEJUMP;
        Char.airjump -= 1;
    }
}
