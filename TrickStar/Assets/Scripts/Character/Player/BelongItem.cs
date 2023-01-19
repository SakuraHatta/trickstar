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

    [Header("Data")]
    [SerializeField]
    private ItemListData ItemLD;

    private int Mselected;          //選択中のitem
    private bool Mdelete;           //削除モードのフラグ

    public BelongItem()
    {
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

        if (Mselected > Const.MAX_ITEMS - 1)
            Mselected = 0;
        else if (Mselected < 0)
            Mselected = Const.MAX_ITEMS - 1;

        belongItemPanelS[Mselected].Choose();

        Debug.Log("選択アイテム : " + Mselected);
    }   

    public void StartBelongItem()   //最初にする処理
    {
        for (int i = 0; i <= Const.MAX_ITEMS - 1; i++)  //アイテムパネルの数だけ繰り返す
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

    public void UseItem()   //選択してるアイテムを使用する
    {
        playerCS.UseItem(Mselected);
    }   

    public void OpenItems() //アイテム欄を開くメゾット
    {
        Mselected = 0;
        belongItemPanelS[Mselected].Choose();
        DrawImages();
    }
    public void CloseItems() //アイテム欄を閉じるメゾット
    {
        for(int i = 0; i < Const.MAX_ITEMS; i++)
        {
            belongItemPanelS[i].UnChoose();
        }
    }

    public void UpdateBelongItem()//インベントリを開いているときに実行するメゾット
    {
        KeyController();
    }   
}
