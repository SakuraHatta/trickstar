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

    //色関連
    private readonly Color NormalC = new Color(1.0f, 1.0f, 1.0f, 1.0f);   //通常の色
    private readonly Color UseC = new Color(0.0f, 1.0f, 0.0f, 1.0f);      //使用中の色
    private readonly Color CantUseC = new Color(1.0f, 0.0f, 0.0f, 1.0f);  //使用できない時の色
    private readonly Color NoneC = new Color(1.0f, 1.0f, 1.0f, 0.0f);     //何も持ってない時の色

    public BelongItem()
    {
        Mdelete = false;
    }

    private void KeyController()//キー操作のメゾット
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MoveSelect(0);
            UseItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveSelect(1);
            UseItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MoveSelect(2);
            UseItem();
        }

        //選択するとき
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    UseItem();
        //}
    }   

    private void MoveSelect(int v)  //選択を引数分動かすメゾット
    {
        Mselected = v;
    }   

    private void UseItem()   //選択してるアイテムを使用する
    {
        playerCS.UseItem(Mselected);

        ChangeColor();
    }   

    public void StartBelongItem()   //最初にする処理
    {
        for (int i = 0; i <= Const.MAX_ITEMS - 1; i++)  //アイテムパネルの数だけ繰り返す
        {
            belongItemPanelS[i].StartPanel();
            belongItemPanelS[i].ChangeColor(NoneC);
        }
    }

    //インベントリのアイテムのイメージを描くメゾット
    public void DrawImages()
    {
        int index = 0;
        foreach(BelongItemData bData in playerCS.GetItems())
        {
            //もし何もアイテムがないなら処理を中断する
            if (bData.MItemID == Const.NO_ITEM) { 
                continue;
                index++;
            }

            belongItemPanelS[index].ChangeImage(ItemLD.ItemDataList[bData.MItemID].Image);
            belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
            index++;
        }
    }   
    //インベントリのアイテムの色と大きさを変えるメゾット
    public void ChangeColor()
    {
        int index = 0;  //ループ回数
        foreach (BelongItemData bData in playerCS.GetItems())
        {
            //アイテムを持っていないとき
            if (bData.MItemID == Const.NO_ITEM)
            {
                belongItemPanelS[index].ChangeColor(NoneC);
                index++;
                continue;
            }
            //アイテムの耐久地がないとき
            else if ((bData.MEndurance == 0))
            {
                belongItemPanelS[index].ChangeColor(CantUseC);
                belongItemPanelS[index].UnChoose();
                belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
                index++;
                continue;
            }
            
            //使用中の時
            if (bData.MActive)
            {
                belongItemPanelS[index].ChangeColor(UseC);
                belongItemPanelS[index].Choose();
                belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
            }
            //使用中じゃないとき
            else
            {
                belongItemPanelS[index].ChangeColor(NormalC);
                belongItemPanelS[index].UnChoose();
                belongItemPanelS[index].ChangeEndurance(bData.MEndurance);
            }
            index++;
        }
    }

    public void OpenItems() //アイテム欄を開くメゾット
    {
        DrawImages();
    }
    public void CloseItems() //アイテム欄を閉じるメゾット
    {
        for(int i = 0; i < Const.MAX_ITEMS; i++)
        {
            //belongItemPanelS[i].UnChoose();
        }
    }

    public void UpdateBelongItem()//インベントリを開いているときに実行するメゾット
    {
        KeyController();
    }   
}
