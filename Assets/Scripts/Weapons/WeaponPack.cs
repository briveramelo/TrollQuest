using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponPack : MonoBehaviour {

    public Dictionary<Weapons, Weapon> weaponPack;
    public Dictionary<Weapons, bool> weaponsCollected = new Dictionary<Weapons, bool>() {
        {Weapons.Sword, true },
        {Weapons.Magic, false },
        {Weapons.Bow, false }
    };
    public Sword mySword;
    public Magic myMagic;
    public Bow myBow;
    public Weapons selectedWeapon = Weapons.Sword;
    public bool attacking;

    void Awake() {
        weaponPack = new Dictionary<Weapons, Weapon>() {
            { Weapons.Sword, mySword },
            { Weapons.Magic, myMagic },
            { Weapons.Bow, myBow },
        };
    }

    public IEnumerator Attack(int wielderAttack) {
        attacking = true;
        yield return StartCoroutine(weaponPack[selectedWeapon].Attack(wielderAttack));
        attacking = false;
    }

    public Weapon GetSelectedWeapon() {
        return weaponPack[selectedWeapon];
    }

    public void Upgrade(WeaponStats newWeaponStats) {
        weaponPack[newWeaponStats.weaponType].Upgrade(newWeaponStats);
    }

    public void SetWeaponPack(WeaponPackSaveable weaponPack) {
        this.weaponsCollected = weaponPack.weaponsCollected;
        this.mySword.myWeaponStats.SetWeaponStats(weaponPack.mySwordStats);
        this.myMagic.myWeaponStats.SetWeaponStats(weaponPack.myMagicStats);
        this.myBow.myWeaponStats.SetWeaponStats(weaponPack.myBowStats);
    }
}
