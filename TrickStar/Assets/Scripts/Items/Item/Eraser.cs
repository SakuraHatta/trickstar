using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : ItemBaseScript
{
    public override void SetActive(CharacterBase Char)
    {
        Mactive = true;
        Char.itemstate |= Char.DIG;
    }
    public override void SetPassive(CharacterBase Char)
    {
        Mactive = false;
        Char.itemstate &= ~Char.DIG;
    }
}
