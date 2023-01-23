using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopObjectScript : MonoBehaviour
{
    [Tooltip("この店で必ず売られているアイテムのID")]
    [SerializeField]
    private int SetItem;    //この店で必ず売られているアイテムID
    private int[] itemId = new int[Const.CARD_NUMBER];     //この店で売られているアイテムのIDのList

    private Vector2 thisPos;    //このショップの位置

    private int GetRandomId(int max) { return Random.Range(0, max); }

    //初期処理
    public void StartShopObject()
    {
        thisPos = this.transform.position;  //ショップの位置を決める

        while (true)
        {
            for (int i = 0; i < Const.CARD_NUMBER; i++)   //カードの数だけ繰り返す
            {
                itemId[i] = GetRandomId(Const.TYPE_ITEMS);  //アイテムのIDをランダムに決める
            }
            //SetItemのアイテムIDがあるかを探す
            var result = itemId.Any(e => e == SetItem); 

            if (result) { break; }  //あったらループを終了/無かったらまたIDを決める
        }
    }

    //playerが店の近くにいるか確認するメゾット
    public bool CheckDistance(Vector2 plrpos)
    {
        float distance = (thisPos - plrpos).magnitude;   //この店の位置とプレイヤーの位置
        //距離が店の大きさより小さいとき
        if (distance < Const.ITEM_SCALE)
        {
            return true;
        }   
        return false;
    }

    //売られているアイテムのIdの配列を返す
    public int[] GetIDArray() { return itemId; }
}
