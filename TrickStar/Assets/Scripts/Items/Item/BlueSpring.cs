using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpring : ItemBaseScript
{
    private readonly float MaddJump;

    public BlueSpring()
    {
        MaddJump = 20.0f;
    }

    public override void SetActive(CharacterBase Char)
    {
        Mactive = true;
        Char.jumppower += MaddJump;
    }

    public override void SetPassive(CharacterBase Char)
    {
        Mactive = false;
        Char.jumppower -= MaddJump;
    }
}
