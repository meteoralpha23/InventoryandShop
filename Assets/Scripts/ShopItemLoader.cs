using UnityEngine;
using System.Collections.Generic;

public class ShopItemLoader : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private List<ItemData> itemsToSell;

    private void Start()
    {
        shopUI.SetItems(itemsToSell);
    }
}
