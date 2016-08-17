using UnityEngine;
using System.Collections;

public enum Stat {
    Health=0,
    Attack=1,
    Defense=2
}

public class Stats : MonoBehaviour{

    public int health;
    public int attack;
    public int defense;

    public Stats(int health, int attack, int defense) {
        this.health = health;
        this.attack = attack;
        this.defense = defense;
    }

    public void Upgrade(StatUpgrade statUpgrade) {
        switch (statUpgrade.StatToUpgrade) {
            case Stat.Health:
                health += statUpgrade.upgradeAmount;
                break;
            case Stat.Attack:
                attack += statUpgrade.upgradeAmount;
                break;
            case Stat.Defense:
                defense += statUpgrade.upgradeAmount;
                break;
        }
    }
}
