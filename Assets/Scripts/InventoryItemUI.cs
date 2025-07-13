using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button actionButton;

    public void Setup(ItemData item, bool showPrice, int price, string buttonText, System.Action onClick)
    {
        iconImage.sprite = item.icon;
        nameText.text = item.itemName;

        if (priceText != null)
        {
            priceText.gameObject.SetActive(showPrice);
            if (showPrice)
                priceText.text = price.ToString();
        }

        if (actionButton != null)
        {
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(() => onClick?.Invoke());

            if (!string.IsNullOrEmpty(buttonText))
            {
                var btnText = actionButton.GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null)
                    btnText.text = buttonText;
            }
        }
    }
}
