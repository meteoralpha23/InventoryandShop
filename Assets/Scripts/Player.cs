using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IShopCustomer
{


public void BoughItem(Item_RE.ItemType itemType)
    {
       Debug.Log("Bough item" +  itemType); 
    }
}
