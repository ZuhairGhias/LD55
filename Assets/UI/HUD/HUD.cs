using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private VisualTreeAsset hotBarCellAsset;
    [SerializeField] private VisualTreeAsset heartAsset;
    [SerializeField] private StyleSheet hudStyles;
    public float health;
    public List<InventorySlot> slots;
    private VisualElement hud;
    private HealthBar healthBar;
    private HotBar hotBar;

    private void Awake()
    {
        PlayerController.PlayerDamage += OnPlayerDamage;
        Inventory.InventoryUpdate += OnInventoryUpdate;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDocument ui = GetComponent<UIDocument>();
        hud = ui.rootVisualElement;
        hud.styleSheets.Add(hudStyles);
        hud.Clear();

        // Set up health bar
        healthBar = new HealthBar(maxHealth, heartAsset);
        healthBar.name = "HealthBar";
        hud.Add(healthBar);

        // Set up hot bar
        Sprite[] sprites = new Sprite[slots.Count];
        int[] capacities = new int[slots.Count];
        string[] inputKeys = new string[slots.Count];
        for (int i = 0; i < slots.Count; i++)
        {
            sprites[i] = slots[i].InventoryItem.ItemSprite;
            capacities[i] = slots[i].capacity;
            inputKeys[i] = slots[i].inputKey;
        }
        hotBar = new HotBar(sprites, capacities, inputKeys, hotBarCellAsset);
        hotBar.name = "HotBar";
        hud.Add(hotBar);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateHealth(health);
        for (int i = 0; i < slots.Count; i++)
        {
            hotBar.UpdateCount(i, slots[i].count);
        }
    }

    void OnPlayerDamage(float healthRatio)
    {
        healthRatio *= maxHealth;
        health = healthRatio;
    }

    void OnInventoryUpdate(InventoryItem.ItemClass itemClass, int currentAmount)
    {
        foreach (InventorySlot Slot in slots)
        {
            if ( Slot.InventoryItem.Class == itemClass )
            {
                Slot.count = currentAmount;
            }
        }
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDamage -= OnPlayerDamage;
        Inventory.InventoryUpdate -= OnInventoryUpdate;
    }
}
