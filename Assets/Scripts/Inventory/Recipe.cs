using System;
using System.Collections.Generic;
using UnityEngine;
using static InventoryItem;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Recipe", order = 2)]

public class Recipe : ScriptableObject
{
    public List<RecipeLine> ItemAmounts;
    public GameObject Summon;
}

[Serializable]
public class RecipeLine
{
    public ItemClass item;
    public int amount;
}
