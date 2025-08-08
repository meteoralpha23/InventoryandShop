using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuantitySelectionPopup : MonoBehaviour
{
    [Header("Quantity Controls")]
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    private ItemData currentItem;
    private int currentQuantity = 1;
    private int maxAffordableQuantity;
    private System.Action<int> onConfirm;
    private bool isSelling = false;

    private void Start()
    {
        // Set up button listeners
        if (increaseButton != null)
            increaseButton.onClick.AddListener(IncreaseQuantity);
        else
            Debug.LogError("Increase Button is not assigned in QuantitySelectionPopup!");

        if (decreaseButton != null)
            decreaseButton.onClick.AddListener(DecreaseQuantity);
        else
            Debug.LogError("Decrease Button is not assigned in QuantitySelectionPopup!");

        if (okButton != null)
            okButton.onClick.AddListener(OnOkButtonClicked);
        else
            Debug.LogError("OK Button is not assigned in QuantitySelectionPopup!");

        if (cancelButton != null)
            cancelButton.onClick.AddListener(CancelSelection);
    }

    public void Show(ItemData item, System.Action<int> onConfirmCallback, bool isSelling = false, int maxQuantity = 0)
    {
        currentItem = item;
        onConfirm = onConfirmCallback;
        currentQuantity = 1;
        this.isSelling = isSelling;

        if (isSelling)
        {
            // For selling, use the provided max quantity, but clamp to 5
            maxAffordableQuantity = Mathf.Min(maxQuantity, 5);
        }
        else
        {
            // For buying consumables, check how many the player already owns
            if (item.category == ItemCategory.Consumable)
            {
                int playerGold = Player.Instance != null ? Player.Instance.Gold : 0;
                int currentOwned = Player.Instance != null ? Player.Instance.GetInventory().GetItemQuantity(item) : 0;
                int maxCanBuy = 5 - currentOwned; // Can only buy up to 5 total
                int maxAffordableByGold = Mathf.FloorToInt(playerGold / item.cost);
                
                maxAffordableQuantity = Mathf.Min(maxCanBuy, maxAffordableByGold);
            }
            else
            {
                // For non-consumables, calculate max affordable quantity based on player's gold, but clamp to 5
                int playerGold = Player.Instance != null ? Player.Instance.Gold : 0;
                maxAffordableQuantity = Mathf.Min(Mathf.FloorToInt(playerGold / item.cost), 5);
            }
        }

        UpdateQuantityUI();
        gameObject.SetActive(true);
    }

    private void IncreaseQuantity()
    {
        if (currentQuantity < maxAffordableQuantity)
        {
            currentQuantity++;
            UpdateQuantityUI();
            
            // Play quantity change sound
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayQuantityChange();
        }
    }

    private void DecreaseQuantity()
    {
        if (currentQuantity > 1)
        {
            currentQuantity--;
            UpdateQuantityUI();
            
            // Play quantity change sound
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayQuantityChange();
        }
    }

    private void UpdateQuantityUI()
    {
        quantityText.text = currentQuantity.ToString();

        // Update button interactability
        increaseButton.interactable = currentQuantity < maxAffordableQuantity;
        decreaseButton.interactable = currentQuantity > 1;
        okButton.interactable = currentQuantity > 0 && currentQuantity <= maxAffordableQuantity;
    }

    private void OnOkButtonClicked()
    {
        // Check if UIManager exists
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager.Instance is null! Cannot show confirmation popup.");
            return;
        }

        if (isSelling)
        {
            // Use sell confirmation popup for selling
            string confirmMessage = $"Do you want to sell {currentQuantity}x {currentItem.itemName}?";
            
            UIManager.Instance.ShowSellConfirmationPopup(currentItem, () =>
            {
                onConfirm?.Invoke(currentQuantity);
                gameObject.SetActive(false);
            }, confirmMessage);
        }
        else
        {
            // Use buy confirmation popup for buying
            string confirmMessage = $"Do you want to buy {currentQuantity}x {currentItem.itemName}?";
            
            UIManager.Instance.ShowBuyConfirmationPopup(currentItem, () =>
            {
                onConfirm?.Invoke(currentQuantity);
                gameObject.SetActive(false);
            }, confirmMessage);
        }
    }

    public void CancelPurchase()
    {
        gameObject.SetActive(false);
    }

    private void CancelSelection()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayCancel();
        gameObject.SetActive(false);
    }
} 