using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    //[SerializeField]

    //キー入力
    public override void KeyController()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
    }

    //歩く処理
    public override void Walk()
    {
        rigid.x = speed;
    }

}
