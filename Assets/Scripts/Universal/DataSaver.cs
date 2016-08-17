using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSaver : MonoBehaviour {

    public static DataSaver Instance;
    DataSave currentDataSave;
    const int maxScores = 5;
    public bool dataExists;

    void Awake() {
        Instance = this;
        dataExists = File.Exists(Application.dataPath + "/savefile.dat");
        Load();
    }

    void Load() {
        if (dataExists) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.dataPath + "/savefile.dat", FileMode.Open);
            currentDataSave = new DataSave((DataSave)bf.Deserialize(fileStream));
            fileStream.Close();
        }
        else {
            currentDataSave = new DataSave();
        }
    }

    public void PromptSave(Dictionary<Achievement, bool> completeAchievements) {
        currentDataSave.completeAchievements = completeAchievements;
        Save();
    }
    public void PromptSave(Stats playerStats) {
        currentDataSave.playerStats = new SaveableStats(playerStats);
        Save();
    }
    public void PromptSave(WeaponPack weaponPack) {
        currentDataSave.playerWeaponPack = new WeaponPackSaveable(weaponPack);
        Save();
    }

    void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.dataPath + "/savefile.dat");

        bf.Serialize(fileStream, currentDataSave);
        fileStream.Close();
    }

    public DataSave CopyCurrentDataSave() {
        return new DataSave(currentDataSave);
    }

}

[Serializable]
public class DataSave {
    public Dictionary<Achievement, bool> completeAchievements = new Dictionary<Achievement, bool>() {
        {Achievement.KillTheTroll, false },
        {Achievement.FindThePond, false },
        {Achievement.DoTheThing, false },
    };
    public SaveableStats playerStats = new SaveableStats(1,1,1);
    public WeaponPackSaveable playerWeaponPack = new WeaponPackSaveable();
    public DataSave(DataSave dataSave) {
        this.completeAchievements = dataSave.completeAchievements;
        this.playerStats = dataSave.playerStats;
        this.playerWeaponPack = dataSave.playerWeaponPack;
    }
    public DataSave() { }
}

[Serializable]
public class SaveableStats{
    public int health = 1;
    public int attack = 1;
    public int defense = 1;
    public int currentHealth = 1;
    public SaveableStats(Stats newStats) {
        this.health = newStats.health;
        this.currentHealth = newStats.health;
        this.attack = newStats.attack;
        this.defense = newStats.defense;
    }
    public SaveableStats(int health, int attack, int defense) {
        this.health = health;
        this.currentHealth = health;
        this.attack = attack;
        this.defense = defense;
    }
    public SaveableStats() { }
}

[Serializable]
public class WeaponPackSaveable {

    public Dictionary<Weapons, bool> weaponsCollected = new Dictionary<Weapons, bool>() {
        {Weapons.Sword, true },
        {Weapons.Magic, false },
        {Weapons.Bow, false }
    };
    public WeaponStatsSavable mySwordStats = new WeaponStatsSavable();
    public WeaponStatsSavable myMagicStats = new WeaponStatsSavable();
    public WeaponStatsSavable myBowStats = new WeaponStatsSavable();

    public WeaponPackSaveable(WeaponPack weaponPack) {
        this.weaponsCollected = weaponPack.weaponsCollected;
        if (weaponPack.mySword.myWeaponStats!=null) {
            this.mySwordStats.SetWeaponStats(weaponPack.mySword.myWeaponStats);
        }
        if (weaponPack.myMagic.myWeaponStats != null) {
            this.myMagicStats.SetWeaponStats(weaponPack.myMagic.myWeaponStats);
        }
        if (weaponPack.myBow.myWeaponStats != null) {
            this.myBowStats.SetWeaponStats(weaponPack.myBow.myWeaponStats);
        }
    }
    public WeaponPackSaveable() { }
}

[Serializable]
public class WeaponStatsSavable {
    public Weapons weaponType = Weapons.Sword;
    public int attack=1;
    [Range(1, 3)] public int level = 1;
    [Range(2, 10)] public int strikesPerSecond = 2;
    [Range(0, 1)] public float percentTimeOfDanger = 0.6f;

    public void SetWeaponStats(WeaponStats weaponStats) {
        this.weaponType = weaponStats.weaponType;
        this.attack = weaponStats.attack;
        this.level = weaponStats.level;
        this.strikesPerSecond = weaponStats.strikesPerSecond;
        this.percentTimeOfDanger = weaponStats.percentTimeOfDanger;
    }
}