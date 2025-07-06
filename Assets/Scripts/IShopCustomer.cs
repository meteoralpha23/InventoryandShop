using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer 
{


    bool BoughtItem(Item_RE.ItemType itemType);
    bool TrySpendGoldAmount(int goldAmount);



}
