using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour {

    [SerializeField] protected Stats myStats;

    public virtual void TakeDamage(int attack) {
        int damageTaken = Mathf.Clamp(attack - myStats.defense, 0, int.MaxValue);
        myStats.currentHealth -= damageTaken;
        if (myStats.currentHealth <= 0) {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

}
