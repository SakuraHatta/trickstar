using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    private List<ItemCard> cardSList = new List<ItemCard>();
    [SerializeField]
    private ItemListData ItemLD;

    private void Test1(int index)    //�����̃A�C�e���̃X�e�[�^�X��\������
    {
        Debug.Log("�A�C�e��ID[" + ItemLD.ItemDataList[index].id + "]�̖��O��["
            + ItemLD.ItemDataList[index].name + "]�ł��B"
            );

        Debug.Log("�l�i��[" + ItemLD.ItemDataList[index].price + "]�ł��B");
        Debug.Log("������ : " + ItemLD.ItemDataList[index].info);
    }

    public void Card()
    {
        foreach (ItemCard cardS in cardSList)
        {
            cardS.SetCard(ItemLD.ItemDataList[0]);
        }
    }
}
