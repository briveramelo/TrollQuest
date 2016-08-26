using UnityEngine;
using System.Collections.Generic;

public class EnemyWeapon : MonoBehaviour {

    public int attack;
    int currentAttack;
    [SerializeField] GameObject soundFXGameObject;
    [SerializeField] AudioClip hitSFX;
    List<Collider2D> thisSwingsHits = new List<Collider2D>();

    public void SetAttack(int wielderAttack) {
        currentAttack = attack + wielderAttack;
        active = true;
        TemporaryAudioPlayer soundBox = (Instantiate(soundFXGameObject, Camera.main.transform) as GameObject).GetComponent<TemporaryAudioPlayer>();
        soundBox.SetClip(hitSFX);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.player && active){
            col.GetComponent<Hero>().TakeDamage(currentAttack);
            active = false;
        }
    }

    bool active = false;
}
