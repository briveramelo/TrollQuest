using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum Weapons {
    Sword=0,
    Magic=1,
    Bow=2,
    Arrow=3
}

public class Weapon : MonoBehaviour{

    public Collider2D myCol;
    public WeaponStats myWeaponStats;
    public AudioSource mySoundBox;
    [HideInInspector] public int wielderAttack;
    [SerializeField] protected Animator myAnimator;
    [SerializeField] protected SpriteRenderer mySpriteRenderer;
    protected List<Collider2D> thisSwingsHits = new List<Collider2D>();

    public virtual IEnumerator Attack(int wielderAttack, CardinalDirection attackDir) {
        int numClips = myWeaponStats.myAudioClips.Length;
        int levelInd = myWeaponStats.level - 1;
        if (levelInd < numClips) {
            mySoundBox.PlayOneShot(myWeaponStats.myAudioClips[levelInd]);
        }
        thisSwingsHits = new List<Collider2D>();
        this.wielderAttack = wielderAttack;
        myCol.enabled = true;
        yield return new WaitForSeconds((1f / (float)myWeaponStats.strikesPerSecond) * myWeaponStats.percentTimeOfDanger);
        myCol.enabled = false;
        yield return new WaitForSeconds((1f / (float)myWeaponStats.strikesPerSecond) * (1f - myWeaponStats.percentTimeOfDanger));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.enemy && !thisSwingsHits.Contains(col)) {
            thisSwingsHits.Add(col);
            col.GetComponent<EnemyHitBox>().TakeDamage(wielderAttack + myWeaponStats.attack);
            myCol.enabled = false;
        }
    }

    public void Upgrade(WeaponStats newWeaponStats) {
        myWeaponStats.SetWeaponStats(newWeaponStats);
        switch (newWeaponStats.weaponType) {
            case Weapons.Sword:
                mySpriteRenderer.sprite = WeaponSprites.Instance.swordSprites[newWeaponStats.level-1];
                break;
            case Weapons.Magic:
                mySpriteRenderer.sprite = WeaponSprites.Instance.magicSprites[newWeaponStats.level - 1];
                break;
            case Weapons.Bow:
                mySpriteRenderer.sprite = WeaponSprites.Instance.bowSprites[newWeaponStats.level - 1];
                myAnimator.SetInteger("AnimState", (int)levelReleasedStates[newWeaponStats.level]);
                break;
        }
        
    }

    Dictionary<int, BowAnimState> levelReleasedStates = new Dictionary<int, BowAnimState>() {
        {1,BowAnimState.Bow1Released},
        {2,BowAnimState.Bow2Released},
        {3,BowAnimState.Bow3Released}
    };
    enum BowAnimState {
        Bow1Cocking = 0,
        Bow1Released = 1,
        Bow2Cocking = 2,
        Bow2Released = 3,
        Bow3Cocking = 4,
        Bow3Released = 5,
    }
}
