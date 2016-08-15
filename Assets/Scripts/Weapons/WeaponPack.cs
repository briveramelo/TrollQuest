using UnityEngine;
using System.Collections;
using System;

public class WeaponPack : MonoBehaviour {

    public Sword mySword = new Sword();
    public Magic myMagic = new Magic();
    public Bow myBow = new Bow();
    public Weapons selectedWeapon = Weapons.Sword;
    public bool attacking;

    public IEnumerator Attack() {
        attacking = true;
        switch (selectedWeapon) {
            case Weapons.Sword:
                yield return StartCoroutine(mySword.Attack());
                break;
            case Weapons.Magic:
                yield return StartCoroutine(myMagic.Attack());
                break;
            case Weapons.Bow:
                yield return StartCoroutine(myBow.Attack());
                break; ;
        }
        attacking = false;
    }

    public Weapon GetSelectedWeapon() {
        switch (selectedWeapon) {
            case Weapons.Sword:
                return mySword;
            case Weapons.Magic:
                return myMagic;
            case Weapons.Bow:
                return myBow;
        }
        return new Sword(); //dummy, unreachable
    }

    public void Upgrade(Weapon weapon) {
        switch (weapon.weaponType) {
            case Weapons.Sword:
                mySword = (Sword)weapon;
                break;
            case Weapons.Magic:
                myMagic = (Magic)weapon;
                break;
            case Weapons.Bow:
                myBow = (Bow)weapon;
                break;
        }
    }
}
