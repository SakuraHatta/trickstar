using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private List<ItemBaseScript> ItemBoxList = new List<ItemBaseScript>();
    
    public void ActiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].SetActive(Char);
    }

    public void PassiveItems(CharacterBase Char, int id)
    {
        ItemBoxList[id].SetPassive(Char);
    }
}
