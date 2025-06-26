using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SHOP_UI : MonoBehaviour
{



    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;


    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(Item_RE.ItemType.Shotgun, Item_RE.GetSprite(Item_RE.ItemType.Shotgun), "Shotgun", Item_RE.GetCost(Item_RE.ItemType.Shotgun), 0);
        Hide();
    }

    private void CreateItemButton(Item_RE.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
       
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopitemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopitemHeight * positionIndex);
        shopItemTransform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());


        shopItemTransform.Find("Icon").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyItem(itemType);

        });
    }

    private void TryBuyItem(Item_RE.ItemType itemType)
    {
        shopCustomer.BoughtItem(itemType); 
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        this.gameObject.SetActive(true);
    }

    public void  Hide()
    {
        gameObject.SetActive(false);    
    }

}
