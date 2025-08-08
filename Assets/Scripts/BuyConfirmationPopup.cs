using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyConfirmationPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    public void Show(System.Action onConfirm, string customMessage = null, bool isSelling = false)
    {
        Debug.Log("BuyConfirmationPopup: Show called with message: " + (customMessage ?? "null") + ", isSelling: " + isSelling);
        
        if (promptText == null)
        {
            Debug.LogError("promptText is null! Please assign it in the inspector.");
            return;
        }
        
        promptText.text = customMessage ?? "Do you want to buy this item?";
        
        Debug.Log("Final prompt text: " + promptText.text);
        gameObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() => {
            onConfirm?.Invoke();
            gameObject.SetActive(false);
        });

        noButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }
}
