using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<HealthBar> { }

    public int maxHealth;
    public float health;
    private int SIZE = 50;

    public HealthBar()
    {
        maxHealth = 5;
        health = 2.5f;
        CreateGUI();
    }

    public HealthBar(int maxHealth, float health)
    {
        this.maxHealth = maxHealth;
        this.health = health;
        CreateGUI();
    }

    public void CreateGUI()
    {
        VisualElement root = new VisualElement();
        root.style.display = DisplayStyle.Flex;
        root.style.flexDirection = FlexDirection.Row;
        root.style.height = SIZE;
        root.style.width = maxHealth * SIZE;
        hierarchy.Add(root);
        for (int i = 0; i < maxHealth; i++)
        {
            if (i > health) // Empty Hearts
            {
                root.Add(new Heart(0f, SIZE));
            }
            else if (i + 1 > health) // Partially Filled Hearts
            {
                root.Add(new Heart((health % 1) * 100, SIZE));
            }
            else // Filled Hearts
            {
                root.Add(new Heart(100f, SIZE));
            }
        }
    }
}
