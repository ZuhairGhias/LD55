using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryItem;

public class PlayerSummoner : MonoBehaviour
{
    Inventory inventory;

    public List<Recipe> Recipes;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        DebugUtils.HandleErrorIfNullGetComponent(inventory, this);
    }

    public void StageItem(ItemClass item)
    {
        if (inventory.Stage(item))
        {
            Debug.Log("Staged " + item.ToString());

            //Check if we have the right ingredients
            foreach (Recipe recipe in Recipes)
            {
                if (CanMakeRecipe(recipe))
                {
                    Debug.Log("Made a recipe");
                    inventory.ConsumeStagedItems();
                }
            }

            if (inventory.IsStageFull())
            {
                Debug.Log("Stage is full. Could not make a recipe. Clearing.");

                inventory.ReturnStagedItems();
            }
        }
        else
        {
            // We don't have enough
            inventory.ReturnStagedItems();
            Debug.Log("Could not stage " + item.ToString());

        }
    }

    public bool CanMakeRecipe(Recipe recipe)
    {
        foreach (RecipeLine line in recipe.ItemAmounts)
        {
            if (!inventory.IsStaged(line.item, line.amount))
            {
                return false;
            }
        }

        return true;
    }
}
