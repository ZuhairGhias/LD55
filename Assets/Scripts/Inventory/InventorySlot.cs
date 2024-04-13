using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{

    public InventoryItem InventoryItem;
    public int count = 0;
    public readonly int capacity = 5;

    // Only add if not at capacity. Return false if already at capacity. True if successfully added.
    public bool Add ()
    {
        if (!IsFull())
        {
            count++;
            return true;
        }
        return false;
    }

    // Only deplete if not empty. Return false if empty. True is successfully depleted.
    public bool Use()
    {
        if (!IsEmpty())
        {
            count--;
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
