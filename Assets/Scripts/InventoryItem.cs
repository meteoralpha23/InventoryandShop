[System.Serializable]
public class InventoryItem
{
    public ItemData data;
    public int quantity;

    public InventoryItem(ItemData data, int quantity)
    {
        this.data = data;
        this.quantity = quantity;
    }

    public float GetTotalWeight()
    {
        return data.weight * quantity;
    }
}
