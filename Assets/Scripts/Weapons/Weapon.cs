﻿using UnityEngine;
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

    public virtual IEnumerator Attack(int wielderAttack) {
        //mySoundBox.PlayOneShot(myClips[level-1]);
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
        if (col.gameObject.layer == Layers.enemy) {
            col.GetComponent<Enemy>().TakeDamage(wielderAttack + attack);
        }
    }
}
