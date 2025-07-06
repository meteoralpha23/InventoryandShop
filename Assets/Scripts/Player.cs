using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
    [SerializeField] private int goldAmount = 100;
    [SerializeField] private SHOP_UI shopUI;
    private void Start()
    {
        Inventory inventory = GetComponent<Inventory>();
        Inventory_UI inventoryUI = inventory.GetComponent<Inventory_UI>();
    

        if (inventoryUI != null && shopUI != null)
        {
            inventoryUI = inventory.GetComponentInChildren<Inventory_UI>();
            inventoryUI.Init(inventory,   shopUI, this);
        }
    }

    public bool BoughtItem(Item_RE.ItemType itemType)
    {
        int cost = Item_RE.GetCost(itemType);

        if (!TrySpendGoldAmount(cost))
        {
            UIManager.Instance.ShowWarning("Not enough gold!");
            Debug.Log("Not enough gold for: " + itemType);
            return false;
        }

        Debug.Log("Bought item: " + itemType);

        GetComponent<Inventory>().AddItem(itemType); 
        UIManager.Instance.UpdateGoldUI(goldAmount);
        return true;
    }


    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        if (goldAmount >= spendGoldAmount)
        {
            goldAmount -= spendGoldAmount;
            return true;
        }
        return false;
    }
    public void AddGold(int amount)
    {
        goldAmount += amount;
        UIManager.Instance.UpdateGoldUI(goldAmount);
    }

    }
    
