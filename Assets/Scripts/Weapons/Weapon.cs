using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum Weapons {
    Sword=0,
    Magic=1,
    Bow=2
}

public class Weapon : MonoBehaviour{

    public Collider2D myCol;
    public WeaponStats myWeaponStats;
    public AudioSource mySoundBox;
    public int wielderAttack;
    List<Collider2D> thisSwingsHits = new List<Collider2D>();

    public virtual IEnumerator Attack(int wielderAttack) {
        //mySoundBox.PlayOneShot(myWeaponStats.myAudioClips[level-1]);
        thisSwingsHits = new List<Collider2D>();
        this.wielderAttack = wielderAttack;
        myCol.enabled = true;
        yield return new WaitForSeconds((1f / (float)myWeaponStats.strikesPerSecond) * myWeaponStats.percentTimeOfDanger);
        myCol.enabled = false;
        yield return new WaitForSeconds((1f / (float)myWeaponStats.strikesPerSecond) * (1f - myWeaponStats.percentTimeOfDanger));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.enemy && !thisSwingsHits.Contains(col)) {
            thisSwingsHits.Add(col);
            col.GetComponent<EnemyHitBox>().TakeDamage(wielderAttack + myWeaponStats.attack);
            myCol.enabled = false;
        }
    }

    public void Upgrade(WeaponStats newWeaponStats) {
        myWeaponStats.SetWeaponStats(newWeaponStats);
    }
}
