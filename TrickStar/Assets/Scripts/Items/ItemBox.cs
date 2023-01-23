using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private List<ItemBaseScript> ItemBoxList = new List<ItemBaseScript>();
    
    //�����̃L�����N�^�[�^�̃I�u�W�F�N�g�ɁA������id�̃A�C�e���𔭓�������
    public void UseItems(CharacterBase Char, BelongItemData belongItemData)
    {
        //�������̃A�C�e�����g�p���Ȃ�
        if (belongItemData.MActive)
        {
            //���̃A�C�e���̎g�p���~���A���������イ���񂷂�
            //belongItemData.MActive = false;
            ItemBoxList[belongItemData.MItemID].StopItem(Char);
            return;
        }

        //�������̃A�C�e�����g�p���ĂȂ��Ȃ�
        //�N���G�C�g�^�C�v�̃A�C�e�����g�p������
        if (Const.CREATE_TYPE == (ItemBoxList[belongItemData.MItemID].GetItemType() & Const.CREATE_TYPE))
        {
            //�L�����N�^�[�������Ă��鑼�̃N���G�C�g�^�C�v�̃A�C�e�����~�߂�
            foreach(BelongItemData bData in Char.GetItems())  //�v���C���[�̎����Ă���A�C�e���������J��Ԃ�    
            {
                if (bData.MItemID == Const.NO_ITEM) { continue; }

                //�����N���G�C�g�^�C�v�̃A�C�e���������Ă���
                if (Const.CREATE_TYPE == (ItemBoxList[bData.MItemID].GetItemType() & Const.CREATE_TYPE)
                    && belongItemData.MItemID != bData.MItemID && bData.MActive)
                {
                    bData.MActive = false;
                    ItemBoxList[bData.MItemID].StopItem(Char); //���̃A�C�e���̎g�p���~�߂�
                }
            }
        }

        //belongItemData.MActive = true;
        ItemBoxList[belongItemData.MItemID].ActiveItem(Char); //�����̃A�C�e��ID�̃A�C�e�����g�p����
    }
    //�����̃L�����N�^�[�^�̃I�u�W�F�N�g�ɁA������id�̃A�C�e���̌��ʂ���菜��
    public void PassiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].StopItem(Char);
    }

    //�ϋv�n�����炷���]�b�g
    public void DecreaseCreate(CharacterBase Char)
    {
        //�v���C���[�̃A�C�e���̐������J��Ԃ�
        foreach (BelongItemData bData in Char.GetItems())
        {
            //�����A�C�e��ID�����������ĂȂ��Ȃ珈���𒆒f���Ď��ɉ�
            if (bData.MItemID == Const.NO_ITEM) { continue; }

            //�A�C�e���̃^�C�v��[�N���G�C�g]�Ŏg�p���Ȃ�
            if (Const.CREATE_TYPE == (ItemBoxList[bData.MItemID].GetItemType() & Const.CREATE_TYPE) &&
                bData.MActive)
            {
                bData.MEndurance--;�@//�ϋv�͂����炷
                //�����ϋv�͂�0�ɂȂ�����
                if (bData.MEndurance == 0)  
                {
                    PassiveItems(Char, bData.MItemID);  //���̃A�C�e���̌��ʂ��������Ė��g�p�ɂ���
                    bData.MActive = false;
                    break;
                }
            }
        }
    }
}
