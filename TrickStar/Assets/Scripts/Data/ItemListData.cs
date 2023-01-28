using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList",menuName = "DataLists/Item")]
    
public class ItemListData : ScriptableObject   //�A�C�e���̃f�[�^�N���X�����X�g�������N���X(����킵����)
{
    [SerializeField]
    private List<ItemData> itemDataList = new List<ItemData>();

    public List<ItemData> ItemDataList { get { return itemDataList; } }
}

[System.Serializable]
public class ItemData   //�A�C�e���̃f�[�^�N���X
{
    [SerializeField]
    private string name;    //�A�C�e����
    [SerializeField]
    private int id;         //�A�C�e��ID
    [SerializeField]
    private int endurance;  //�A�C�e���̑ϋv��
    [SerializeField]
    private Sprite image;   //�A�C�e���̉摜
    [SerializeField]
    private string info;    //�A�C�e���̐�����

    //���ꂼ��̃����o�[�ϐ��̃Q�b�^�[(�ǂݎ���p)
    public string Name { get { return name; } }
    public int ID { get { return id; } }
    public int Endurance { get { return endurance; } }
    public Sprite Image { get { return image; } }
    public string Info { get { return info; } }
}
