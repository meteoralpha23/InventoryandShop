public interface IShopCustomer
{
    bool BoughtItem(ItemData item);
    bool TrySpendGold(int amount);
}
