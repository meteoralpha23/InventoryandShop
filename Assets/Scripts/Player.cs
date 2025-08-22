using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private int gold = 0; // Start with 0 gold as per requirements
    [SerializeField] private Inventory inventory;
    [SerializeField] private float maxWeight = 100f; // Maximum weight player can carry

    public int Gold => gold;
    public float MaxWeight => maxWeight;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("Inventory is NULL on Player!");
            return;
        }

        UIManager.Instance.UpdateGoldUI(gold);
        UIManager.Instance.UpdateWeightUI(inventory.GetCurrentWeight(), maxWeight);
    }

    public bool TrySpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UIManager.Instance.UpdateGoldUI(gold);
            return true;
        }

        UIManager.Instance.ShowWarning("Not enough gold!");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayNotEnoughGold();
        }
        else
        {
            Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
        }
        return false;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UIManager.Instance.UpdateGoldUI(gold);
    }

    public bool BoughtItem(ItemData item, int quantity = 1)
    {
        int totalCost = item.cost * quantity;
        float totalWeight = item.weight * quantity;
        
        // Check if player has enough gold
        if (!TrySpendGold(totalCost)) return false;
        
        // Check if adding this item would exceed weight limit
        if (inventory.GetCurrentWeight() + totalWeight > maxWeight)
        {
            UIManager.Instance.ShowWarning("Inventory too heavy!");
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayInventoryFull();
            }
            else
            {
                Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
            }
            // Refund the gold since we can't carry the item
            AddGold(totalCost);
            return false;
        }
        
        inventory.AddItem(item, quantity);
        UIManager.Instance.UpdateWeightUI(inventory.GetCurrentWeight(), maxWeight);
        
        // Play sounds
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBuySuccess();
        }
        else
        {
            Debug.Log("SoundManager.Instance is null! Cannot play sounds.");
        }
        
        return true;
    }

    public int GetGoldAmount() => gold;

    public bool HasItem(ItemData item)
    {
        return inventory.HasItem(item);
    }

    public bool HasEnoughQuantity(ItemData item, int requiredQuantity)
    {
        return inventory.GetItemQuantity(item) >= requiredQuantity;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
