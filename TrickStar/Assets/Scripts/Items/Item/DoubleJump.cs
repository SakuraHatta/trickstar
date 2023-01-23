using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : ItemBaseScript
{
    private readonly int MaddAirJumpTimes;

    public DoubleJump()
    {
        MitemType = 0b0010;
        MaddAirJumpTimes = 1;
    }

    public override void ActiveItem(CharacterBase Char){
        Char.Itemstate |= Const.DOUBLEJUMP;
        Char.Limitairjump += MaddAirJumpTimes;
    }

    public override void StopItem(CharacterBase Char) 
    {
        Char.Itemstate &= ~Const.DOUBLEJUMP;
        Char.Limitairjump -= 1;
    }
}
