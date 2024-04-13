using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static InventoryItem;

public class Collector : MonoBehaviour
{
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        DebugUtils.HandleErrorIfNullGetComponent(inventory, this);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colleded");
        Collectible collectible = other.gameObject.GetComponent<Collectible>();

        if (collectible != null)
        {
            Debug.Log("collectible " + collectible.name);
            inventory.TryAddItem(collectible.itemClass);
            Destroy(collectible.gameObject);
        }
    }
}