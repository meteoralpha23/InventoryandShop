using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_RE : MonoBehaviour
{    public enum ItemType
    {
        Shotgun


    }

        public static int GetCost(ItemType itemType) {
            switch (itemType) {
           default:
            case ItemType.Shotgun:        return 0;
  
            }
        }

        public static Sprite GetSprite(ItemType itemType) {
            switch (itemType) {
            default:
            case ItemType.Shotgun:    return GameAssets.i.Shotgun;

            }
        }
}
