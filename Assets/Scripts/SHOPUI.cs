using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform container;
    [SerializeField] private GameObject itemPrefab;

    [Header("Category Buttons")]
    [SerializeField] private Button weaponButton;
    [SerializeField] private Button consumableButton;
    [SerializeField] private Button materialButton;
    [SerializeField] private Button treasureButton;

    [Header("Description Panel")]
    [SerializeField] private DescriptionPanelController descriptionPanel;

    private List<ItemData> itemsForSale = new();
    private IShopCustomer currentCustomer;
    private ItemCategory currentCategory = ItemCategory.Weapon;
    private bool hasMadePurchase = false; // Track if player made any purchase during this shop visit

    private void Start()
    {
        weaponButton.onClick.AddListener(() => {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayTabSwitch();
            else
                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
            ShowCategory(ItemCategory.Weapon);
        });
        consumableButton.onClick.AddListener(() => {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayTabSwitch();
            else
                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
            ShowCategory(ItemCategory.Consumable);
        });
        materialButton.onClick.AddListener(() => {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayTabSwitch();
            else
                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
            ShowCategory(ItemCategory.Material);
        });
        treasureButton.onClick.AddListener(() => {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayTabSwitch();
            else
                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
            ShowCategory(ItemCategory.Treasure);
        });

        Hide();
        if (descriptionPanel != null)
            descriptionPanel.Hide();
    }

    public void Show(IShopCustomer customer)
    {
        currentCustomer = customer;
        hasMadePurchase = false; // Reset purchase flag when opening shop
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
            
        // Play appropriate sound based on whether player made a purchase
        if (SoundManager.Instance != null)
        {
            if (hasMadePurchase)
            {
                SoundManager.Instance.PlayMerchantGoodbye(); // Happy goodbye if purchase was made
            }
            else
            {
                SoundManager.Instance.PlayMerchantDisappointed(); // Disappointed if no purchase
            }
        }
        else
        {
            Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
        }
    }

    public void SetItems(List<ItemData> items)
    {
        itemsForSale = items;
        ShowCategory(currentCategory);
    }

    public void AddItemToShop(ItemData item)
    {
        if (!itemsForSale.Contains(item))
        {
            itemsForSale.Add(item);
            ShowCategory(currentCategory);
        }
    }

    private void ShowCategory(ItemCategory category)
    {
        if (UIManager.Instance != null)
            UIManager.Instance.HideAllPopups();
        currentCategory = category;
        // Don't unfreeze the description panel when refreshing - let it stay visible
        // if (descriptionPanel != null)
        //     descriptionPanel.Unfreeze();

        foreach (Transform child in container)
        {
            if (child.gameObject != itemPrefab)
                Destroy(child.gameObject);
        }

        foreach (var item in itemsForSale)
        {
            if (item.category != category) continue;
            GameObject go = Instantiate(itemPrefab, container);
            go.SetActive(true);

            ShopItemUI itemUI = go.GetComponent<ShopItemUI>();
            bool isOwned;
            
            if (currentCustomer is Player player)
            {
                // For consumables, check if player has 5 or more of the item
                if (item.category == ItemCategory.Consumable)
                {
                    isOwned = player.HasEnoughQuantity(item, 5);
                }
                else
                {
                    isOwned = player.HasItem(item);
                }
            }
            else
            {
                isOwned = false;
            }

            itemUI.Setup(item, isOwned, () =>
            {
                // Use quantity selection for consumables, regular confirmation for others
                if (item.category == ItemCategory.Consumable)
                {
                    // Check if player can buy any more of this consumable
                    if (currentCustomer is Player player)
                    {
                        int currentOwned = player.GetInventory().GetItemQuantity(item);
                        if (currentOwned >= 5)
                        {
                            // Player already has 5, can't buy more
                            UIManager.Instance.ShowWarning("You already have the maximum amount of this item!");
                            return;
                        }
                    }
                    
                    UIManager.Instance.ShowQuantitySelectionPopup(item, (quantity) =>
                    {
                        if (currentCustomer.BoughtItem(item, quantity))
                        {
                            hasMadePurchase = true; // Mark that a purchase was made
                            if (SoundManager.Instance != null)
                                SoundManager.Instance.PlayMerchantLaugh();
                            else
                                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
                            
                            // Add items to the draggable item list
                  
                            
                            ShowCategory(currentCategory);
                        }
                        else
                        {
                            if (SoundManager.Instance != null)
                                SoundManager.Instance.PlayMerchantDisappointed();
                            else
                                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
                        }
                    });
                }
                else
                {
                    UIManager.Instance.ShowBuyConfirmationPopup(item, () =>
                    {
                        if (currentCustomer.BoughtItem(item))
                        {
                            hasMadePurchase = true; // Mark that a purchase was made
                            if (SoundManager.Instance != null)
                                SoundManager.Instance.PlayMerchantLaugh();
                            else
                                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
                            
                     
                            
                            ShowCategory(currentCategory);
                        }
                        else
                        {
                            if (SoundManager.Instance != null)
                                SoundManager.Instance.PlayMerchantDisappointed();
                            else
                                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
                        }
                    });
                }
            }, descriptionPanel);
        }
    }
}
