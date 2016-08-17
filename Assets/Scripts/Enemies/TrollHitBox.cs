using UnityEngine;
using System.Collections;

public class TrollHitBox : EnemyHitBox {

    [SerializeField] GiantTroll myGiantTroll;
    public int healthForLastPlea;
    bool firstHit=true;

    public override void TakeDamage(int attack) {
        int damageTaken = Mathf.Clamp(attack - myStats.defense, 0, int.MaxValue);
        myStats.currentHealth -= damageTaken;
        
        if (firstHit) {
            firstHit = false;
            myGiantTroll.SpeakNextLine(1);
        }
        else if (myStats.currentHealth < healthForLastPlea) {
            myGiantTroll.SpeakNextLine(2);
        }

        if (myStats.currentHealth <= 0) {
            AchievementSensor.Instance.EarnAchievement(Achievement.KillTheTroll);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
