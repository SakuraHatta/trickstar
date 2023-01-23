using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseScript : MonoBehaviour
{
    protected uint MitemType;    //�A�C�e���̎��

    ////�A�C�e���g�p���Ȃ��ɏ������郁�]�b�g
    //public virtual void UpdateItem() {; }
    //�����̃L�����N�^�[�ɃA�C�e���̌��ʂ𔭓������郁�]�b�g
    public virtual void ActiveItem(CharacterBase Char) {; }
    //�����̃L�����N�^�[�̃A�C�e���̌��ʂ��������]�b�g
    public virtual void StopItem(CharacterBase Char) {;}   

    //�A�C�e�����g������A�ϋv�n�����炷���]�b�g
    public virtual void Decrease() {; }

    public uint GetItemType() { return MitemType; } //�A�C�e���̃^�C�v���������郁�]�b�g
}
