using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bow : Weapon {

    [SerializeField] GameObject arrow;

    Dictionary<CardinalDirection, Vector2> cardMoveDir = new Dictionary<CardinalDirection, Vector2>() {
        {CardinalDirection.Up,Vector2.up},
        {CardinalDirection.Right,Vector2.right},
        {CardinalDirection.Down,Vector2.down},
        {CardinalDirection.Left,Vector2.left}
    };

    Dictionary<int, AnimState> levelCockingStates = new Dictionary<int, AnimState>() {
        {1,AnimState.Bow1Cocking},
        {2,AnimState.Bow2Cocking},
        {3,AnimState.Bow3Cocking}
    };
    Dictionary<int, AnimState> levelReleasedStates = new Dictionary<int, AnimState>() {
        {1,AnimState.Bow1Released},
        {2,AnimState.Bow2Released},
        {3,AnimState.Bow3Released}
    };

    enum AnimState {
        Bow1Cocking=0,
        Bow1Released=1,
        Bow2Cocking = 2,
        Bow2Released = 3,
        Bow3Cocking = 4,
        Bow3Released = 5,
    }

    public float shootSpeed = 7f;
    public override IEnumerator Attack(int wielderAttack, CardinalDirection attackDir) {
        Arrow myArrow = (Instantiate(arrow, transform.position, Quaternion.identity) as GameObject).GetComponent<Arrow>();
        myAnimator.SetInteger("AnimState", (int)levelCockingStates[myWeaponStats.level]);
        myArrow.SetArrowType(myWeaponStats.level-1);
        int attack = myWeaponStats.attack + wielderAttack;
        myArrow.Launch(cardMoveDir[attackDir] * shootSpeed, attack);
        myArrow.SetOrientation(attackDir);

        thisSwingsHits = new List<Collider2D>();
        this.wielderAttack = wielderAttack;
        yield return new WaitForSeconds((1f / (float)myWeaponStats.strikesPerSecond) * myWeaponStats.percentTimeOfDanger);
        myAnimator.SetInteger("AnimState", (int)levelReleasedStates[myWeaponStats.level]);
        yield return new WaitForSeconds((1f / (float)myWeaponStats.strikesPerSecond) * (1f - myWeaponStats.percentTimeOfDanger));
    }
}
