using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectibleCell : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<CollectibleCell> { }

    private int count;
    private int capacity;
    private Label countLabel;

    public CollectibleCell() : this(AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Collectibles/CrustyBread.png"), 30, "u", 50) { }

    public CollectibleCell(Sprite item, int capacity, string inputKey,int size)
    {
        this.count = 0;
        this.capacity = capacity;
        VisualElement cell = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/HUD/HotBar/CollectibleCell/CollectibleCell.uxml").Instantiate();
        cell.style.height = size;
        cell.style.width = size;
        hierarchy.Add(cell);
        VisualElement icon = cell.Query("Icon").First();
        icon.style.backgroundImage = new StyleBackground(item);
        countLabel = (Label)cell.Query("Count").First();
        Label inputKeyLabel = (Label)cell.Query("InputKey").First();
        inputKeyLabel.text = inputKey;
        UpdateCount(count);
    }

    public void UpdateCount(int count)
    {
        this.count = count;
        countLabel.text = count.ToString();
        if (count >= capacity)
        {
            countLabel.style.color = new StyleColor(new Color(1f, 0.5f, 0.5f));
        }
        else
        {
            countLabel.style.color = new StyleColor(new Color(1f, 1f, 1f));
        }
    }
}
