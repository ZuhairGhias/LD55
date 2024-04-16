using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{

    public InventoryItem InventoryItem;
    public int count;
    public int capacity;
    public string inputKey;

    // Only add if not at capacity. Return false if already at capacity. True if successfully added.
    public bool Add(int i = 1)
    {
        if (!IsFull())
        {
            count =  Mathf.Min(count + i, capacity);
            return true;
        }
        return false;
    }

    // Only deplete if not empty. Return false if empty. True is successfully depleted.
    public bool Use(int i = 1)
    {
        if (!IsEmpty())
        {
            count = Mathf.Max(count - i, 0);
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return count == capacity;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
}
