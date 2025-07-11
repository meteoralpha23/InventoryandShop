//using UnityEngine;
//using UnityEngine.UI;

<<<<<<< Updated upstream
//public class Item : MonoBehaviour
//{
//    public enum ItemType
//    {
//        Shotgun,
//        Handgun,
//        Grenade,      // Moved to Weapons
//        HeavyGun,
//        Rifle,
//        Sniper,

//        Bandage,      // New Consumable
//        Medkit,       // New Consumable

//        GoldCoin,     // New Treasure
//        Gem,          // New Treasure
//        AncientRelic, // New Treasure

//        MetalScrap ,   // New Material

//            TreasureMap,
//        Ore
//    }


//    public static int GetCost(ItemType itemType)
//    {
//        switch (itemType)
//        {
//            case ItemType.Shotgun: return 500;
//            case ItemType.Handgun: return 200;
//            case ItemType.Grenade: return 300;     
//            case ItemType.HeavyGun: return 700;
//            case ItemType.Rifle: return 600;
//            case ItemType.Sniper: return 800;

//            case ItemType.Bandage: return 50;
//            case ItemType.Medkit: return 150;

//            case ItemType.GoldCoin: return 1000;
//            case ItemType.Gem: return 2000;
//            case ItemType.AncientRelic: return 5000;

//            case ItemType.MetalScrap: return 100;

//            default: return 0;
//        }
//    }

//    public static Sprite GetSprite(ItemType itemType)
//    {
//        switch (itemType)
//        {
//            case ItemType.Shotgun: return GameAssets.i.Shotgun;
//            case ItemType.Handgun: return GameAssets.i.Handgun;
//            case ItemType.Grenade: return GameAssets.i.Grenade;
//            case ItemType.HeavyGun: return GameAssets.i.HeavyGun;
//            case ItemType.Rifle: return GameAssets.i.Rifle;
//            case ItemType.Sniper: return GameAssets.i.Sniper;

//            case ItemType.Bandage: return GameAssets.i.Bandage;
//            case ItemType.Medkit: return GameAssets.i.Medkit;

//            case ItemType.GoldCoin: return GameAssets.i.GoldCoin;
//            case ItemType.Gem: return GameAssets.i.Gem;
//            case ItemType.AncientRelic: return GameAssets.i.AncientRelic;

//            case ItemType.MetalScrap: return GameAssets.i.MetalScrap;

//            default:
//                Debug.LogWarning($"No sprite found for {itemType}, returning null.");
//                return null; 
//        }
//    }

//    public enum ItemCategory
//    {
//        Weapon,
//        Material,
//        Consumable,
//        Treasure
//    }

//    public enum Rarity
//    {
//        VeryCommon,
//        Common,
//        Rare,
//        Epic,
//        Legendary
//    }
//    public static ItemCategory GetCategory(ItemType itemType)
//    {
//        switch (itemType)
//        {
//            case ItemType.Shotgun:
//            case ItemType.Handgun:
//            case ItemType.HeavyGun:
//            case ItemType.Rifle:
//            case ItemType.Sniper:
//            case ItemType.Grenade: 
//                return ItemCategory.Weapon;

//            case ItemType.Bandage:
//            case ItemType.Medkit:
//                return ItemCategory.Consumable;

//            case ItemType.GoldCoin:
//            case ItemType.Gem:
//            case ItemType.AncientRelic:
//                return ItemCategory.Treasure;

//            case ItemType.MetalScrap:
//                return ItemCategory.Material;

//            default:
//                return ItemCategory.Material;
//        }
//    }

//    public static string GetDescription(ItemType itemType)
//    {
//        switch (itemType)
//        {
//            case ItemType.Shotgun: return "Powerful close-range weapon.";
//            case ItemType.Handgun: return "Reliable sidearm.";
//            case ItemType.Grenade: return "Explodes after short delay.";
//            case ItemType.HeavyGun: return "Deals massive damage.";
//            case ItemType.Rifle: return "Balanced medium-range firearm.";
//            case ItemType.Sniper: return "Long-range precision weapon.";
//            default: return "Unknown item.";
//        }
//    }




//}
=======
[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public string description;
    public int buyingPrice;
    public int sellingPrice;
    public float weight;
    public Rarity rarity;
    public int quantity;
    public ItemCategory category;

    public Item(
        string itemName,
        ItemCategory category,
        Sprite icon,
        string description,
        int buyingPrice,
        int sellingPrice,
        float weight,
        Rarity rarity,
        int quantity = 1)
    {
        this.itemName = itemName;
        this.category = category;
        this.icon = icon;
        this.description = description;
        this.buyingPrice = buyingPrice;
        this.sellingPrice = sellingPrice;
        this.weight = weight;
        this.rarity = rarity;
        this.quantity = quantity;
    }
}
>>>>>>> Stashed changes
