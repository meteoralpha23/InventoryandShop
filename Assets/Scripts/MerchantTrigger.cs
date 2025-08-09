using UnityEngine;

public class MerchantTrigger : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;

    private IShopCustomer customer;
    private bool inRange;

    private void OnTriggerEnter(Collider other)
    {
        customer = other.GetComponent<IShopCustomer>();
        if (customer != null)
        {
            inRange = true;
            UIManager.Instance.ShowWarning("Press E to open shop");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (customer != null && other.GetComponent<IShopCustomer>() == customer)
        {
            inRange = false;
            customer = null;
            shopUI.Hide();
        }
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            // Don't open shop if any popup is active
            if (UIManager.Instance != null && UIManager.Instance.IsAnyPopupActive())
            {
                return;
            }

            if (shopUI.gameObject.activeSelf)
                shopUI.Hide();
            else
                shopUI.Show(customer);
        }
    }
}
