using UnityEngine;
using System.Collections.Generic;

public enum Stat {
    Health=0,
    Attack=1,
    Defense=2,
    CurrentHealth=3
}

public static class StatClass {
    public static Dictionary<Stat, string> statStrings = new Dictionary<Stat, string>() {
        {Stat.Health ,"MAX HLT" },
        {Stat.Attack ,"ATK" },
        {Stat.Defense ,"DFN" },
        {Stat.CurrentHealth ,"HLT" }
    };
}

public class Stats : MonoBehaviour{

    public int health=1;
    public int currentHealth=1;
    public int attack=1;
    public int defense=1;

    public Stats(Stats newStats) {
        this.health = newStats.health;
        this.currentHealth = newStats.health;
        this.attack = newStats.attack;
        this.defense = newStats.defense;
    }
    public Stats(SaveableStats newStats) {
        this.health = newStats.health;
        this.currentHealth = newStats.health;
        this.attack = newStats.attack;
        this.defense = newStats.defense;
    }
    public void SetStats(SaveableStats newStats) {
        this.health = newStats.health;
        this.currentHealth = newStats.health;
        this.attack = newStats.attack;
        this.defense = newStats.defense;
    }

    public void Upgrade(StatUpgrade statUpgrade) {
        switch (statUpgrade.StatToUpgrade) {
            case Stat.Health:
                health += statUpgrade.upgradeAmount;
                break;
            case Stat.CurrentHealth:
                currentHealth += statUpgrade.upgradeAmount;
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
