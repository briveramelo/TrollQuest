using UnityEngine;
using System.Collections;

public enum Weapons {
    Sword=0,
    Magic=1,
    Bow=2
}

public class WeaponPack{
    public Sword mySword = new Sword();
    public Magic myMagic = new Magic();
    public Bow myBow = new Bow();

    public Weapon GetSelectedWeapon(Weapons selectedWeapon) {
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

    public void Upgrade(Weapon weapon){
        switch (weapon.weaponType){
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

public abstract class Weapon : MonoBehaviour{

    public Weapons weaponType;
    public Collider2D myCol;
    public int attack=1;
    [Range (1,3)] public int level=1;

    public void GetCollected() {
        Destroy(gameObject);
    }

    public abstract void Attack();
}
