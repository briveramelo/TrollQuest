using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour {

    [SerializeField] protected Stats myStats;

    float verticaDisplayOffset = 0.5f;
    Color textColor = new Color(0f, (float)((float)174 / (float)255), 1f);
    public virtual void TakeDamage(int attack) {
        int damageTaken = Mathf.Clamp(attack - myStats.defense, 0, int.MaxValue);
        if (damageTaken > 0) {
            string displayText = "-" + damageTaken.ToString();
            Vector3 spawnSpot = transform.position + Vector3.up * verticaDisplayOffset;
            DisplayRepository.Instance.CreateDisplay(Display.Damage, spawnSpot, displayText, textColor);
        }
        myStats.currentHealth -= damageTaken;
        if (myStats.currentHealth <= 0) {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

}
