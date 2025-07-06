using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Warning Popup")]
    [SerializeField] private GameObject warningPopup;
    [SerializeField] private TextMeshProUGUI warningText;

    [Header("Buy Confirmation Popup")]
    [SerializeField] private GameObject buyConfirmPopup;
    [SerializeField] private TextMeshProUGUI buyConfirmPopupText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [Header("Bottom Prompt")]
    [SerializeField] private TextMeshProUGUI bottomPromptText;

    private Coroutine warningCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (warningPopup != null)
            warningPopup.SetActive(false);

        if (buyConfirmPopup != null)
            buyConfirmPopup.SetActive(false);

        if (bottomPromptText != null)
            bottomPromptText.gameObject.SetActive(false);
    }

    public void UpdateGoldUI(int newGoldAmount)
    {
        goldText.SetText("Gold: " + newGoldAmount);
    }

    public void ShowWarning(string message, float duration = 3f)
    {
        if (warningCoroutine != null)
            StopCoroutine(warningCoroutine);

        warningCoroutine = StartCoroutine(ShowWarningCoroutine(message, duration));
    }

    private IEnumerator ShowWarningCoroutine(string message, float duration)
    {
        warningText.text = message;
        warningPopup.SetActive(true);
        yield return new WaitForSeconds(duration);
        warningPopup.SetActive(false);
    }

    public void ShowWarningPopup(string message)
    {
        ShowWarning(message, 1f);
    }

    public void ShowBuyConfirmationPopup(Item_RE.ItemType itemType, System.Action onConfirm)
    {
        buyConfirmPopup.SetActive(true);
        buyConfirmPopupText.SetText($"Buy {itemType} for {Item_RE.GetCost(itemType)} gold?");

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() =>
        {
            buyConfirmPopup.SetActive(false);
            onConfirm?.Invoke();
        });

        noButton.onClick.AddListener(() =>
        {
            buyConfirmPopup.SetActive(false);
        });
    }

    public void ShowBottomPrompt(string message)
    {
        if (bottomPromptText != null)
        {
            bottomPromptText.text = message;
            bottomPromptText.gameObject.SetActive(true);
        }
    }

    public void HideBottomPrompt()
    {
        if (bottomPromptText != null)
            bottomPromptText.gameObject.SetActive(false);
    }
}
