using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelongItemData
{
    private int MitemID;    //�A�C�e����Id
    private int Mendurance;   //�A�C�e���̑ϋv�n
    private bool Mactive;   //�A�C�e�����g�p���Ă��邩�̃t���O


    public BelongItemData()
    {
        MitemID = Const.NO_ITEM;
        Mendurance = Const.NO_ITEM;
        Mactive = false;
    }

    public bool MActive { get { return Mactive; } set { Mactive = value; } }
    public int MItemID { get { return MitemID; } set { MitemID = value; } }
    public int MEndurance { get { return Mendurance; } set { Mendurance = value; } }
}
