using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    [SerializeField] private SHOP_UI uiShop;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In");
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if(shopCustomer != null )
        {
            uiShop.Show(shopCustomer);
        }
    }

    private void OnTriggerExit(Collider other)
    {


        Debug.Log("out");
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            uiShop.Hide();
        }

    }
}
