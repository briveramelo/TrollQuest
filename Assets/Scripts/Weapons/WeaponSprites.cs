using UnityEngine;
using System.Collections.Generic;

public class WeaponSprites : MonoBehaviour {

    public static WeaponSprites Instance;
    Dictionary<Weapons, Sprite[]> myWeaponSprites;
    [SerializeField] public Sprite[] swordSprites;
    [SerializeField] public Sprite[] magicSprites;
    [SerializeField] public Sprite[] bowSprites;

    void Awake() {
        Instance = this;
        myWeaponSprites = new Dictionary<Weapons, Sprite[]>() {
            {Weapons.Sword, swordSprites },
            {Weapons.Magic, magicSprites },
            {Weapons.Bow, bowSprites }
        };
    }

    public Sprite GetWeaponSprite(WeaponStats weaponStats) {
        int levelIndex = weaponStats.level - 1;
        return (myWeaponSprites[weaponStats.weaponType])[levelIndex];
    }

}
