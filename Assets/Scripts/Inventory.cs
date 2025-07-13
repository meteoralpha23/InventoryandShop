using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;
    private List<ItemData> itemList = new();

    public void AddItem(ItemData item)
    {
        if (!itemList.Contains(item))
        {
            itemList.Add(item);
            OnInventoryChanged?.Invoke();
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (itemList.Remove(item))
            OnInventoryChanged?.Invoke();
    }

    public List<ItemData> GetAllItems() => new(itemList);
    public bool HasItem(ItemData item) => itemList.Contains(item);
}
