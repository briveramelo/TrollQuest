using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum CardinalDirection {
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

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
    public WeaponPack myWeaponPack;
    public Dictionary<Weapons, bool> weaponsCollected = new Dictionary<Weapons, bool>() {
        {Weapons.Sword, true },
        {Weapons.Magic, false },
        {Weapons.Bow, false }
    };
    float moveSpeed = 5f;
    [SerializeField] Stats myStats;
    public int currentHealth;

    void Start() {
        CameraFollowPlayer.Instance.getNewHero(transform);
        PauseMenu.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon());
        PauseMenu.Instance.SetStats(myStats);
        currentHealth = myStats.health;
        UICanvas.Instance.SetHealth(currentHealth, myStats.health);
    }

    void Update() {
        CheckForWeaponInput();
        CheckForMovementInput();
    }

    void CheckForWeaponInput() {
        if (Input.GetButtonDown(Controls.Attack) && !myWeaponPack.attacking) {
            HandleAttack(myLastDir);
        }
        else if (Input.GetButtonDown(Controls.Swap)){
            SwapWeapons();
        }
    }


    #region HandleAttack
    void HandleAttack(CardinalDirection myCardDir) {
        StartCoroutine (myWeaponPack.Attack(myStats.attack));
        if (myWeaponPack.selectedWeapon == Weapons.Bow){
            myAnimator.SetInteger("AnimState", (int)cardDirAttacks[myCardDir].bowAnimState);
        }
        else{
            myAnimator.SetInteger("AnimState", (int)cardDirAttacks[myCardDir].swordMagicAnimState);
        }
    }

    class DirectionAttacks {
        public AnimState bowAnimState;
        public AnimState swordMagicAnimState;

        public DirectionAttacks(AnimState bowAnimState, AnimState swordMagicAnimState) {
            this.bowAnimState = bowAnimState;
            this.swordMagicAnimState = swordMagicAnimState;
        }
    }

    Dictionary<CardinalDirection, DirectionAttacks> cardDirAttacks = new Dictionary<CardinalDirection, DirectionAttacks>() {
        {CardinalDirection.Up, new DirectionAttacks(AnimState.BowUp, AnimState.SwordMagicUp) },
        {CardinalDirection.Right, new DirectionAttacks(AnimState.BowRight, AnimState.SwordMagicRight) },
        {CardinalDirection.Down, new DirectionAttacks(AnimState.BowDown, AnimState.SwordMagicDown) },
        {CardinalDirection.Left, new DirectionAttacks(AnimState.BowLeft, AnimState.SwordMagicLeft) },
    };
    #endregion

    void SwapWeapons() {
        myWeaponPack.selectedWeapon++;
        if ((int)myWeaponPack.selectedWeapon > System.Enum.GetValues(typeof(Weapons)).Length-1) {
            myWeaponPack.selectedWeapon = 0;
        }
        else if (!weaponsCollected[myWeaponPack.selectedWeapon]) {
            SwapWeapons();
        }
        UICanvas.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon());
        PauseMenu.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon());
    }

    #region AnimStates and Cardinal Directions
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

    
    public CardinalDirection myLastDir = CardinalDirection.Down;
    #endregion

    #region HandleMovement

    void CheckForMovementInput() {
        if (HandleMovement(CardinalDirection.Up)) { }
        else if (HandleMovement(CardinalDirection.Right)) { }
        else if (HandleMovement(CardinalDirection.Down)) { }
        else if (HandleMovement(CardinalDirection.Left)) { }
        else { HandleIdle(myLastDir); }
    }

    bool HandleMovement(CardinalDirection cardMoveDir) {
        if (Input.GetButton(moveDirectionsValues[cardMoveDir].inputControlString)){
            rigbod.velocity = moveDirectionsValues[cardMoveDir].moveDir * moveSpeed;
            myAnimator.SetInteger("AnimState", (int)moveDirectionsValues[cardMoveDir].runAnimState);
            myLastDir = cardMoveDir;
            return true;
        }
        return false;
    }

    void HandleIdle(CardinalDirection cardMoveDir) {
        rigbod.velocity = Vector2.zero;
        myAnimator.SetInteger("AnimState", (int)moveDirectionsValues[cardMoveDir].idleAnimState);
    }

    class DirectionValues {
        public Vector2 moveDir;
        public AnimState runAnimState;
        public AnimState idleAnimState;
        public string inputControlString;
        public DirectionValues(Vector2 moveDir, AnimState runAnimState, AnimState idleAnimState, string inputControlString) {
            this.moveDir = moveDir;
            this.runAnimState = runAnimState;
            this.idleAnimState = idleAnimState;
            this.inputControlString = inputControlString;
        }
    }

    Dictionary<CardinalDirection, DirectionValues> moveDirectionsValues = new Dictionary<CardinalDirection, DirectionValues>() {
        {CardinalDirection.Up, new DirectionValues(Vector2.up, AnimState.RunUp, AnimState.IdleUp, Controls.Up)},
        {CardinalDirection.Right, new DirectionValues(Vector2.right, AnimState.RunRight, AnimState.IdleRight, Controls.Right)},
        {CardinalDirection.Down, new DirectionValues(Vector2.down, AnimState.RunDown, AnimState.IdleDown, Controls.Down)},
        {CardinalDirection.Left, new DirectionValues(Vector2.left, AnimState.RunLeft, AnimState.IdleLeft, Controls.Left)},
    };
    #endregion

    #region CollectThings
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.shrooms) {
            CollectShrooms(col.GetComponent<Shroom>());
        }
        else if (col.gameObject.layer == Layers.weaponUpgrades) {
            CollectWeapon(col.GetComponent<Weapon>());
        }
        else if (col.gameObject.layer == Layers.loot) {
            Debug.Log("Got LOOT!");
        }
    }

    void CollectShrooms(Shroom collectedShroom) {
        StatUpgrade shroomStatUpgrade = collectedShroom.GetCollected();
        myStats.Upgrade(shroomStatUpgrade);
        PauseMenu.Instance.SetStats(myStats);
        UICanvas.Instance.SetHealth(currentHealth, myStats.health);
    }

    void CollectWeapon(Weapon collectedWeapon) {
        weaponsCollected[collectedWeapon.weaponType] = true;
        myWeaponPack.Upgrade(collectedWeapon);
        collectedWeapon.GetCollected();
        PauseMenu.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon());
        UICanvas.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon());
    }
    #endregion

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
