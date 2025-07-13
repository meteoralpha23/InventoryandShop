using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemCategory category;
    public int cost;
    public string description;
    public Rarity rarity;
}

public enum ItemCategory { Weapon, Consumable, Material, Treasure }
public enum Rarity { VeryCommon, Common, Rare, Epic, Legendary }
