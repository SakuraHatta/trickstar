using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    //[SerializeField]

    //ƒL[“ü—Í
    public override void KeyController()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
    }

    //•à‚­ˆ—
    public override void Walk()
    {
        rigid.x = speed;
    }

}
