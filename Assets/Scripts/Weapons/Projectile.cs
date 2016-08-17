using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Collider2D myCol;
    public Rigidbody2D rigbod;
    [SerializeField] CharacterToDamage characterToDamage;

    enum CharacterToDamage{
        Hero=9,
        Enemy=11
    }
    public int attack;

    void Awake() {

    }

    public void Launch(Vector2 moveDir, int attack) {
        this.attack += attack;
        rigbod.velocity = moveDir;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == (int)characterToDamage) {
            if (characterToDamage == CharacterToDamage.Enemy) {
                col.GetComponent<EnemyHitBox>().TakeDamage(attack);
            }
            else if (characterToDamage == CharacterToDamage.Hero) {
                col.GetComponent<Hero>().TakeDamage(attack);
            }
            Destroy(gameObject);
        }
    }

}
