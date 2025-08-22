using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform container;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TextMeshProUGUI weightText;

    [Header("Category Dropdown")]
    [SerializeField] private TMP_Dropdown categoryDropdown;

    [Header("Description Panel")]
    [SerializeField] private DescriptionPanelController descriptionPanel;

    private Inventory inventory;
    private Player player;
    private ShopUI shopUI;
    private ItemData currentItem;
    private ItemCategory currentCategory = ItemCategory.Weapon;

    private void Start()
    {
        // Set up dropdown
        SetupCategoryDropdown();
        
        Hide();
        if (descriptionPanel != null)
            descriptionPanel.Hide();
    }

    private void SetupCategoryDropdown()
    {
        if (categoryDropdown != null)
        {
            // Clear existing options
            categoryDropdown.ClearOptions();
            
            // Add category options
            List<string> options = new List<string>
            {
                "Weapons",
                "Consumables", 
                "Materials",
                "Treasures"
            };
            
            categoryDropdown.AddOptions(options);
            
            // Set default value
            categoryDropdown.value = 0;
            
            // Add listener
            categoryDropdown.onValueChanged.AddListener(OnCategoryChanged);
        }
    }

    private void OnCategoryChanged(int index)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayTabSwitch();
        else
            Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
            
        // Convert dropdown index to ItemCategory
        ItemCategory selectedCategory = (ItemCategory)index;
        ShowCategory(selectedCategory);
    }
    


    public void Init(Inventory inv, Player p, ShopUI shop)
    {
        inventory = inv;
        player = p;
        shopUI = shop;

        inventory.OnInventoryChanged += RefreshUI;
        RefreshUI();
        if (descriptionPanel != null)
            descriptionPanel.Hide();
    }

    public void Show()
    {
        ShowCategory(currentCategory);
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (descriptionPanel != null)
            descriptionPanel.Unfreeze();
    }

    public void SetCategory(ItemCategory category)
    {
        currentCategory = category;
        // Update dropdown value to match the new category
        if (categoryDropdown != null)
        {
            categoryDropdown.value = (int)category;
        }
        RefreshUI();
    }

    private void ShowCategory(ItemCategory category)
    {
        if (UIManager.Instance != null)
            UIManager.Instance.HideAllPopups();
        currentCategory = category;
        
        // Update dropdown value to match the selected category
        if (categoryDropdown != null)
        {
            categoryDropdown.value = (int)category;
        }
        
        // Hide description panel when switching categories
        if (descriptionPanel != null)
            descriptionPanel.Hide();

        RefreshUI();
    }

    private void RefreshUI()
    {
        // Update weight display
        UpdateWeightDisplay();

        foreach (Transform child in container)
        {
            if (child.gameObject != itemPrefab)
                Destroy(child.gameObject);
        }

        List<InventoryItem> items = inventory.GetItemsByCategory(currentCategory);

        foreach (var item in items)
        {
            GameObject go = Instantiate(itemPrefab, container);
            go.SetActive(true);

            InventoryItemUI itemUI = go.GetComponent<InventoryItemUI>();
            if (itemUI != null)
            {
                int sellPrice = Mathf.FloorToInt(item.data.cost * 0.5f); 

                itemUI.Setup(
                    item,
                    showPrice: true,
                    price: sellPrice,
                    buttonText: "Sell",
                    onClick: () => ShowSellPopup(item),
                    descriptionPanel
                );
            }
        }
    }

    private void UpdateWeightDisplay()
    {
        if (weightText != null && inventory != null && player != null)
        {
            float currentWeight = inventory.GetCurrentWeight();
            float maxWeight = player.MaxWeight;
            
            weightText.text = $"{currentWeight:F1}/{maxWeight:F1}";
            
            // Change color based on weight (red when near max)
            if (currentWeight >= maxWeight * 0.9f)
            {
                weightText.color = Color.red;
            }
            else if (currentWeight >= maxWeight * 0.7f)
            {
                weightText.color = Color.yellow;
            }
            else
            {
                weightText.color = Color.white;
            }
        }
    }

    private void ShowSellPopup(InventoryItem item)
    {
        currentItem = item.data;
        int sellPrice = Mathf.FloorToInt(item.data.cost * 0.5f);
        
        // Use quantity selection for consumables with multiple quantities
        if (item.data.category == ItemCategory.Consumable && item.quantity > 1)
        {
            UIManager.Instance.ShowQuantitySelectionPopup(item.data, (quantity) =>
            {
                if (quantity <= item.quantity)
                {
                    int totalSellPrice = sellPrice * quantity;
                    inventory.RemoveItem(item.data, quantity);
                    player.AddGold(totalSellPrice);
                    shopUI.AddItemToShop(item.data);
                    
                    // Update weight display
                    UIManager.Instance.UpdateWeightUI(inventory.GetCurrentWeight(), player.MaxWeight);
                    
                    // Play sounds
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlaySellSuccess();
                    }
                }
            }, true, item.quantity);
        }
        else
        {
            // Use the sell confirmation popup with custom sell message
            string sellMessage = $"Do you want to sell this item?";
            
            UIManager.Instance.ShowSellConfirmationPopup(item.data, () =>
            {
                inventory.RemoveItem(item.data);
                player.AddGold(sellPrice);
                shopUI.AddItemToShop(item.data);
                
                // Update weight display
                UIManager.Instance.UpdateWeightUI(inventory.GetCurrentWeight(), player.MaxWeight);
                
                // Play sounds
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlaySellSuccess();
                }
            }, sellMessage);
        }
    }
}
