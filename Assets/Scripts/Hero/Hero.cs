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
    [SerializeField] GameObject soundFXGameObject;
    [SerializeField] AudioClip hitSFX;
    public WeaponPack myWeaponPack;
    
    float moveSpeed = 5f;
    public static int lives;
    [SerializeField] Stats myStats;

    void Start() {
        if (DataSaver.Instance.dataExists) {
            //LoadStats();
        }
        Invoke("FillUI", 0.1f);
    }

    void FillUI() {
        PauseMenu.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon().myWeaponStats);
        PauseMenu.Instance.SetStats(myStats);
        UICanvas.Instance.SetHealth(myStats);
    }

    void LoadStats() {
        DataSave quickSave = DataSaver.Instance.CopyCurrentDataSave();
        myStats.SetStats(quickSave.playerStats);
        myWeaponPack.SetWeaponPack(quickSave.playerWeaponPack);
    }

    void Update() {
        CheckForWeaponInput();
        CheckForMovementInput();
    }

    void CheckForWeaponInput() {
        if (Input.GetButtonDown(Controls.Attack) && !myWeaponPack.attacking) {
            StartCoroutine(myWeaponPack.Attack(myStats.attack, myLastDir));
            SetAttackAnimState();
        }
        else if (Input.GetButtonDown(Controls.Swap)){
            SwapWeapons();
        }
    }

    Dictionary<CardinalDirection, AnimState> bowAnimStates = new Dictionary<CardinalDirection, AnimState>() {
        {CardinalDirection.Up, AnimState.BowUp },
        {CardinalDirection.Right, AnimState.BowRight },
        {CardinalDirection.Down, AnimState.BowRight },
        {CardinalDirection.Left, AnimState.BowLeft }
    };

    Dictionary<CardinalDirection, AnimState> swordMagicAnimStates = new Dictionary<CardinalDirection, AnimState>() {
        {CardinalDirection.Up, AnimState.SwordMagicUp },
        {CardinalDirection.Right, AnimState.SwordMagicRight },
        {CardinalDirection.Down, AnimState.SwordMagicRight },
        {CardinalDirection.Left, AnimState.SwordMagicLeft }
    };

    Dictionary<CardinalDirection, AnimState> runningAnimStates = new Dictionary<CardinalDirection, AnimState>() {
        {CardinalDirection.Up, AnimState.RunUp },
        {CardinalDirection.Right, AnimState.RunRight },
        {CardinalDirection.Left, AnimState.RunLeft }
    };

    #region HandleAttack
    void SetAttackAnimState() {
        if (myWeaponPack.selectedWeapon == Weapons.Bow){
            if (myLastDir != CardinalDirection.Down) {
                myAnimator.SetInteger("AnimState", (int)bowAnimStates[myLastDir]);
            }
            else {
                if (myLastX == CardinalDirection.Right) {
                    myAnimator.SetInteger("AnimState", (int)AnimState.BowRight);
                }
                else {
                    myAnimator.SetInteger("AnimState", (int)AnimState.BowLeft);
                }
            }
        }
        else{
            if (myLastDir != CardinalDirection.Down) {
                myAnimator.SetInteger("AnimState", (int)swordMagicAnimStates[myLastDir]);
            }
            else {
                if (myLastX == CardinalDirection.Right) {
                    myAnimator.SetInteger("AnimState", (int)AnimState.SwordMagicRight);
                }
                else {
                    myAnimator.SetInteger("AnimState", (int)AnimState.SwordMagicLeft);
                }
            }
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

    #endregion

    void SwapWeapons() {
        myWeaponPack.GetSelectedWeapon().GetComponent<SpriteRenderer>().enabled = false;
        myWeaponPack.selectedWeapon++;
        if ((int)myWeaponPack.selectedWeapon > (int)Weapons.Bow) {
            myWeaponPack.selectedWeapon = 0;
        }
        else if (!myWeaponPack.weaponsCollected[myWeaponPack.selectedWeapon]) {
            SwapWeapons();
            return;
        }
        if (myWeaponPack.selectedWeapon != Weapons.Magic) {
            myWeaponPack.GetSelectedWeapon().GetComponent<SpriteRenderer>().enabled = true;
        }
        UICanvas.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon().myWeaponStats);
        PauseMenu.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon().myWeaponStats);
    }

    #region AnimStates and Cardinal Directions
    enum AnimState {
        RunUp=0,
        RunRight=1,
        RunLeft=3,
        IdleUp=4,
        IdleRight=5,
        IdleLeft=7,
        SwordMagicUp=8,
        SwordMagicRight=9,
        SwordMagicLeft=11,
        BowUp = 12,
        BowRight =13,
        BowLeft=15
    }

    
    public CardinalDirection myLastY = CardinalDirection.Up;
    public CardinalDirection myLastX = CardinalDirection.Right;
    public CardinalDirection myLastDir = CardinalDirection.Down;
    #endregion

    #region HandleMovement

    void CheckForMovementInput() {
        if (SetRunningAnimation(CardinalDirection.Up)) { }
        else if (SetRunningAnimation(CardinalDirection.Right)) { }
        else if (SetRunningAnimation(CardinalDirection.Down)) { }
        else if (SetRunningAnimation(CardinalDirection.Left)) { }
        else { HandleIdle(); }
    }

    bool SetRunningAnimation(CardinalDirection cardMoveDir) {
        if (Input.GetButton(moveDirectionsControls[cardMoveDir].inputControlString)){
            rigbod.velocity = moveDirectionsControls[cardMoveDir].moveDir * moveSpeed;

            if (!myWeaponPack.attacking) {
                if (cardMoveDir != CardinalDirection.Down) {
                    myAnimator.SetInteger("AnimState", (int)runningAnimStates[cardMoveDir]);
                }
                else {
                    if (myLastX == CardinalDirection.Right) {
                        myAnimator.SetInteger("AnimState", (int)AnimState.RunRight);
                    }
                    else {
                        myAnimator.SetInteger("AnimState", (int)AnimState.RunLeft);
                    }
                }
            }

            if (cardMoveDir == CardinalDirection.Up || cardMoveDir == CardinalDirection.Down) {
                myLastY = cardMoveDir;
            }
            else {
                myLastX = cardMoveDir;
            }
            myLastDir = cardMoveDir;
            return true;
        }
        return false;
    }

    void HandleIdle() {
        rigbod.velocity = Vector2.zero;
        if (!myWeaponPack.attacking) {
            if (myLastX == CardinalDirection.Right) {
                myAnimator.SetInteger("AnimState", (int)AnimState.IdleRight);
            }
            else if (myLastX == CardinalDirection.Left) {
                myAnimator.SetInteger("AnimState", (int)AnimState.IdleLeft);
            }
            if (myLastDir == CardinalDirection.Up) {
                myAnimator.SetInteger("AnimState", (int)AnimState.IdleUp);
            }
            else if (myLastDir == CardinalDirection.Down) {
                if (myLastX == CardinalDirection.Right) {
                    myAnimator.SetInteger("AnimState", (int)AnimState.IdleRight);
                }
                else {
                    myAnimator.SetInteger("AnimState", (int)AnimState.IdleLeft);
                }
            
            }
        }
    }

    class DirectionValues {
        public Vector2 moveDir;
        public string inputControlString;
        public DirectionValues(Vector2 moveDir, string inputControlString) {
            this.moveDir = moveDir;
            this.inputControlString = inputControlString;
        }
    }

    Dictionary<CardinalDirection, DirectionValues> moveDirectionsControls = new Dictionary<CardinalDirection, DirectionValues>() {
        {CardinalDirection.Up, new DirectionValues(Vector2.up, Controls.Up)},
        {CardinalDirection.Right, new DirectionValues(Vector2.right, Controls.Right)},
        {CardinalDirection.Down, new DirectionValues(Vector2.down, Controls.Down)},
        {CardinalDirection.Left, new DirectionValues(Vector2.left, Controls.Left)},
    };
    #endregion

    #region CollectThings
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.shrooms) {
            CollectShrooms(col.GetComponents<Shroom>());
        }
        else if (col.gameObject.layer == Layers.weaponUpgrades) {
            CollectWeapon(col.GetComponent<WeaponStats>());
        }
        else if (col.gameObject.layer == Layers.loot) {
            Debug.Log("Got LOOT!");
        }
    }

    void CollectShrooms(Shroom[] collectedShroom) {
        for (int i = 0; i < collectedShroom.Length; i++) {
            StatUpgrade shroomStatUpgrade = collectedShroom[i].GetCollected();
            myStats.Upgrade(shroomStatUpgrade);
            string displayText = "+" + shroomStatUpgrade.upgradeAmount + " " + StatClass.statStrings[shroomStatUpgrade.StatToUpgrade];
            Vector3 spawnSpot = transform.position + Vector3.up * verticaDisplayOffset * (i+1);
            DisplayRepository.Instance.CreateDisplay(Display.Damage, spawnSpot, displayText, textColor);
        }
        PauseMenu.Instance.SetStats(myStats);
        UICanvas.Instance.SetHealth(myStats);
    }

    void CollectWeapon(WeaponStats newWeaponStats) {
        myWeaponPack.weaponsCollected[newWeaponStats.weaponType] = true;
        if (myWeaponPack.weaponPack[newWeaponStats.weaponType].myWeaponStats.level < newWeaponStats.level) {
            myWeaponPack.Upgrade(newWeaponStats);
            string displayText = "+Level " + newWeaponStats.level.ToString() + " " + newWeaponStats.weaponType.ToString().ToUpper();
            Vector3 spawnSpot = transform.position + Vector3.up * verticaDisplayOffset;
            DisplayRepository.Instance.CreateDisplay(Display.Damage, spawnSpot, displayText, textColor);
        }
        newWeaponStats.GetCollected();
        PauseMenu.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon().myWeaponStats);
        UICanvas.Instance.SetWeapon(myWeaponPack.GetSelectedWeapon().myWeaponStats);
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

    float verticaDisplayOffset = 0.5f;
    Color textColor = new Color(1f, (float)((float)207/(float)255), (float)((float)46/(float)255)); //orange health bar color
    public void TakeDamage(int attack) {
        int damageTaken = Mathf.Clamp(attack - myStats.defense, 0, int.MaxValue);
        if (damageTaken > 0) {
            string displayText = "-" + damageTaken.ToString();
            Vector3 spawnSpot = transform.position + Vector3.up * verticaDisplayOffset;
            DisplayRepository.Instance.CreateDisplay(Display.Damage, spawnSpot, displayText, textColor);
            TemporaryAudioPlayer soundBox = (Instantiate(soundFXGameObject, Camera.main.transform) as GameObject).GetComponent<TemporaryAudioPlayer>();
            soundBox.SetClip(hitSFX);
        }
        myStats.currentHealth -= damageTaken;
        UICanvas.Instance.SetHealth(myStats);
        if (myStats.currentHealth <= 0) {
            lives--;
            DataSaver.Instance.PromptSave(myStats);
            DataSaver.Instance.PromptSave(myWeaponPack);
            if (lives > 0) {
                Respawner.Instance.Respawn();
            }
            else {
                PauseMenu.Instance.Quit();
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
