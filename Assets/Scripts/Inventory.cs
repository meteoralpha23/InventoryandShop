using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item.ItemType> itemList = new List<Item.ItemType>();
    [SerializeField] private Inventory_UI inventoryUI;

    public void AddItem(Item.ItemType itemType)
    {
        if (!itemList.Contains(itemType))
        {
            itemList.Add(itemType);
            Debug.Log("Item added to inventory: " + itemType);
            inventoryUI?.RefreshInventory(itemList); 
        }
    }

    public bool HasItem(Item.ItemType itemType)
    {
        return itemList.Contains(itemType);
    }

    public List<Item.ItemType> GetAllItems()
    {
        return itemList;
    }
    public void RemoveItem(Item.ItemType itemType)
    {
        if (itemList.Contains(itemType))
        {
            itemList.Remove(itemType);
            inventoryUI?.RefreshInventory(itemList);
        }
    }
}
