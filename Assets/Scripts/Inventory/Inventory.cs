using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryItem;
using static UnityEditor.Progress;

/// <summary>
/// Can be added to the player and configured in the editor. Colletion items should look for this item and add themselves probably. The Summon script will need to use the items here, making sure it isn't empty.
/// </summary>
public class Inventory : MonoBehaviour
{


    [SerializeField] public List<InventorySlot> slots;
    public Dictionary<ItemClass, int> stagedItems;
    private const int stagedItemLimit = 3;

    // Start is called before the first frame update
    void Start()
    {
        DebugUtils.HandleErrorIfNullGetComponent(slots, this);

        stagedItems = new Dictionary<ItemClass, int>();
    }

    // Adds an item to the staging area if possible, taking it from the inventory.
    // Returns false if staging area is full or if there are no items in the inventory of the type.
    public bool Stage(ItemClass item)
    {
        if(!IsStageFull() && TryUseItem(item))
        {
            if(!stagedItems.ContainsKey(item))
            {
                stagedItems.Add(item, 0);
            }

            stagedItems[item]++;
            return true;
        }

        return false;
    }

    // Adds an item to the staging area if possible, taking it from the inventory
    public void ReturnStagedItems()
    {
        foreach(ItemClass item in stagedItems.Keys)
        {
            //Try adding items back one by one
            TryAddItem(item, stagedItems[item]);
        }
        stagedItems.Clear();

    }

    public void ConsumeStagedItems()
    {
        stagedItems.Clear();

    }

    public bool IsStageFull()
    {
        int count = 0;
        foreach(int i in stagedItems.Values)
        {
            count += i;
        }

        return count >= stagedItemLimit;
    }

    /// <summary>
    /// Add item of type to the inventory
    /// </summary>
    /// <param name="item"></param>
    public bool TryAddItem(ItemClass item, int times = 1)
    {
        if (slots != null)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.InventoryItem.Class == item)
                {
                    return slot.Add(times);
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// Use item of type from the inventory
    /// </summary>
    /// <param name="item"></param>
    public bool TryUseItem(ItemClass item)
    {
        if (slots != null)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.InventoryItem.Class == item)
                {
                    return slot.Use();
                }
            }
        }
        return false;
    }

    internal bool IsStaged(ItemClass item, int amount)
    {
        return stagedItems.ContainsKey(item) && stagedItems[item] == amount;
    }
}
