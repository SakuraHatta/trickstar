using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    //[SerializeField]

    //�L�[����
    public override void KeyController()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
    }

    //��������
    public override void Walk()
    {
        rigid.x = speed;
    }

}
