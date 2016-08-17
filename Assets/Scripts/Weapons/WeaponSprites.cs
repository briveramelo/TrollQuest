using UnityEngine;
using System.Collections.Generic;

public class WeaponSprites : MonoBehaviour {

    public static WeaponSprites Instance;
    Dictionary<Weapons, Sprite[]> myWeaponSprites;
    [SerializeField] public Sprite[] swordSprites;
    [SerializeField] public Sprite[] magicSprites;
    [SerializeField] public Sprite[] bowSprites;
    [SerializeField] public Sprite[] arrowSprites;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        myWeaponSprites = new Dictionary<Weapons, Sprite[]>() {
            {Weapons.Sword, swordSprites },
            {Weapons.Magic, magicSprites },
            {Weapons.Bow, bowSprites },
            {Weapons.Arrow, arrowSprites }
        };
    }

    public Sprite GetWeaponSprite(WeaponStats weaponStats) {
        int levelIndex = weaponStats.level - 1;
        return (myWeaponSprites[weaponStats.weaponType])[levelIndex];
    }

}
