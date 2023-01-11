using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName = "DataLists/Character")]
public class CharacterListData : ScriptableObject
{
    public List<CharacterData> CharacterDataList = new List<CharacterData>();
}

[System.Serializable]
public class CharacterData  //�L�����N�^�[�f�[�^�N���X
{
    public string name;     //���O
    public int maxhp;       //�ő�̗�
    public int attack;      //�U����
    public float jumppower; //�W�����v��
    public float speed;     //�ړ����x
}
