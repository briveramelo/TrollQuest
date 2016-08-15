using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum Weapons {
    Sword=0,
    Magic=1,
    Bow=2
}


public class Weapon : MonoBehaviour{

    [HideInInspector] public Weapons weaponType;
    public Collider2D myCol;
    public int attack=1;
    public int wielderAttack;
    [Range (1,3)] public int level=1;
    [Range (2,10)] public int strikesPerSecond = 2;
    [Range(0, 1)] public float percentTimeOfDanger = 0.6f;
    public AudioSource mySoundBox;
    public AudioClip[] myClips;
    List<Collider2D> thisSwingsHits = new List<Collider2D>();

    public virtual IEnumerator Attack(int wielderAttack) {
        //mySoundBox.PlayOneShot(myClips[level-1]);
        thisSwingsHits = new List<Collider2D>();
        this.wielderAttack = wielderAttack;
        myCol.enabled = true;
        yield return new WaitForSeconds((1f / (float)strikesPerSecond) * percentTimeOfDanger);
        myCol.enabled = false;
        yield return new WaitForSeconds((1f / (float)strikesPerSecond) * (1f - percentTimeOfDanger));
    }

    public void GetCollected() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Does this have the col?");
        Debug.Log(thisSwingsHits.Contains(col));
        if (col.gameObject.layer == Layers.enemy && !thisSwingsHits.Contains(col)) {
            thisSwingsHits.Add(col);
            col.GetComponent<Enemy>().TakeDamage(wielderAttack + attack);
            myCol.enabled = false;
        }
    }

    public void Upgrade(Weapon newWeapon) {
        attack = newWeapon.attack;
        level = newWeapon.level;
        strikesPerSecond = newWeapon.strikesPerSecond;
        percentTimeOfDanger = newWeapon.percentTimeOfDanger;
        myClips = newWeapon.myClips;
    }
}
