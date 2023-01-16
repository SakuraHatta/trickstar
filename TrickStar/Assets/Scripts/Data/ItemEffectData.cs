using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList", menuName = "DataLists/ItemEffect")]
public class ItemEffectData : ScriptableObject
{
    public List<ItemEffect> Effects = new List<ItemEffect>();
}

[System.Serializable]
public class ItemEffect
{
    public int maxhp;
    public float jumppower;
    public float speed;
}
