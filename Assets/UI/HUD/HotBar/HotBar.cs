using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HotBar : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<HotBar> { }

    private const int SIZE = 75;
    private VisualElement root;
    private List<CollectibleCell> cells;

    private static Sprite[] sampleSprites = new Sprite[]
    {
        AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Collectibles/BananaPeel.png"),
        AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Collectibles/CandyWrap.png"),
        AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Collectibles/CrustyBread.png")
    };

    public HotBar(): this(sampleSprites, new int[3] { 30, 30, 30 }) { }

    public HotBar(Sprite[] sprites, int[] capacities)
    {
        cells = new List<CollectibleCell>();
        root = new VisualElement();
        root.style.display = DisplayStyle.Flex;
        root.style.flexDirection = FlexDirection.RowReverse;
        root.style.height = SIZE;
        root.style.width = sprites.Length * SIZE;
        hierarchy.Add(root);
        for (int i = 0; i < sprites.Length; i++)
        {
            cells.Add(new CollectibleCell(sprites[i], capacities[i], SIZE));
            root.Add(cells[i]);
        }
    }

    public void UpdateCount(int index, int count)
    {
        cells[index].UpdateCount(count);
    }

    public void AddNewSlot(InventorySlot slot)
    {
        cells.Add(new CollectibleCell(slot.InventoryItem.ItemSprite, slot.capacity, SIZE));
        root.Add(cells[cells.Count - 1]);
        root.style.width = cells.Count * SIZE;
    }
}
