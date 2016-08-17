using UnityEngine;
using System.Collections;

public class WeaponStats : MonoBehaviour {
    public Weapons weaponType;
    public int attack;
    [Range (1,3)] public int level=1;
    [Range (2,10)] public int strikesPerSecond = 2;
    [Range(0, 1)] public float percentTimeOfDanger = 0.6f;
    public AudioClip[] myAudioClips;

    public void GetCollected() {
        Destroy(gameObject);
    }

    public void SetWeaponStats(WeaponStatsSavable weaponStats) {
        this.weaponType = weaponStats.weaponType;
        this.attack = weaponStats.attack;
        this.level = weaponStats.level;
        this.strikesPerSecond = weaponStats.strikesPerSecond;
        this.percentTimeOfDanger = weaponStats.percentTimeOfDanger;
    }
    public void SetWeaponStats(WeaponStats weaponStats) {
        this.weaponType = weaponStats.weaponType;
        this.attack = weaponStats.attack;
        this.level = weaponStats.level;
        this.strikesPerSecond = weaponStats.strikesPerSecond;
        this.percentTimeOfDanger = weaponStats.percentTimeOfDanger;
    }
}
