using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using static InventoryItem;

/// <summary>
/// Can be added to the player and configured in the editor. Colletion items should look for this item and add themselves probably. The Summon script will need to use the items here, making sure it isn't empty.
/// </summary>
public class Inventory : MonoBehaviour
{


    [SerializeField] public List<InventorySlot> slots;
    [SerializeField] private GameObject[] stagedObjects;
    [SerializeField] private InventoryItem[] itemData;
    public Dictionary<ItemClass, int> stagedItems;
    private const int stagedItemLimit = 3;
    private int itemsToFail;
    private int itemsToSucceed;

    public static Action<ItemClass, int> InventoryUpdate;

    // Start is called before the first frame update
    void Start()
    {
        DebugUtils.HandleErrorIfNullGetComponent(slots, this);

        stagedItems = new Dictionary<ItemClass, int>();

        //Initialize listeners based on default state
        foreach (InventorySlot slot in slots)
        {
                InventoryUpdate?.Invoke(slot.InventoryItem.Class, slot.count); 
        }
    }

    // Adds an item to the staging area if possible, taking it from the inventory.
    // Returns false if staging area is full or if there are no items in the inventory of the type.
    public bool Stage(ItemClass item)
    {
        if(!IsStageFull() && TryUseItem(item))
        {
            stagedObjects[CountStagedItems()].GetComponent<SpriteRenderer>().sprite = ItemClassToSprite(item);
            stagedObjects[CountStagedItems()].GetComponent<Animator>().Play("Base Layer.Stage");

            if(!stagedItems.ContainsKey(item))
            {
                stagedItems.Add(item, 0);
            }

            stagedItems[item]++;

            switch (CountStagedItems())
            {
                case 1:
                    AudioManager.PlaySoundEffect("summon-01");
                    break;
                case 2:
                    AudioManager.PlaySoundEffect("summon-02");
                    break;
                case 3:
                    AudioManager.PlaySoundEffect("summon-03");
                    break;
            }

            return true;
        }

        return false;
    }

    private Sprite ItemClassToSprite(ItemClass item)
    {
        foreach (InventoryItem data in itemData)
        {
            if (data.Class == item)
            {
                return data.ItemSprite;
            }
        }
        return null;
    }

    // Adds an item to the staging area if possible, taking it from the inventory
    public void ReturnStagedItems()
    {
        itemsToFail = CountStagedItems();

        foreach(ItemClass item in stagedItems.Keys)
        {
            //Try adding items back one by one
            TryAddItem(item, stagedItems[item]);
        }

        stagedItems.Clear();
    }

    public void ConsumeStagedItems()
    {
        itemsToSucceed = CountStagedItems();

        stagedItems.Clear();
    }

    public bool IsStageFull()
    {
        return CountStagedItems() >= stagedItemLimit;
    }

    public int CountStagedItems()
    {
        int count = 0;
        foreach (int i in stagedItems.Values)
        {
            count += i;
        }
        return count;
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
                    if (slot.Add(times))
                    {
                        InventoryUpdate?.Invoke(slot.InventoryItem.Class, slot.count);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                    if(slot.Use())
                    {
                        InventoryUpdate?.Invoke(slot.InventoryItem.Class, slot.count);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }

    internal bool IsStaged(ItemClass item, int amount)
    {
        return stagedItems.ContainsKey(item) && stagedItems[item] == amount;
    }

    private void Update()
    {
        for (int i = 0; i < itemsToFail; i++)
        {
            stagedObjects[i].GetComponent<Animator>().Play("Base Layer.Fail");
        }

        for (int i = 0; i < itemsToSucceed; i++)
        {
            stagedObjects[i].GetComponent<Animator>().Play("Base Layer.Success");
        }

        itemsToFail = 0;
        itemsToSucceed = 0;
    }
}
