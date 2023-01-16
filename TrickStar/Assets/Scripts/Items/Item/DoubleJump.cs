using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : ItemBaseScript
{
    public override void SetActive(CharacterBase Char)
    {
        Mactive = true;
        Char.itemstate |= Char.DOUBLEJUMP;
        Char.airjump += 1;
    }
    public override void SetPassive(CharacterBase Char) 
    {
        Mactive = false;
        Char.itemstate &= ~Char.DOUBLEJUMP;
        Char.airjump -= 1;
    }
}
