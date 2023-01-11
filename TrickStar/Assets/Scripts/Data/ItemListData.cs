using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList",menuName = "DataLists/Item")]
public class ItemListData : ScriptableObject   //�A�C�e���̃f�[�^�N���X�����X�g�������N���X(����킵����)
{
    public List<ItemData> ItemDataList = new List<ItemData>(); 
}

[System.Serializable]
public class ItemData   //�A�C�e���̃f�[�^�N���X
{
    public int id;         //�A�C�e��ID
    public string name;    //�A�C�e����
    public int price;      //�A�C�e���̒l�i
    public Sprite image;   //�A�C�e���̉摜
    public string info;    //�A�C�e���̐�����
}
