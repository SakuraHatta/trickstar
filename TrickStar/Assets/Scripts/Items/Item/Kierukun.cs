using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kierukun : ItemBaseScript
{
    public override void SetActive(CharacterBase Char) { 
        Mactive = true;
        Char.itemstate |= Char.INVINCLEBLE;
    }

    public override void SetPassive(CharacterBase Char) { 
        Mactive = false;
        Char.itemstate &= ~Char.INVINCLEBLE;
    }
}
