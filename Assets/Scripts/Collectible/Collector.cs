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
        Collectible collectible = other.gameObject.GetComponent<Collectible>();

        if (collectible != null)
        {
            Debug.Log("collectible " + collectible.name);
            inventory.TryAddItem(collectible.itemClass);
            

            switch (collectible.itemClass)
            {
                case ItemClass.FRUIT:
                    AudioManager.PlaySoundEffect("fruit-pickup");
                    break;
                case ItemClass.CANDY:
                    AudioManager.PlaySoundEffect("candy-item-pickup");
                    break;
                case ItemClass.CRUSTY_BREAD:
                    AudioManager.PlaySoundEffect("stale-bread-pickup");
                    break;
            }

            Destroy(collectible.gameObject);
        }
    }
}
