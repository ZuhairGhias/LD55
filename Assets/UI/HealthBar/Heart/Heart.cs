using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Heart : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory: UxmlFactory<Heart> { }

    public float percentFill;
    public int size;

    public Heart()
    {
        percentFill = 50f;
        size = 50;
        CreateGUI();
    }

    public Heart(float percentFill, int size)
    {
        this.percentFill = percentFill;
        this.size = size;
        CreateGUI();
    }

    public void CreateGUI()
    {
        // Instantiate UXML
        VisualElement heart = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/HealthBar/Heart/Heart.uxml").Instantiate();
        VisualElement foreground = heart.Query("HeartForeground").First();
        foreground.style.width = Length.Percent(percentFill);
        heart.style.height = size;
        heart.style.width = size;
        hierarchy.Add(heart);
    }
}
