using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private List<ItemBaseScript> ItemBoxList = new List<ItemBaseScript>();
    
    //�����̃L�����N�^�[�^�̃I�u�W�F�N�g�ɁA������id�̃A�C�e���𔭓�������
    public void ActiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].SetActive(Char);
    }
    //�����̃L�����N�^�[�^�̃I�u�W�F�N�g�ɁA������id�̃A�C�e���̌��ʂ���菜��
    public void PassiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].SetPassive(Char);
    }
}
