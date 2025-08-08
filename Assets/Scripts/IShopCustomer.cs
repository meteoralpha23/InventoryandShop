public interface IShopCustomer
{
    bool BoughtItem(ItemData item, int quantity = 1);
    int GetGoldAmount();
    bool HasItem(ItemData item);
}
