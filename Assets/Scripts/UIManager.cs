using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] public BuyConfirmationPopup buyConfirmationPopup;
    [SerializeField] private QuantitySelectionPopup quantitySelectionPopup;
    [SerializeField] public SellConfirmationPopup sellConfirmationPopup;
    
    // Specific gold text components for inventory and shop
    [Header("Gold Display")]
    [SerializeField] private TextMeshProUGUI inventoryGoldText;
    [SerializeField] private TextMeshProUGUI shopGoldText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        warningPanel.SetActive(false);
    }

    public void UpdateGoldUI(int gold)
    {
        // Update main gold text (if exists)
        if (goldText != null)
        {
            goldText.text = gold.ToString();
        }
        
        // Update inventory gold text
        if (inventoryGoldText != null)
        {
            inventoryGoldText.text = gold.ToString();
        }
        
        // Update shop gold text
        if (shopGoldText != null)
        {
            shopGoldText.text = gold.ToString();
        }
    }

    public void UpdateWeightUI(float currentWeight, float maxWeight)
    {
        if (weightText != null)
        {
            weightText.text = $"{currentWeight:F1}/{maxWeight:F1}";
            
            // Change color based on weight (red when near max)
            if (currentWeight >= maxWeight * 0.9f)
            {
                weightText.color = Color.red;
            }
            else if (currentWeight >= maxWeight * 0.7f)
            {
                weightText.color = Color.yellow;
            }
            else
            {
                weightText.color = Color.white;
            }
        }
    }

    public void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
        }
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
            CancelInvoke(nameof(HideWarning));
            Invoke(nameof(HideWarning), 2f);
        }
        
        // Play warning sound for certain messages
        if (SoundManager.Instance != null)
        {
            if (message.Contains("heavy") || message.Contains("weight"))
            {
                SoundManager.Instance.PlayWeightWarning();
            }
            else if (message.Contains("gold") || message.Contains("money"))
            {
                SoundManager.Instance.PlayNotEnoughGold();
            }
            else
            {
                SoundManager.Instance.PlayBuyFail();
            }
        }
    }

    private void HideWarning()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }



    public void ShowBuyConfirmationPopup(ItemData item, System.Action onConfirm, string customMessage = null, bool isSelling = false)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayPopupOpen();
        buyConfirmationPopup.Show(() => {
            onConfirm?.Invoke();
        }, customMessage, isSelling);
    }

    public void ShowQuantitySelectionPopup(ItemData item, System.Action<int> onConfirm, bool isSelling = false, int maxQuantity = 0)
    {
        if (quantitySelectionPopup == null)
        {
            Debug.LogError("QuantitySelectionPopup is not assigned in UIManager! Please assign it in the inspector.");
            return;
        }
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayPopupOpen();
        quantitySelectionPopup.Show(item, onConfirm, isSelling, maxQuantity);
    }

    public void ShowSellConfirmationPopup(ItemData item, System.Action onConfirm, string customMessage = null)
    {
        if (sellConfirmationPopup == null)
        {
            Debug.LogWarning("SellConfirmationPopup is not assigned in UIManager! Falling back to BuyConfirmationPopup.");
            // Fallback to buy confirmation popup with sell text
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayPopupOpen();
            buyConfirmationPopup.Show(onConfirm, customMessage, true); // isSelling = true
            return;
        }
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayPopupOpen();
        sellConfirmationPopup.Show(onConfirm, customMessage);
    }



    public void HideAllPopups()
    {
        if (buyConfirmationPopup != null)
            buyConfirmationPopup.gameObject.SetActive(false);
        if (quantitySelectionPopup != null)
            quantitySelectionPopup.gameObject.SetActive(false);
        if (sellConfirmationPopup != null)
            sellConfirmationPopup.gameObject.SetActive(false);
        // Add any other popups you want to hide
    }



 
    public bool IsAnyPopupActive()
    {
        bool buyPopupActive = buyConfirmationPopup != null && buyConfirmationPopup.gameObject.activeSelf;
        bool sellPopupActive = sellConfirmationPopup != null && sellConfirmationPopup.gameObject.activeSelf;
        bool quantityPopupActive = quantitySelectionPopup != null && quantitySelectionPopup.gameObject.activeSelf;
        bool warningActive = warningPanel != null && warningPanel.activeSelf;

        return buyPopupActive || sellPopupActive || quantityPopupActive || warningActive;
    }
}
