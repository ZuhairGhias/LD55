using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Heart : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory: UxmlFactory<Heart> { }

    private float percentFill;
    private VisualElement foreground;

    public Heart() : this(50f, 50) { }

    public Heart(float percentFill, int size)
    {
        this.percentFill = percentFill;
        VisualElement heart = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/HUD/HealthBar/Heart/Heart.uxml").Instantiate();
        foreground = heart.Query("HeartForeground").First();
        foreground.style.width = Length.Percent(0f);
        SetPercentFill(percentFill);
        heart.style.height = size;
        heart.style.width = size;
        hierarchy.Add(heart);
    }

    public void SetPercentFill(float percentFill)
    {
        foreground.style.width = Length.Percent(percentFill);
    }
}
