using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private Player player;
    [SerializeField] private ShopUI shopUI; // Optional, only if you use shop features
    [SerializeField] private DescriptionPanelController descriptionPanel; // Add reference to description panel

    private bool isInventoryOpen = false;

    private void Start()
    {
        if (inventoryCanvas != null)
            inventoryCanvas.SetActive(false);

        // ? Initialize inventory UI here
        if (inventoryUI != null && player != null)
        {
            Inventory inv = player.GetInventory();
            if (inv != null)
            {
                inventoryUI.Init(inv, player, shopUI);
            }
            else
            {
                Debug.LogError("Inventory is null on Player! Make sure it's assigned.");
            }
        }
        else
        {
            Debug.LogError("InventoryUI or Player reference not assigned in InventoryManager.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // If the sell confirmation popup is active, do nothing
            if (UIManager.Instance != null && UIManager.Instance.sellConfirmationPopup != null && UIManager.Instance.sellConfirmationPopup.gameObject.activeSelf)
            {
                return;
            }

            // Always toggle inventory (open or close)
            ToggleInventory();
        }
    }

    private void LateUpdate()
    {
        if (isInventoryOpen && EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (inventoryCanvas != null)
            inventoryCanvas.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0001f;
            EventSystem.current.SetSelectedGameObject(null);
            
            // Show inventory UI
            if (inventoryUI != null)
            {
                inventoryUI.Show();
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            EventSystem.current.SetSelectedGameObject(null);
            
            // Hide inventory UI (this will also hide the description panel)
            if (inventoryUI != null)
            {
                inventoryUI.Hide();
            }
            
            // Also hide description panel directly as a backup
            if (descriptionPanel != null)
            {
                descriptionPanel.Hide();
            }
        }
    }
}
