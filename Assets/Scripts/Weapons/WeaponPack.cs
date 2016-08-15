using UnityEngine;
using System.Collections;
using System;

public class WeaponPack : MonoBehaviour {

    public Sword mySword;
    public Magic myMagic;
    public Bow myBow;
    public Weapons selectedWeapon = Weapons.Sword;
    public bool attacking;

    public IEnumerator Attack(int wielderAttack) {
        attacking = true;
        switch (selectedWeapon) {
            case Weapons.Sword:
                yield return StartCoroutine(mySword.Attack(wielderAttack));
                break;
            case Weapons.Magic:
                yield return StartCoroutine(myMagic.Attack(wielderAttack));
                break;
            case Weapons.Bow:
                yield return StartCoroutine(myBow.Attack(wielderAttack));
                break;
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
