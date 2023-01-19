using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpring : ItemBaseScript
{
    private readonly float MaddJump;

    public BlueSpring()
    {
        MaddJump = 10.0f;
    }

    public override void UseItem(CharacterBase Char)
    {
        if (!Mactive)
        {
            Char.jumppower += MaddJump;
        }
        else
        {
            Char.jumppower += -MaddJump;
        }

        Mactive = !Mactive;
    }
}
