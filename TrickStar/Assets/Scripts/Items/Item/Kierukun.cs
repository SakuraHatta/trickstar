using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kierukun : ItemBaseScript
{
    public override void SetActive(CharacterBase Char) { 
        if (!Mactive)
        {
            Char.itemstate |= Const.INVINCLEBLE;
        }
        else
        {
            Char.itemstate &= ~Const.INVINCLEBLE;
        }

        Mactive = !Mactive;
    }

    public override void SetPassive(CharacterBase Char) { 
        Mactive = false;
        Char.itemstate &= ~Const.INVINCLEBLE;
    }
}
