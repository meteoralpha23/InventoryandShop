using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;
    private List<InventoryItem> ownedItems = new();

    public void AddItem(ItemData item, int quantity = 1)
    {
        var existing = ownedItems.Find(x => x.data == item);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            ownedItems.Add(new InventoryItem(item, quantity));
        }

        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemData data, int quantity = 1)
    {
        var item = ownedItems.Find(i => i.data == data);
        if (item != null)
        {
            item.quantity -= quantity;
            if (item.quantity <= 0)
                ownedItems.Remove(item);

            OnInventoryChanged?.Invoke();
        }
    }

    public List<InventoryItem> GetAllItems() => new(ownedItems);

    public float GetCurrentWeight()
    {
        return ownedItems.Sum(item => item.GetTotalWeight());
    }

    public bool HasItem(ItemData item)
    {
        return ownedItems.Exists(x => x.data == item);
    }

    public int GetItemQuantity(ItemData item)
    {
        var inventoryItem = ownedItems.Find(x => x.data == item);
        return inventoryItem?.quantity ?? 0;
    }

    public List<InventoryItem> GetItemsByCategory(ItemCategory category)
    {
        return ownedItems.FindAll(item => item.data.category == category);
    }
}
