using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public Sprite Shotgun;
    public Sprite Handgun;
    public Sprite Grenade;
    public Sprite HeavyGun;
    public Sprite Rifle;
    public Sprite Sniper;

    public Sprite Bandage;
    public Sprite Medkit;
    public Sprite GoldCoin;
    public Sprite Gem;
    public Sprite AncientRelic;
    public Sprite MetalScrap;
}
