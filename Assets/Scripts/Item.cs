using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public ItemCategory category;
    public Sprite icon;
    public string description;
    public int buyPrice;
    public int sellPrice;
    public float weight;
    public Rarity rarity;
    public int quantity;

    public Item(string name, ItemCategory category, Sprite icon, string description, int buyPrice, int sellPrice, float weight, Rarity rarity, int quantity = 1)
    {
        this.name = name;
        this.category = category;
        this.icon = icon;
        this.description = description;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
        this.weight = weight;
        this.rarity = rarity;
        this.quantity = quantity;
    }
}
