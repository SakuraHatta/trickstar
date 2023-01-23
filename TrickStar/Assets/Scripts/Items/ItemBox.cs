using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private List<ItemBaseScript> ItemBoxList = new List<ItemBaseScript>();
    
    //引数のキャラクター型のオブジェクトに、引数のidのアイテムを発動させる
    public void ActiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].SetActive(Char);
    }
    //引数のキャラクター型のオブジェクトに、引数のidのアイテムの効果を取り除く
    public void PassiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].SetPassive(Char);
    }
}
