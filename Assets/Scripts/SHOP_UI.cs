using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SHOP_UI : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] private Transform itemListContainer;
    [SerializeField] private GameObject shopItemPrefab;

    [Header("Category Tab Buttons")]
    [SerializeField] private Button weaponsButton;
    [SerializeField] private Button materialsButton;
    [SerializeField] private Button consumablesButton;
    [SerializeField] private Button treasuresButton;

    private IShopCustomer shopCustomer;
    private Inventory inventory;
    [SerializeField] private Inventory_UI inventoryUI;

    private Dictionary<Item.ItemType, GameObject> shopItemButtons = new();
    private Item.ItemCategory currentCategory;
   [SerializeField] private TextMeshProUGUI shopDescriptionText;
=======
    public GameObject shopPanel;
    public Transform itemsGrid;
    public GameObject itemButtonPrefab;

    public Button btnWeapon, btnMaterial, btnConsumable, btnTreasure;
    public Image icon;
    public TextMeshProUGUI nameText, priceText;
    public Button actionButton;

    private IShopCustomer customer;
    private Item_RE.ItemType selectedType;
    private List<GameObject> spawnedButtons = new();
>>>>>>> Stashed changes

    void Start()
    {
<<<<<<< Updated upstream
        weaponsButton.onClick.AddListener(() => ShowCategory(Item.ItemCategory.Weapon));
        materialsButton.onClick.AddListener(() => ShowCategory(Item.ItemCategory.Material));
        consumablesButton.onClick.AddListener(() => ShowCategory(Item.ItemCategory.Consumable));
        treasuresButton.onClick.AddListener(() => ShowCategory(Item.ItemCategory.Treasure));

       

        currentCategory = Item.ItemCategory.Weapon;
        ShowCategory(currentCategory);
        Hide();
    }

    private void ShowCategory(Item.ItemCategory category)
    {
        currentCategory = category;

        UIManager.Instance.HideBuyConfirmationPopup();
        foreach (Transform child in itemListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item.ItemType itemType in System.Enum.GetValues(typeof(Item.ItemType)))
        {
            if (Item.GetCategory(itemType) != category) continue;
            if (inventory != null && inventory.HasItem(itemType)) continue;

            CreateItemButton(itemType);
        }
    }

    private void CreateItemButton(Item.ItemType itemType)
    {
        GameObject shopItemGO = Instantiate(shopItemPrefab, itemListContainer);

        shopItemGO.transform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemType.ToString());
        shopItemGO.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(Item.GetCost(itemType).ToString());
        shopItemGO.transform.Find("Icon").GetComponent<Image>().sprite = Item.GetSprite(itemType);
        if (GameObject.Find("ShopDescriptionText") == null)
        {
            Debug.Log("Not found");
        }
        else
        {
            GameObject.Find("ShopDescriptionText")?.GetComponent<TextMeshProUGUI>()?.SetText(Item.GetDescription(itemType));

        }
        shopItemGO.transform.Find("Background").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (shopCustomer is Player player)
            {
                int playerGold = player.GetGoldAmount();
                int cost = Item.GetCost(itemType);

                if (playerGold < cost)
                {
                    UIManager.Instance.ShowWarning("Not enough gold!");
                    return;
                }

                UIManager.Instance.ShowBuyConfirmationPopup(itemType, () => TryBuyItem(itemType));
            }
        });

        shopItemButtons[itemType] = shopItemGO;
    }

    private void TryBuyItem(Item.ItemType itemType)
    {
        if (inventory == null || inventory.HasItem(itemType)) return;

        shopCustomer.BoughtItem(itemType);
        inventory.AddItem(itemType);

        // Refresh category
        ShowCategory(currentCategory);
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

        ShowCategory(currentCategory);

        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
=======
        btnWeapon.onClick.AddListener(() => ShowCategory(Item_RE.ItemCategory.Weapon));
        btnMaterial.onClick.AddListener(() => ShowCategory(Item_RE.ItemCategory.Material));
        btnConsumable.onClick.AddListener(() => ShowCategory(Item_RE.ItemCategory.Consumable));
        btnTreasure.onClick.AddListener(() => ShowCategory(Item_RE.ItemCategory.Treasure));

        shopPanel.SetActive(false);
    }

    public void Show(IShopCustomer cust)
    {
        customer = cust;
        shopPanel.SetActive(true);
        ShowCategory(Item_RE.ItemCategory.Weapon);
>>>>>>> Stashed changes
    }

    public void Hide()
    {
        shopPanel.SetActive(false);
        customer = null;
        ClearGrid();
    }

    void ShowCategory(Item_RE.ItemCategory cat)
    {
        ClearGrid();
        foreach (Item_RE.ItemType type in System.Enum.GetValues(typeof(Item_RE.ItemType)))
            if (/* assign categories manually */ true)  // You'll need to map category here
                CreateButton(type);
    }

    void CreateButton(Item_RE.ItemType type)
    {
        var GO = Instantiate(itemButtonPrefab, itemsGrid);
        spawnedButtons.Add(GO);
        GO.transform.Find("Icon").GetComponent<Image>().sprite = Item_RE.GetSprite(type);
        GO.transform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(type.ToString());
        GO.GetComponent<Button>().onClick.AddListener(() => OnSelect(type));
    }

    void OnSelect(Item_RE.ItemType type)
    {
        selectedType = type;
        icon.sprite = Item_RE.GetSprite(type);
        nameText.SetText(type.ToString());
        priceText.SetText($"Price: {Item_RE.GetCost(type)}");
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() =>
            UIManager.Instance.ShowBuyConfirmationPopup(type, TryBuy));
    }

    void TryBuy()
    {
        if (customer.TrySpendGoldAmount(Item_RE.GetCost(selectedType)))
        {
            customer.BoughtItem(selectedType);
            UIManager.Instance.ShowWarning($"Bought {selectedType}");
            ShowCategory(/* current */ Item_RE.ItemCategory.Weapon);
        }
        else
            UIManager.Instance.ShowWarning("Not enough gold!");
    }

    void ClearGrid()
    {
        foreach (var go in spawnedButtons) Destroy(go);
        spawnedButtons.Clear();
    }

    public void AddItemToShop(Item.ItemType itemType)
    {
<<<<<<< Updated upstream
        if (inventory != null && inventory.HasItem(itemType)) return;
        if (Item.GetCategory(itemType) != currentCategory) return;

        if (!shopItemButtons.ContainsKey(itemType))
        {
            CreateItemButton(itemType);
        }
=======
        if (shopPanel.activeSelf)
            ShowCategory(/* current */ Item_RE.ItemCategory.Weapon);
>>>>>>> Stashed changes
    }
}
