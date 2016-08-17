using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour {

    [SerializeField] protected Stats myStats;
    public int currentHealth;

    void Awake() {
        currentHealth = myStats.health;
    }

    public virtual void TakeDamage(int attack) {
        Debug.Log(attack);
        int damageTaken = Mathf.Clamp(attack - myStats.defense, 0, int.MaxValue);
        currentHealth -= damageTaken;
        if (currentHealth <= 0) {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

}
