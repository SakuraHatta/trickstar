using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Logo : UIController
{
    public override void UpdateText()
    {
        timeValue += Time.deltaTime;
        
        //à⁄ìÆèÛë‘ÇÃéû
        if (Const.TWEEN_TEXT == (textState & Const.TWEEN_TEXT))
        {
            TweenText();
        }
        else if(Const.DEFAULT == (textState & Const.DEFAULT))
        {
            UpDownText();
        }
    }
}
