using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionPanelController : MonoBehaviour
{
    [SerializeField] private GameObject panelRoot; 
    [SerializeField] private Image descriptionImage;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI ammoCapacityText;
    [SerializeField] private TextMeshProUGUI reloadSpeedText;
    [SerializeField] private TextMeshProUGUI rateOfFireText;
    [SerializeField] private TextMeshProUGUI precisionText;
    [SerializeField] private GameObject freezePanel; 

    private bool isFrozen = false;

    public void Show(ItemData item)
    {
        if (item == null || item.descriptionImage == null)
        {
            Hide();
            return;
        }
        panelRoot.SetActive(true);
        if (freezePanel != null && isFrozen)
            freezePanel.SetActive(true);
        else if (freezePanel != null)
            freezePanel.SetActive(false);

        descriptionImage.sprite = item.descriptionImage;


        bool isWeapon = item.category == ItemCategory.Weapon;
        if (powerText != null)
        {
            powerText.gameObject.SetActive(isWeapon);
            if (isWeapon) powerText.text = item.power.ToString("0.00");
        }

        if (ammoCapacityText != null)
        {
            ammoCapacityText.gameObject.SetActive(isWeapon);
            if (isWeapon) ammoCapacityText.text = item.ammoCapacity.ToString();
        }

        if (reloadSpeedText != null)
        {
            reloadSpeedText.gameObject.SetActive(isWeapon);
            if (isWeapon) reloadSpeedText.text = item.reloadSpeed.ToString("0.00");
        }

        if (rateOfFireText != null)
        {
            rateOfFireText.gameObject.SetActive(isWeapon);
            if (isWeapon) rateOfFireText.text = item.rateOfFire.ToString("0.00");
        }

        if (precisionText != null)
        {
            precisionText.gameObject.SetActive(isWeapon);
            if (isWeapon) precisionText.text = item.precision.ToString("0.00");
        }

    }


    public void Hide()
    {
        panelRoot.SetActive(false);
        if (freezePanel != null)
            freezePanel.SetActive(false);
        isFrozen = false;
    }

    public void Freeze(ItemData item)
    {
        isFrozen = true;
        Show(item);
    }

    public void Unfreeze()
    {
        isFrozen = false;
        Hide();
    }

    public bool IsFrozen() => isFrozen;
} 