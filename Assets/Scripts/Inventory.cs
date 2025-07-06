using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item_RE.ItemType> itemList = new List<Item_RE.ItemType>();
    [SerializeField] private Inventory_UI inventoryUI;

    public void AddItem(Item_RE.ItemType itemType)
    {
        if (!itemList.Contains(itemType))
        {
            itemList.Add(itemType);
            Debug.Log("Item added to inventory: " + itemType);
            inventoryUI?.RefreshInventory(itemList); 
        }
    }

    public bool HasItem(Item_RE.ItemType itemType)
    {
        return itemList.Contains(itemType);
    }

    public List<Item_RE.ItemType> GetAllItems()
    {
        return itemList;
    }
    public void RemoveItem(Item_RE.ItemType itemType)
    {
        if (itemList.Contains(itemType))
        {
            itemList.Remove(itemType);
            inventoryUI?.RefreshInventory(itemList);
        }
    }
}
