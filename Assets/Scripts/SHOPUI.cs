using UnityEngine;
using System.Collections.Generic;

public class SHOPUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject itemPrefab;

    private List<ItemData> itemsForSale = new();
    private IShopCustomer currentCustomer;

    public void Show(IShopCustomer customer)
    {
        currentCustomer = customer;
        RefreshUI();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetItems(List<ItemData> items)
    {
        itemsForSale = items;
        RefreshUI();
    }

    public void AddItemToShop(ItemData item)
    {
        if (!itemsForSale.Contains(item))
        {
            itemsForSale.Add(item);
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        foreach (Transform child in container)
            if (child != itemPrefab.transform) Destroy(child.gameObject);

        foreach (var item in itemsForSale)
        {
            var go = Instantiate(itemPrefab, container);
            var itemUI = go.GetComponent<InventoryItemUI>();
            itemUI.Setup(item, true, item.cost, "Buy", () =>
            {
                if (currentCustomer.BoughtItem(item))
                {
                    itemsForSale.Remove(item);
                    RefreshUI();
                }
            });
            go.SetActive(true);
        }
    }
}
