using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;
    public Button button;

    public void Setup(Item item, System.Action onClick)
    {
        icon.sprite = item.icon;
        nameText.text = item.itemName;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());
    }
}
