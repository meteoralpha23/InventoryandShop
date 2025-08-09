using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI weightText;

    private InventoryItem currentItem;
    private DescriptionPanelController descriptionPanel;

    public void Setup(InventoryItem item, bool showPrice, int price, string buttonText, System.Action onClick, DescriptionPanelController descPanel = null)
    {
        currentItem = item;
        descriptionPanel = descPanel;

        if (item == null) Debug.LogError("Item is null!");
        if (item.data == null) Debug.LogError("Item data is null!");
        if (iconImage == null) Debug.LogError("Icon is not assigned!");
        if (nameText == null) Debug.LogError("NameText is not assigned!");
        if (actionButton == null) Debug.LogError("ActionButton is not assigned!");
        iconImage.sprite = item.data.icon;
        nameText.text = item.data.itemName;
        quantityText.text = "x" + item.quantity;

        // Display weight
        if (weightText != null)
        {
            weightText.text = $"{item.data.weight:F1}";
        }

        var buttonTextComp = actionButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonTextComp != null)
        {
            buttonTextComp.text = buttonText;
        }
        else
        {
            Debug.LogWarning("No TextMeshProUGUI found in button's children!");
        }
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => onClick?.Invoke());
        
        // Freeze description panel on button click
        actionButton.onClick.AddListener(() => {
            if (descriptionPanel != null && currentItem != null)
                descriptionPanel.Freeze(currentItem.data);
        });
        


        if (quantityText != null)
        {
            if (item.data.category == ItemCategory.Consumable || item.data.category == ItemCategory.Material)
            {
                quantityText.gameObject.SetActive(true);
                quantityText.text = $"x{item.quantity}";
            }
            else
            {
                quantityText.gameObject.SetActive(false);
            }
        }
        Debug.Log($"[InventoryItemUI] Setting price: {price} for {item.data.itemName}");
        if (showPrice && priceText != null)
        {
            priceText.gameObject.SetActive(true);
            priceText.text = price.ToString();
        }
        else if (priceText != null)
        {
            priceText.gameObject.SetActive(false);
        }
        rarityText.text = item.data.rarity.ToString();

       
        switch (item.data.rarity)
        {
            case Rarity.VeryCommon: rarityText.color = Color.gray; break;
            case Rarity.Common: rarityText.color = Color.white; break;
            case Rarity.Rare: rarityText.color = Color.blue; break;
            case Rarity.Epic: rarityText.color = new Color(0.6f, 0f, 1f); break;
            case Rarity.Legendary: rarityText.color = Color.yellow; break;
        }

        if (item.data.category == ItemCategory.Weapon)
        {
            bulletText.gameObject.SetActive(true);
            bulletText.text = new string('●', item.data.bulletCount);
        }
        else
        {
            bulletText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == actionButton.gameObject && descriptionPanel != null && !descriptionPanel.IsFrozen() && currentItem != null)
        {
            descriptionPanel.Show(currentItem.data);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == actionButton.gameObject && descriptionPanel != null && !descriptionPanel.IsFrozen())
        {
            descriptionPanel.Hide();
        }
    }
}

