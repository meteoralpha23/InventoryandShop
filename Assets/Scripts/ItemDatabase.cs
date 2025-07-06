using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    private void Awake()
    {
        Instance = this;
        LoadItems();
    }

    public List<Item> allItems = new List<Item>();

    private void LoadItems()
    {
        // Add sample items here
        allItems.Add(new Item(
            name: "Shotgun",
            category: ItemCategory.Weapon,
            icon: GameAssets.i.Shotgun,
            description: "A powerful close-range firearm.",
            buyPrice: 500,
            sellPrice: 300,
            weight: 5.0f,
            rarity: Rarity.Rare,
            quantity: 1
        ));

        // Add more test items similarly...
    }

    public Item GetItemByName(string name)
    {
        return allItems.Find(i => i.name == name);
    }

    public List<Item> GetItemsByCategory(ItemCategory category)
    {
        return allItems.FindAll(i => i.category == category);
    }
}
