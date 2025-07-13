using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
    [SerializeField] private int gold = 100;
    public int Gold => gold;

    public bool TrySpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UIManager.Instance.UpdateGoldUI(gold);
            return true;
        }

        UIManager.Instance.ShowWarning("Not enough gold!");
        return false;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UIManager.Instance.UpdateGoldUI(gold);
    }

    public bool BoughtItem(ItemData item)
    {
        if (!TrySpendGold(item.cost)) return false;

        GetComponent<Inventory>().AddItem(item);
        return true;
    }
}
