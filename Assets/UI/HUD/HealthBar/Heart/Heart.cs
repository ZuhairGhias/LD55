using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Heart : VisualElement
{

    private float percentFill;
    private VisualElement foreground;

    public Heart(float percentFill, int size, VisualTreeAsset heartAsset)
    {
        this.percentFill = percentFill;
        VisualElement heart = heartAsset.Instantiate();
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
