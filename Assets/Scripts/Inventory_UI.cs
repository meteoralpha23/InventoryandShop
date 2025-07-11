using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Inventory_UI : MonoBehaviour
{
<<<<<<< Updated upstream
    [Header("Inventory UI Elements")]
    [SerializeField] private Transform itemContainer;
    [SerializeField] private GameObject itemPrefab;

    [Header("Category Tabs")]
    [SerializeField] private Button weaponTabButton;
    [SerializeField] private Button materialTabButton;
    [SerializeField] private Button consumableTabButton;
    [SerializeField] private Button treasureTabButton;

    [Header("Sell Confirmation Popup")]
    [SerializeField] private GameObject sellConfirmationPopup;
    [SerializeField] private TextMeshProUGUI sellPopupText;
    [SerializeField] private Button sellYesButton;
    [SerializeField] private Button sellNoButton;

    private Inventory inventory;
    private SHOP_UI shopUI;
    private Player player;
    private Item.ItemType currentSellItem;

    private Item.ItemCategory currentCategory = Item.ItemCategory.Weapon;

    public void Init(Inventory inventory, SHOP_UI shopUI, Player player)
    {
        this.inventory = inventory;
        this.shopUI = shopUI;
        this.player = player;
        RefreshInventory(inventory.GetAllItems());
    }

    private void Start()
    {
        sellConfirmationPopup.SetActive(false);

        weaponTabButton.onClick.AddListener(() => SetCategory(Item.ItemCategory.Weapon));
        materialTabButton.onClick.AddListener(() => SetCategory(Item.ItemCategory.Material));
        consumableTabButton.onClick.AddListener(() => SetCategory(Item.ItemCategory.Consumable));
        treasureTabButton.onClick.AddListener(() => SetCategory(Item.ItemCategory.Treasure));
    }

    private void SetCategory(Item.ItemCategory category)
    {
        currentCategory = category;
        RefreshInventory(inventory.GetAllItems());
    }

    public void RefreshInventory(List<Item.ItemType> itemList)
=======
    public Transform container;
    public GameObject itemPrefab;
    public SHOP_UI shopUI;
    public Player player;

    public void Init(Inventory inv, SHOP_UI ui, Player p)
    {
        shopUI = ui; player = p;
    }

    public void RefreshInventory(List<Item_RE.ItemType> list)
>>>>>>> Stashed changes
    {
        foreach (Transform c in container) Destroy(c.gameObject);
        foreach (var t in list)
        {
<<<<<<< Updated upstream
            if (child == itemPrefab.transform) continue;
            Destroy(child.gameObject);
        }

        foreach (Item.ItemType itemType in itemList)
        {
            if (Item.GetCategory(itemType) != currentCategory) continue;

            GameObject itemGO = Instantiate(itemPrefab, itemContainer);
            itemGO.SetActive(true);

            itemGO.transform.Find("Icon").GetComponent<Image>().sprite = Item.GetSprite(itemType);
            itemGO.transform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemType.ToString());

            Button backgroundButton = itemGO.transform.Find("Background").GetComponent<Button>();
            backgroundButton.onClick.RemoveAllListeners();
            backgroundButton.onClick.AddListener(() => ShowSellConfirmation(itemType));
        }
    }

    private void ShowSellConfirmation(Item.ItemType itemType)
    {
        sellConfirmationPopup.SetActive(true);
        currentSellItem = itemType;

        int sellPrice = Mathf.FloorToInt(Item.GetCost(itemType) * 0.5f);
        sellPopupText.text = $"Do you want to sell <b>{itemType}</b> for <b>{sellPrice}</b> gold?";

        sellYesButton.onClick.RemoveAllListeners();
        sellYesButton.onClick.AddListener(() =>
=======
            var go = Instantiate(itemPrefab, container);
            go.transform.Find("Icon").GetComponent<Image>().sprite = Item_RE.GetSprite(t);
            go.transform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(t.ToString());
            go.GetComponent<Button>().onClick.AddListener(() => ShowSell(t));
        }
    }

    void ShowSell(Item_RE.ItemType t)
    {
        int price = Item_RE.GetCost(t) / 2;
        UIManager.Instance.ShowSellConfirmationPopup(t, () =>
>>>>>>> Stashed changes
        {
            player.AddGold(price);
            GetComponentInParent<Inventory>().RemoveItem(t);
            shopUI.AddItemToShop(t);
            UIManager.Instance.ShowWarning($"Sold {t} for {price}");
        });
    }
<<<<<<< Updated upstream

    private void SellItem(Item.ItemType itemType)
    {
        if (inventory == null || player == null || shopUI == null)
        {
            Debug.LogError("SellItem failed: Inventory, Player, or ShopUI is null.");
            return;
        }

        int sellPrice = Mathf.FloorToInt(Item.GetCost(itemType) * 0.5f);
        inventory.RemoveItem(itemType);
        player.AddGold(sellPrice);
        shopUI.AddItemToShop(itemType);
    }
=======
>>>>>>> Stashed changes
}
