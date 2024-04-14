using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<HealthBar> { }

    private int maxHealth;
    private float health;
    private const int SIZE = 75;
    private VisualElement root;
    private List<Heart> hearts;

    public HealthBar(): this(5) { }

    public HealthBar(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        hearts = new List<Heart>();
        root = new VisualElement();
        root.style.display = DisplayStyle.Flex;
        root.style.flexDirection = FlexDirection.Row;
        root.style.height = SIZE;
        root.style.width = maxHealth * SIZE;
        hierarchy.Add(root);
        for (int i = 0; i < maxHealth; i++)
        {
            hearts.Add(new Heart(0, SIZE));
            root.Add(hearts[i]);
        }
        UpdateHealth(health);
    }

    public void UpdateHealth(float health)
    {
        this.health = health;
        for (int i = 0; i < maxHealth; i++)
        {
            if (i > health) // Empty Hearts
            {
                hearts[i].SetPercentFill(0f);
            }
            else if (i + 1 > health) // Partially Filled Hearts
            {
                hearts[i].SetPercentFill((health % 1) * 100);
            }
            else // Filled Hearts
            {
                hearts[i].SetPercentFill(100f);
            }
        }
    }
}
