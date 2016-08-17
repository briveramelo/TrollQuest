using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WeaponPack : MonoBehaviour {

    public Dictionary<Weapons, Weapon> weaponPack;
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
}
