using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpring : ItemBaseScript
{
    private readonly float MaddJump;

    public BlueSpring()
    {
        MaddJump = 2.0f;
        MitemType = 0b0001;
    }

    public override void ActiveItem(CharacterBase Char)
    {
            Char.Jumppower += MaddJump;
    }

    public override void StopItem(CharacterBase Char)
    {
            Char.Jumppower -= MaddJump;
    }
}
