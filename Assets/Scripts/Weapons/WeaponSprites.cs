using UnityEngine;
using System.Collections;

public class WeaponSprites : MonoBehaviour {

    public static WeaponSprites Instance;

    [SerializeField] public Sprite[] swordSprites;
    [SerializeField] public Sprite[] magicSprites;
    [SerializeField] public Sprite[] bowSprites;

    void Awake() {
        Instance = this;
    }

    public Sprite GetWeaponSprite(Weapon weapon) {
        int levelIndex = weapon.level - 1;
        switch (weapon.weaponType) {
            case Weapons.Sword:
                return swordSprites[levelIndex];
            case Weapons.Magic:
                return magicSprites[levelIndex];
            case Weapons.Bow:
                return bowSprites[levelIndex];
        }
        return new Sprite(); //dummy sprite
    }

}
