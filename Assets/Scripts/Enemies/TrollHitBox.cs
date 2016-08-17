using UnityEngine;
using System.Collections;

public class TrollHitBox : EnemyHitBox {

    [SerializeField] GiantTroll myGiantTroll;
    public int healthForLastPlea;
    bool firstHit=true;

    public override void TakeDamage(int attack) {
        base.TakeDamage(attack);
        if (firstHit) {
            firstHit = false;
            myGiantTroll.SpeakNextLine(1);
        }
        else if (currentHealth < healthForLastPlea) {
            myGiantTroll.SpeakNextLine(2);
        }
    }
}
