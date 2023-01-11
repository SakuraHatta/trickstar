using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList",menuName = "DataLists/Item")]
public class ItemListData : ScriptableObject   //アイテムのデータクラスをリスト化したクラス(紛らわしいね)
{
    public List<ItemData> ItemDataList = new List<ItemData>(); 
}

[System.Serializable]
public class ItemData   //アイテムのデータクラス
{
    public int id;         //アイテムID
    public string name;    //アイテム名
    public int price;      //アイテムの値段
    public Sprite image;   //アイテムの画像
    public string info;    //アイテムの説明文
}
