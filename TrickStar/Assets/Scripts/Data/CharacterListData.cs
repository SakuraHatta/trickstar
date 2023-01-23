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
public class CharacterData  //キャラクターデータクラス
{
    [SerializeField]
    public string name;     //名前
    [SerializeField]
    public int maxhp;       //最大体力
    [SerializeField]
    public int power;      //攻撃力
    [SerializeField]
    public float jumppower; //ジャンプ力
    [SerializeField]
    public float speed;     //移動速度

    public string Name { get { return name; } }
    public int Maxhp { get { return maxhp; } }
    public int Power { get { return power; } }
    public float Jumppower { get { return jumppower; } }
    public float Speed { get { return speed; } }
}
