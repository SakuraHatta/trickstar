using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePen : ItemBaseScript
{
    public override void SetActive(CharacterBase Char)
    {
        Mactive = true;
        Char.itemstate |= Char.CREATE;
    }
    public override void SetPassive(CharacterBase Char)
    {
        Mactive = false;
        Char.itemstate &= ~Char.CREATE;
    }
}
