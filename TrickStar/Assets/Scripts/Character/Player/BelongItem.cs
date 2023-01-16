using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelongItem : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private List<BelongItemPanel> belongItemPanelS = new List<BelongItemPanel>();
    [SerializeField]
    private PlayerController playerCS;

    [SerializeField]
    private ItemListData ItemLD;

    private readonly int MAX_ITEMS; //持てるアイテムの数
    private int Mitems;              //持っているアイテム数

    private int Mselected;          //選択中のitem
    private bool Mdelete;           //削除モードのフラグ

    public BelongItem()
    {
        MAX_ITEMS = 3 - 1;  //持てるアイテムの最大数を決める
    }

    private void KeyController()//キー操作のメゾット
    {
        //左に選択を移動するとき
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveSelect(-1);
        }
        //右に選択を移動するとき
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveSelect(1);
        }
        //選択するとき
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UseItem();
        }
    }   

    private void MoveSelect(int v)  //選択を引数分動かすメゾット
    {
        Mselected += v;

        if (Mselected > MAX_ITEMS)
            Mselected = 0;
        else if (Mselected < 0)
            Mselected = MAX_ITEMS;
    }   

    private void UseItem()
    {
        playerCS.UseItem(Mselected);
    }   //選択してるアイテムを使用する

    private void DrawImages()//インベントリのアイテムのイメージを描くメゾット
    {
        int index = 0;
        foreach(int id in playerCS.GetItems())
        {
            belongItemPanelS[index].ChangeImage(ItemLD.ItemDataList[id].image);
            index++;
        }
    }   

    public void UpdateBelongItem()
    {
        KeyController();
    }   //インベントリを開いているときに実行するメゾット
}
