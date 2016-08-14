using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public static class Controls {
    public static string Attack = "Attack";
    public static string Swap = "Swap";
    public static string Up = "Up";
    public static string Down = "Down";
    public static string Left = "Left";
    public static string Right = "Right";
    public static string Pause = "Pause";
}

public class Hero : MonoBehaviour {

    [SerializeField] Rigidbody2D rigbod;
    [SerializeField] Animator myAnimator;
    public WeaponPack myWeapons = new WeaponPack();
    public Weapons selectedWeapon = Weapons.Sword;
    public Dictionary<Weapons, bool> weaponsCollected = new Dictionary<Weapons, bool>() {
        {Weapons.Sword, true },
        {Weapons.Magic, false },
        {Weapons.Bow, false }
    };
    float moveSpeed = 5f;
    Stats myStats = new Stats(1000, 10, 10);
    public int currentHealth;

    void Start() {
        CameraFollowPlayer.Instance.getNewHero(transform);
        PauseMenu.Instance.SetWeapon(myWeapons.GetSelectedWeapon(selectedWeapon));
        PauseMenu.Instance.SetStats(myStats);
        currentHealth = myStats.health;
        UICanvas.Instance.SetHealth(currentHealth, myStats.health);
    }

    void Update() {
        CheckForWeaponInput();
        CheckForMovementInput();
    }

    void CheckForWeaponInput() {
        if (Input.GetButtonDown(Controls.Attack)){
            Attack();
        }
        else if (Input.GetButtonDown(Controls.Swap)){
            SwapWeapons();
        }
    }

    void Attack() {

    }

    void SwapWeapons() {
        selectedWeapon++;
        if ((int)selectedWeapon > System.Enum.GetValues(typeof(Weapons)).Length-1) {
            selectedWeapon = 0;
        }
        else if (!weaponsCollected[selectedWeapon]) {
            SwapWeapons();
        }
        UICanvas.Instance.SetWeapon(myWeapons.GetSelectedWeapon(selectedWeapon));
        PauseMenu.Instance.SetWeapon(myWeapons.GetSelectedWeapon(selectedWeapon));
    }

    enum AnimState {
        RunUp=0,
        RunRight=1,
        RunDown=2,
        RunLeft=3,
        IdleUp=4,
        IdleRight=5,
        IdleDown=6,
        IdleLeft=7,
        SwordMagicUp=8,
        SwordMagicRight=9,
        SwordMagicDown=10,
        SwordMagicLeft=11,
        BowUp = 12,
        BowRight =13,
        BowDown =14,
        BowLeft=15
    }

    enum LastDirection {
        Up=0,
        Right=1,
        Down=2,
        Left=3
    }
    LastDirection myLastDir;
    void CheckForMovementInput() {
        if (Input.GetButton(Controls.Up)){
            rigbod.velocity = Vector2.up * moveSpeed;
            myAnimator.SetInteger("AnimState", (int)AnimState.RunUp);
            myLastDir = LastDirection.Up;
        }
        else if (Input.GetButton(Controls.Down)){
            rigbod.velocity = Vector2.down * moveSpeed;
            myAnimator.SetInteger("AnimState", (int)AnimState.RunDown);
            myLastDir = LastDirection.Down;
        }
        else if (Input.GetButton(Controls.Left)){
            rigbod.velocity = Vector2.left * moveSpeed;
            myAnimator.SetInteger("AnimState", (int)AnimState.RunLeft);
            myLastDir = LastDirection.Left;
        }
        else if (Input.GetButton(Controls.Right)){
            rigbod.velocity = Vector2.right * moveSpeed;
            myAnimator.SetInteger("AnimState", (int)AnimState.RunRight);
            myLastDir = LastDirection.Right;
        }
        else{
            rigbod.velocity = Vector2.zero;
            switch (myLastDir) {
                case LastDirection.Up:
                    myAnimator.SetInteger("AnimState", (int)AnimState.IdleUp);
                    break;
                case LastDirection.Right:
                    myAnimator.SetInteger("AnimState", (int)AnimState.IdleRight);
                    break;
                case LastDirection.Down:
                    myAnimator.SetInteger("AnimState", (int)AnimState.IdleDown);
                    break;
                case LastDirection.Left:
                    myAnimator.SetInteger("AnimState", (int)AnimState.IdleLeft);
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.shrooms) {
            myStats.Upgrade(col.GetComponent<Shroom>().GetCollected());
            PauseMenu.Instance.SetStats(myStats);
            UICanvas.Instance.SetHealth(currentHealth, myStats.health);
        }
        else if (col.gameObject.layer == Layers.weaponUpgrades) {
            Weapon collectedWeapon = col.GetComponent<Weapon>();
            myWeapons.Upgrade(collectedWeapon);
            collectedWeapon.GetCollected();
            PauseMenu.Instance.SetWeapon(myWeapons.GetSelectedWeapon(selectedWeapon));
            UICanvas.Instance.SetWeapon(myWeapons.GetSelectedWeapon(selectedWeapon));
        }
        else if (col.gameObject.layer == Layers.loot) {
            Debug.Log("Got LOOT!");
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.layer == Layers.room) {
            if (Input.GetButtonDown(Controls.Attack)) {
                int nextScene = col.GetComponent<Room>().GetScene();
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void TakeDamage(int attack) {
        int damageTaken = Mathf.Clamp(attack - myStats.defense, 0, int.MaxValue);
        currentHealth -= damageTaken;
        UICanvas.Instance.SetHealth(currentHealth, myStats.health);
        if (currentHealth <= 0) {
            //Display game over message and boot to main menu
        }
    }
}
