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
}
