public interface IShopCustomer
{
<<<<<<< Updated upstream


    bool BoughtItem(Item.ItemType itemType);
    bool TrySpendGoldAmount(int goldAmount);



=======
    bool TrySpendGoldAmount(int amount);
    int GetGoldAmount();
    void AddGold(int amount);
    void BoughtItem(Item_RE.ItemType itemType);
>>>>>>> Stashed changes
}
