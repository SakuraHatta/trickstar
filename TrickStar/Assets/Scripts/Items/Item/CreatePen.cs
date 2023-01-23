using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePen : ItemBaseScript
{
    public CreatePen()
    {
        MitemType = 0b0100;
    }

    //�A�C�e�����g�p���郁�]�b�g
    public override void ActiveItem(CharacterBase Char)
    {
        Char.Itemstate |= Const.CREATE;
    }

    //�A�C�e���̎g�p����߂�X�N���v�g
    public override void StopItem(CharacterBase Char)
    {
        Char.Itemstate &= ~Const.CREATE;
    }
}
