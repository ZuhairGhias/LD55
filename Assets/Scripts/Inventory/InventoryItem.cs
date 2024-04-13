using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{

    public string ItemName;
    public ItemClass Class;
    public Sprite ItemSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum ItemClass
    {
        CRUSTY_BREAD,
        CANDY,
        FRUIT
    }
}
