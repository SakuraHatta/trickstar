using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList",menuName = "DataLists/Item")]
    
public class ItemListData : ScriptableObject   //アイテムのデータクラスをリスト化したクラス(紛らわしいね)
{
    [SerializeField]
    private List<ItemData> itemDataList = new List<ItemData>();

    public List<ItemData> ItemDataList { get { return itemDataList; } }
}

[System.Serializable]
public class ItemData   //アイテムのデータクラス
{
    [SerializeField]
    private string name;    //アイテム名
    [SerializeField]
    private int id;         //アイテムID
    [SerializeField]
    private int endurance;  //アイテムの耐久力
    [SerializeField]
    private Sprite image;   //アイテムの画像
    [SerializeField]
    private string info;    //アイテムの説明文

    //それぞれのメンバー変数のゲッター(読み取り専用)
    public string Name { get { return name; } }
    public int ID { get { return id; } }
    public int Endurance { get { return endurance; } }
    public Sprite Image { get { return image; } }
    public string Info { get { return info; } }
}
