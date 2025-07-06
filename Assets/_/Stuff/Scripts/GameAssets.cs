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
}
