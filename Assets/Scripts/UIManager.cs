// UIManager.cs
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningText;

    [Header("Bottom Prompt")]
    [SerializeField] private GameObject bottomPromptPanel;
    [SerializeField] private TextMeshProUGUI bottomPromptText;

    private void Awake()
    {
        Instance = this;
        warningPanel.SetActive(false);
        if (bottomPromptPanel != null) bottomPromptPanel.SetActive(false);
    }

    public void UpdateGoldUI(int gold)
    {
        goldText.text = $"Gold: {gold}";
    }

    public void ShowWarning(string message)
    {
        warningText.text = message;
        warningPanel.SetActive(true);
        CancelInvoke(nameof(HideWarning));
        Invoke(nameof(HideWarning), 2f);
    }

    private void HideWarning()
    {
        warningPanel.SetActive(false);
    }

    public void ShowBottomPrompt(string message)
    {
        if (bottomPromptPanel != null && bottomPromptText != null)
        {
            bottomPromptText.text = message;
            bottomPromptPanel.SetActive(true);
        }
    }

    public void HideBottomPrompt()
    {
        if (bottomPromptPanel != null)
            bottomPromptPanel.SetActive(false);
    }
}
