using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HotBar : VisualElement
{

    private const int SIZE = 75;
    private VisualElement root;
    private List<CollectibleCell> cells;
    private VisualTreeAsset cellAsset;

    public HotBar(Sprite[] sprites, int[] capacities, string[] inputKeys, VisualTreeAsset cellAsset)
    {
        this.cellAsset = cellAsset;
        cells = new List<CollectibleCell>();
        root = new VisualElement();
        root.style.display = DisplayStyle.Flex;
        root.style.flexDirection = FlexDirection.RowReverse;
        root.style.height = SIZE;
        root.style.width = sprites.Length * SIZE;
        hierarchy.Add(root);
        for (int i = 0; i < sprites.Length; i++)
        {
            cells.Add(new CollectibleCell(sprites[i], capacities[i], inputKeys[i], SIZE, cellAsset));
            root.Add(cells[i]);
        }
    }

    public void UpdateCount(int index, int count)
    {
        cells[index].UpdateCount(count);
    }

    public void AddNewSlot(InventorySlot slot)
    {
        cells.Add(new CollectibleCell(slot.InventoryItem.ItemSprite, slot.capacity, slot.inputKey, SIZE, cellAsset));
        root.Add(cells[cells.Count - 1]);
        root.style.width = cells.Count * SIZE;
    }
}
