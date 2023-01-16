using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseScript : MonoBehaviour
{
    protected int MitemID;   //�A�C�e����ID(ItemDataList��Index�̂���)

    protected bool Mactive;    //�A�C�e���𓮂����Ă��邩�̃t���O

    public ItemBaseScript()
    {
        Mactive = false;
    }

    public virtual void SetActive(CharacterBase Char) {;}    //�A�C�e���̌��ʂ𔭓������郁�]�b�g
    public virtual void SetPassive(CharacterBase Char) {;}   //�A�C�e���̌��ʂ��������]�b�g

    public bool CheckActive() { return Mactive; }   //�A�C�e�����N�����Ă邩�m�F���郁�]�b�g
}
