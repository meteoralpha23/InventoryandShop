using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SHOP_UI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject shopItemPrefab;

    private IShopCustomer shopCustomer;
    private Inventory inventory;
    [SerializeField] private Inventory_UI inventoryUI;

    private Dictionary<Item_RE.ItemType, GameObject> shopItemButtons = new();

    private void Start()
    {
        int index = 0;
        foreach (Item_RE.ItemType itemType in System.Enum.GetValues(typeof(Item_RE.ItemType)))
        {
            CreateItemButton(itemType, Item_RE.GetSprite(itemType), itemType.ToString(), Item_RE.GetCost(itemType), index);
            index++;
        }

        Hide();
    }


    private void CreateItemButton(Item_RE.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        if (inventory != null && inventory.HasItem(itemType))
        {
            return; // Don't show already-owned item
        }

        GameObject shopItemGO = Instantiate(shopItemPrefab, container);
        RectTransform shopItemRectTransform = shopItemGO.GetComponent<RectTransform>();
        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemGO.transform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemGO.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemGO.transform.Find("Icon").GetComponent<Image>().sprite = itemSprite;

        shopItemGO.transform.Find("Background").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (shopCustomer is Player player)
            {
                int playerGold = player.GetGoldAmount();
                int cost = Item_RE.GetCost(itemType);

                if (playerGold < cost)
                {
                    UIManager.Instance.ShowWarning("Not enough gold!");
                    return;
                }

                // Show confirmation popup before buying
                UIManager.Instance.ShowBuyConfirmationPopup(itemType, () =>
                {
                    TryBuyItem(itemType);
                });
            }
        });

        shopItemButtons[itemType] = shopItemGO;
    }

    private void TryBuyItem(Item_RE.ItemType itemType)
    {
        if (inventory == null || inventory.HasItem(itemType)) return;

        shopCustomer.BoughtItem(itemType);
        inventory.AddItem(itemType);

        if (shopItemButtons.TryGetValue(itemType, out GameObject itemGO))
        {
            Destroy(itemGO);
            shopItemButtons.Remove(itemType);
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;

        if (shopCustomer is Player player)
        {
            inventory = player.GetComponent<Inventory>();
            UIManager.Instance.UpdateGoldUI(player.GetGoldAmount());

            inventoryUI.Init(inventory, this, player);
        }

        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddItemToShop(Item_RE.ItemType itemType)
    {
        if (inventory != null && inventory.HasItem(itemType)) return;

        if (!shopItemButtons.ContainsKey(itemType))
        {
            int index = shopItemButtons.Count;
            CreateItemButton(itemType, Item_RE.GetSprite(itemType), itemType.ToString(), Item_RE.GetCost(itemType), index);
        }
    }
}
