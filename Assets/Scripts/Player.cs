using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
    [SerializeField] private int goldAmount = 100;

    public bool TrySpendGoldAmount(int amount)
    {
        if (goldAmount >= amount)
        {
<<<<<<< Updated upstream
            inventoryUI = inventory.GetComponentInChildren<Inventory_UI>();
            inventoryUI.Init(inventory,   shopUI, this);
        }
    }

    public bool BoughtItem(Item.ItemType itemType)
    {
        int cost = Item.GetCost(itemType);

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
=======
            goldAmount -= amount;
            UIManager.Instance.UpdateGoldUI(goldAmount);
>>>>>>> Stashed changes
            return true;
        }
        return false;
    }

    public int GetGoldAmount() => goldAmount;

    public void AddGold(int amount)
    {
        goldAmount += amount;
        UIManager.Instance.UpdateGoldUI(goldAmount);
    }

    public void BoughtItem(Item_RE.ItemType itemType)
    {
        GetComponent<Inventory>().AddItem(itemType);
        UIManager.Instance.ShowWarning($"Bought {itemType}");
    }
}
