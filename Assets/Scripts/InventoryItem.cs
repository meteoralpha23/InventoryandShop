using UnityEngine;

[System.Serializable]
public class InventoryItem : MonoBehaviour
{
    public ItemData data;
    public int quantity;

   
    public void Initialize(ItemData itemData, int itemQuantity)
    {
        this.data = itemData;
        this.quantity = itemQuantity;
    }

    public float GetTotalWeight()
    {
        if (data == null) return 0f;
        return data.weight * quantity;
    }
}
