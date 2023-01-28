using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kierukun : ItemBaseScript
{

    private SpriteRenderer spriteR;

    public Kierukun()
    {
        MitemType = 0b0001;
    }

    public override void ActiveItem(CharacterBase Char) {
        spriteR = Char.gameObject.GetComponent<SpriteRenderer>();
        spriteR.enabled = false;

        Char.Itemstate |= Const.INVINCLEBLE;
    }

    public override void StopItem(CharacterBase Char) {
        spriteR = Char.gameObject.GetComponent<SpriteRenderer>();
        spriteR.enabled = true;

        Char.Itemstate &= ~Const.INVINCLEBLE;
    }
}
