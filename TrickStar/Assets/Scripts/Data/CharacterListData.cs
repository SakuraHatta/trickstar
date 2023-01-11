using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName = "DataLists/Character")]
public class CharacterListData : ScriptableObject
{
    public List<CharacterData> CharacterDataList = new List<CharacterData>();
}

[System.Serializable]
public class CharacterData  //キャラクターデータクラス
{
    public string name;     //名前
    public int maxhp;       //最大体力
    public int attack;      //攻撃力
    public float jumppower; //ジャンプ力
    public float speed;     //移動速度
}
