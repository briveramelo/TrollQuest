using UnityEngine;
using System.Collections;

public class TrollHitBox : EnemyHitBox {

    [SerializeField] GiantTroll myGiantTroll;
    public int healthForLastPlea;
    bool firstHit=true;

    float verticaDisplayOffset = 0.5f;
    Color textColor = new Color(0f, (float)((float)174 / (float)255), 1f);
    public override void TakeDamage(int attack) {
        int damageTaken = Mathf.Clamp(attack - myStats.defense, 0, int.MaxValue);
        if (damageTaken > 0) {
            string displayText = "-" + damageTaken.ToString();
            Vector3 spawnSpot = transform.position + Vector3.up * verticaDisplayOffset;
            DisplayRepository.Instance.CreateDisplay(Display.Damage, spawnSpot, displayText, textColor);
        }
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
