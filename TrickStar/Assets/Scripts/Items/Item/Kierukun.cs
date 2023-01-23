using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kierukun : ItemBaseScript
{
    public Kierukun()
    {
        MitemType = 0b0001;
    }

    public override void ActiveItem(CharacterBase Char) { 
        Char.Itemstate |= Const.INVINCLEBLE;
    }

    public override void StopItem(CharacterBase Char) { 
        Char.Itemstate &= ~Const.INVINCLEBLE;
    }
}
