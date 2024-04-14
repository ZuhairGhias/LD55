using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    public float health;
    public List<InventorySlot> slots;
    private HealthBar healthBar;
    private HotBar hotBar;

    // Start is called before the first frame update
    void Start()
    {
        UIDocument ui = GetComponent<UIDocument>();
        healthBar = (HealthBar)ui.rootVisualElement.Query("HealthBar");
        hotBar = (HotBar)ui.rootVisualElement.Query("HotBar");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.updateHealth(health);
        for (int i = 0; i < slots.Count; i++)
        {
            hotBar.updateCount(i, slots[i].count);
        }
    }
}
