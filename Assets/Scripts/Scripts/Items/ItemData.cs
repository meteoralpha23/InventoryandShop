using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public Sprite descriptionImage; // Large image for right-side panel
    public Sprite draggableSprite; // Sprite for dragging in inventory grid
    public ItemCategory category;
    public int cost;
    public Rarity rarity;
    public float weight;

    [Header("Weapon-specific")]
    public int bulletCount;

    // --- Only stat fields for right-side panel ---
    public float power;
    public int ammoCapacity;
    public float reloadSpeed;
    public float rateOfFire;
    public float precision;
}

public enum ItemCategory { Weapon, Consumable, Material, Treasure }
public enum Rarity { VeryCommon, Common, Rare, Epic, Legendary }
