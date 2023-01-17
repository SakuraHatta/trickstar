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
        Mselected = 0;
        Mdelete = false;
    }

    private void KeyController()//キー操作のメゾット
    {
        //左に選択を移動するとき
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelect(-1);
        }
        //右に選択を移動するとき
        if (Input.GetKeyDown(KeyCode.RightArrow))
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
        belongItemPanelS[Mselected].UnChoose();

        Mselected += v;

        if (Mselected > MAX_ITEMS)
            Mselected = 0;
        else if (Mselected < 0)
            Mselected = MAX_ITEMS;

        belongItemPanelS[Mselected].Choose();

        Debug.Log("選択アイテム : " + Mselected);
    }   

    public void StartBelongItem()   //最初にする処理
    {
        for (int i = 0; i <= MAX_ITEMS; i++)  //アイテムパネルの数だけ繰り返す
        {
            belongItemPanelS[i].StartPanel();
        }
    }

    public void DrawImages()//インベントリのアイテムのイメージを描くメゾット
    {
        int index = 0;
        foreach(int id in playerCS.GetItems())
        {
            belongItemPanelS[index].ChangeImage(ItemLD.ItemDataList[id].image);
            index++;
        }
    }   

    public void UseItem()
    {
        playerCS.UseItem(Mselected);
    }   //選択してるアイテムを使用する

    public void OpenItems() //アイテム欄を開くメゾット
    {
        Mselected = 0;
        belongItemPanelS[Mselected].Choose();
        DrawImages();
    }
    public void CloseItems() //アイテム欄を閉じるメゾット
    {
        for(int i = 0; i < MAX_ITEMS; i++)
        {
            belongItemPanelS[i].UnChoose();
        }
    }

    public void UpdateBelongItem()//インベントリを開いているときに実行するメゾット
    {
        KeyController();
    }   
}
