using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryItem;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private ItemClass itemClass;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collectible collided with "+other);
        Inventory inventory = other.gameObject.GetComponent<Inventory>();
        
        if (inventory != null) {
            Debug.Log("Inventory: "+inventory.name);
            inventory.TryAddItem(itemClass);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
