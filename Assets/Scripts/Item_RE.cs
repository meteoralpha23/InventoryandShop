using UnityEngine;
using UnityEngine.UI;

public class Item_RE : MonoBehaviour
{
    public enum ItemType
    {
        Shotgun,
        Handgun,
        Grenade,
        HeavyGun,
        Rifle,
        Sniper
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Shotgun: return 500;
            case ItemType.Handgun: return 200;
            case ItemType.Grenade: return 150;
            case ItemType.HeavyGun: return 700;
            case ItemType.Rifle: return 600;
            case ItemType.Sniper: return 800;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Shotgun: return GameAssets.i.Shotgun;
            case ItemType.Handgun: return GameAssets.i.Handgun;
            case ItemType.Grenade: return GameAssets.i.Grenade;
            case ItemType.HeavyGun: return GameAssets.i.HeavyGun;
            case ItemType.Rifle: return GameAssets.i.Rifle;
            case ItemType.Sniper: return GameAssets.i.Sniper;
        }
    }
    public enum ItemCategory
    {
        Weapon,
        Material,
        Consumable,
        Treasure
    }

    public enum Rarity
    {
        VeryCommon,
        Common,
        Rare,
        Epic,
        Legendary
    }
}
