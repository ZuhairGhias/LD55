using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryItem;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private ItemClass itemClass;

    private void OnTriggerEnter(Collider other) {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null) {
            inventory.TryAddItem(itemClass);
            GameObject.Destroy(this);
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
