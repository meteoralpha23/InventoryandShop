using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI weightText;

    private ItemData currentItem;
    private DescriptionPanelController descriptionPanel;

    public void Setup(ItemData item, bool isOwned, System.Action onBuy, DescriptionPanelController descPanel)
    {
        currentItem = item;
        descriptionPanel = descPanel;

        iconImage.sprite = item.icon;
        nameText.text = item.itemName;
        rarityText.text = item.rarity.ToString();

        if (item.category == ItemCategory.Weapon)
        {
            bulletText.gameObject.SetActive(true);
            bulletText.text = new string('●', item.bulletCount);
        }
        else
        {
            bulletText.gameObject.SetActive(false);
        }

        if (weightText != null)
        {
            weightText.text = $"Weight: {item.weight:F1}";
        }

        if (isOwned)
        {
            priceText.text = "Owned";
            buyButton.interactable = false;
            iconImage.color = Color.gray;

            var btnColors = buyButton.colors;
            btnColors.normalColor = Color.gray;
            buyButton.colors = btnColors;
        }
        else
        {
            priceText.text = item.cost.ToString();
            iconImage.color = Color.white;

            buyButton.interactable = true;
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => onBuy?.Invoke());
        }

        // Freeze description panel on button click
        buyButton.onClick.AddListener(() => {
            if (descriptionPanel != null)
                descriptionPanel.Freeze(currentItem);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == buyButton.gameObject && descriptionPanel != null && !descriptionPanel.IsFrozen())
        {
            descriptionPanel.Show(currentItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == buyButton.gameObject && descriptionPanel != null && !descriptionPanel.IsFrozen())
        {
            descriptionPanel.Hide();
        }
    }
}
