using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer 
{
    public void BoughtItem(Item_RE.ItemType itemType)
    {
        Debug.Log("Bought item: " + itemType);
    }



}
