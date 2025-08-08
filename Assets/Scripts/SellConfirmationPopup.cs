using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellConfirmationPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private TextMeshProUGUI yesButtonText;
    [SerializeField] private TextMeshProUGUI noButtonText;

    public void Show(System.Action onConfirm, string customMessage = null)
    {

        if (promptText != null)
        {
            promptText.text = customMessage ?? "Do you want to sell this item?";
        }
        else
        {
            Debug.LogError("promptText is null! Please assign it in the inspector.");
        }

        // Set button texts
        if (yesButtonText != null)
        {
            yesButtonText.text = "Sell";
        }
        
        if (noButtonText != null)
        {
            noButtonText.text = "Cancel";
        }

        gameObject.SetActive(true);

       
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() => {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayConfirmation();
            onConfirm?.Invoke();
            gameObject.SetActive(false);
        });

        noButton.onClick.AddListener(() => {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayCancel();
            gameObject.SetActive(false);
        });
    }
} 
