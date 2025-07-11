using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<Item> allItems = new List<Item>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadItems();
    }

    private void LoadItems()
    {
        Item shotgun = new Item(
            itemName: "Shotgun",
            category: ItemCategory.Weapons,
            icon: GameAssets.i.Shotgun,
            description: "A powerful close-range firearm.",
            buyingPrice: 500,
            sellingPrice: 300,
            weight: 5.0f,
            rarity: Rarity.Rare,
            quantity: 1
        );

        allItems.Add(shotgun);
    }

    public Item GetItemByName(string itemName)
    {
        return allItems.Find(item => item.itemName == itemName);
    }

    public List<Item> GetItemsByCategory(ItemCategory category)
    {
        return allItems.FindAll(item => item.category == category);
    }
}
