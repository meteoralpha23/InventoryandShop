using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Inventory_UI : MonoBehaviour
{
    [SerializeField] private Transform itemContainer;
    [SerializeField] private GameObject itemPrefab;

    private Inventory inventory;
    private SHOP_UI shopUI;
    private Player player;

    [SerializeField] private GameObject sellConfirmationPopup;
    [SerializeField] private TextMeshProUGUI sellPopupText;
    [SerializeField] private Button sellYesButton;
    [SerializeField] private Button sellNoButton;

    private Item_RE.ItemType currentSellItem;
    public void Init(Inventory inventory, SHOP_UI shopUI, Player player)
    {
        this.inventory = inventory;
        this.shopUI = shopUI;
        this.player = player;
    }
    private void Start()
    {
        sellConfirmationPopup.SetActive(false);
    }



    public void RefreshInventory(List<Item_RE.ItemType> itemList)
    {
        foreach (Transform child in itemContainer)
        {
            if (child == itemPrefab.transform) continue;
            Destroy(child.gameObject);
        }

        foreach (Item_RE.ItemType itemType in itemList)
        {
            GameObject itemGO = Instantiate(itemPrefab, itemContainer);
            itemGO.SetActive(true);

            itemGO.transform.Find("Icon").GetComponent<Image>().sprite = Item_RE.GetSprite(itemType);
            itemGO.transform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemType.ToString());

            Button backgroundButton = itemGO.transform.Find("Background").GetComponent<Button>();
            backgroundButton.onClick.RemoveAllListeners(); // Optional but good for safety
            backgroundButton.onClick.AddListener(() => ShowSellConfirmation(itemType));


        }
    }
    private void ShowSellConfirmation(Item_RE.ItemType itemType)
    {
        sellConfirmationPopup.SetActive(true);
        currentSellItem = itemType;
        int sellPrice = Mathf.FloorToInt(Item_RE.GetCost(itemType) * 0.5f); // Or your chosen rate

        sellPopupText.text = $"Do you want to sell <b>{itemType}</b> for <b>{sellPrice}</b> gold?";
        sellConfirmationPopup.SetActive(true);

        sellYesButton.onClick.RemoveAllListeners();
        sellYesButton.onClick.AddListener(() =>
        {
            SellItem(currentSellItem);
            sellConfirmationPopup.SetActive(false);
        });

        sellNoButton.onClick.RemoveAllListeners();
        sellNoButton.onClick.AddListener(() =>
        {
            sellConfirmationPopup.SetActive(false);
        });
    }

    private void SellItem(Item_RE.ItemType itemType)
    {
        if (inventory == null || player == null || shopUI == null)
        {
            Debug.LogError("SellItem failed: Inventory, Player, or ShopUI is null.");
            return;
        }

        int sellPrice = Mathf.FloorToInt(Item_RE.GetCost(itemType) * 0.5f);
        inventory.RemoveItem(itemType);
        player.AddGold(sellPrice);
        shopUI.AddItemToShop(itemType);
    }

}
