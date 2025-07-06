using UnityEngine;

public class MerchantController : MonoBehaviour
{
    [SerializeField] private SHOP_UI uiShop;
    private bool isPlayerInRange = false;
    private IShopCustomer currentCustomer;
    private bool isShopOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        currentCustomer = other.GetComponent<IShopCustomer>();
        if (currentCustomer != null)
        {
            isPlayerInRange = true;
            if (!isShopOpen)
                UIManager.Instance.ShowBottomPrompt("Press E to open shop");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null && shopCustomer == currentCustomer)
        {
            isPlayerInRange = false;
            currentCustomer = null;
            isShopOpen = false;
            UIManager.Instance.HideBottomPrompt();
            uiShop.Hide(); // Close shop if player leaves
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (currentCustomer != null)
            {
                if (!isShopOpen)
                {
                    uiShop.Show(currentCustomer);
                    UIManager.Instance.HideBottomPrompt();
                    isShopOpen = true;
                }
                else
                {
                    uiShop.Hide();
                    UIManager.Instance.ShowBottomPrompt("Press E to open shop");
                    isShopOpen = false;
                }
            }
        }
    }
}
