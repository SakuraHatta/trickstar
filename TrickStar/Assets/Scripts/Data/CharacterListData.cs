using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName = "DataLists/Character")]
public class CharacterListData : ScriptableObject
{
    [SerializeField]
    private List<CharacterData> characterDataList = new List<CharacterData>();

    public List<CharacterData> CharacterDataList { get { return characterDataList; } }
}

[System.Serializable]
public class CharacterData  //�L�����N�^�[�f�[�^�N���X
{
    [SerializeField]
    public string name;     //���O
    [SerializeField]
    public int maxhp;       //�ő�̗�
    [SerializeField]
    public int power;      //�U����
    [SerializeField]
    public float jumppower; //�W�����v��
    [SerializeField]
    public float speed;     //�ړ����x

    public string Name { get { return name; } }
    public int Maxhp { get { return maxhp; } }
    public int Power { get { return power; } }
    public float Jumppower { get { return jumppower; } }
    public float Speed { get { return speed; } }
}
