using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject sellPopup;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private Inventory inventory;
    private Player player;
    private SHOPUI shopUI;
    private ItemData currentItem;

    public void Init(Inventory inv, Player p, SHOPUI shop)
    {
        inventory = inv;
        player = p;
        shopUI = shop;

        inventory.OnInventoryChanged += RefreshUI;
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (Transform child in container)
            if (child != itemPrefab.transform) Destroy(child.gameObject);

        foreach (var item in inventory.GetAllItems())
        {
            var go = Instantiate(itemPrefab, container);
            var itemUI = go.GetComponent<InventoryItemUI>();
            itemUI.Setup(item, false, 0, "", () => ShowSellPopup(item));
            go.SetActive(true);
        }
    }

    private void ShowSellPopup(ItemData item)
    {
        currentItem = item;
        int sellPrice = Mathf.FloorToInt(item.cost * 0.5f);
        sellText.text = $"Sell {item.itemName} for {sellPrice} gold?";
        sellPopup.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() =>
        {
            inventory.RemoveItem(item);
            player.AddGold(sellPrice);
            shopUI.AddItemToShop(item);
            sellPopup.SetActive(false);
        });

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(() => sellPopup.SetActive(false));
    }
}
