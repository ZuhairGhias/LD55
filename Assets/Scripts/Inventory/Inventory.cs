using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryItem;

/// <summary>
/// Can be added to the player and configured in the editor. Colletion items should look for this item and add themselves probably. The Summon script will need to use the items here, making sure it isn't empty.
/// </summary>
public class Inventory : MonoBehaviour
{


    [SerializeField] public List<InventorySlot> slots;

    // Start is called before the first frame update
    void Start()
    {
        DebugUtils.HandleErrorIfNullGetComponent(slots, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Add item of type to the inventory
    /// </summary>
    /// <param name="item"></param>
    public bool TryAddItem(ItemClass item)
    {
        if (slots == null)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.InventoryItem.Class == item)
                {
                    return slot.Add();
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
        if (slots == null)
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
}
