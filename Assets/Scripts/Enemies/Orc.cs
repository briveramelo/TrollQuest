using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orc : MonoBehaviour {

    [SerializeField] Rigidbody2D rigbod;
    [SerializeField] Animator myAnimator;
    [SerializeField] Collider2D myAttackingCol;
    [SerializeField] EnemyWeapon myEnemyWeapon;
    [SerializeField] Stats myStats;

    public Vector3[] attackPositions;
    public bool attacking;
    public float swingDistance;
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        for (int i = 0; i < attackPositions.Length; i++) {
            Gizmos.DrawWireSphere(transform.position + attackPositions[i], ((CircleCollider2D)myAttackingCol).radius * transform.lossyScale.x);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, swingDistance);
    }

    void Awake() {
        StartCoroutine(TravelToNextSquare());
    }


    Dictionary<CardinalDirection, Vector2> moveDirs = new Dictionary<CardinalDirection, Vector2>() {
        {CardinalDirection.Up, Vector2.up },
        {CardinalDirection.Right, Vector2.right },
        {CardinalDirection.Down, Vector2.down },
        {CardinalDirection.Left, Vector2.left }
    };
    
    Dictionary<CardinalDirection, AnimState> cardDirAnimStates = new Dictionary<CardinalDirection, AnimState>() {
        {CardinalDirection.Up, AnimState.RunningUp},
        {CardinalDirection.Right, AnimState.RunningDown },
        {CardinalDirection.Down, AnimState.RunningDown },
        {CardinalDirection.Left, AnimState.RunningDown }
    };
    Dictionary<CardinalDirection, AnimState> attackingAnimStates = new Dictionary<CardinalDirection, AnimState>() {
        {CardinalDirection.Up, AnimState.AttackingUp},
        {CardinalDirection.Right, AnimState.AttackingDown },
        {CardinalDirection.Down, AnimState.AttackingDown},
        {CardinalDirection.Left, AnimState.AttackingDown }
    };
    Dictionary<CardinalDirection, AnimState> cockingAnimStates = new Dictionary<CardinalDirection, AnimState>() {
        {CardinalDirection.Up, AnimState.CockingUp},
        {CardinalDirection.Down, AnimState.CockingDown },
        {CardinalDirection.Left, AnimState.CockingDown},
        {CardinalDirection.Right, AnimState.CockingDown }
    };
    public float moveSpeed;
    public float breathTime;
    public float moveTime;
    IEnumerator TravelToNextSquare() {
        CardinalDirection newDirection = (CardinalDirection)Random.Range(0, 4);
        Vector2 moveDir = moveDirs[newDirection];
        rigbod.velocity = moveDir * moveSpeed;
        SetRunningAnimation(rigbod.velocity.y>0);
        yield return new WaitForSeconds(moveTime);
        rigbod.velocity = Vector2.zero;
        myAnimator.SetInteger("AnimState", (int)AnimState.Idle);
        yield return new WaitForSeconds(breathTime);
        StartCoroutine(TravelToNextSquare());
    }

    public float cockTime;
    public float attackTime;
    public float hangTime;
    IEnumerator Attack(CardinalDirection attackDir) {
        attacking = true;
        myEnemyWeapon.SetAttack(myStats.attack);
        bool goUp = attackDir == CardinalDirection.Up ? true:false;
        SetCockingAnimation(goUp);
        
        int attackIndex = !goUp ? 0 :1;
        rigbod.velocity = Vector2.zero;
        yield return new WaitForSeconds(cockTime);
        myAttackingCol.enabled = true;
        myAttackingCol.offset = attackPositions[attackIndex] * (1/transform.lossyScale.x);
        SetAttackingAnimation(goUp);
        yield return new WaitForSeconds(attackTime);
        myAttackingCol.enabled = false;
        yield return new WaitForSeconds(hangTime);
        attacking = false;
        StartCoroutine (TravelToNextSquare());
    }

    void SetCockingAnimation(bool goUp) {
        AnimState myCockingState;
        if (goUp) {
            myCockingState = AnimState.CockingUp;
        }
        else {
            myCockingState = AnimState.CockingDown;
        }
        myAnimator.SetInteger("AnimState", (int)myCockingState);
    }
    void SetAttackingAnimation(bool goUp) {
        AnimState myAttackingState;
        if (goUp) {
            myAttackingState = AnimState.AttackingUp;
        }
        else {
            myAttackingState = AnimState.AttackingDown;
        }
        myAnimator.SetInteger("AnimState", (int)myAttackingState);
    }
    void SetRunningAnimation(bool goUp) {
        AnimState myRunningState;
        if (goUp) {
            myRunningState = AnimState.RunningUp;
        }
        else {
            myRunningState = AnimState.RunningDown;
        }
        myAnimator.SetInteger("AnimState", (int)myRunningState);
    }


    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.layer == Layers.player && !approaching && !attacking) {
            StopAllCoroutines();
            StartCoroutine(Approach(col.transform));
        }
    }

    public bool approaching;
    public float approachUnitDistance;
    public float approachSpeed;
    
    IEnumerator Approach(Transform playerTransform) {
        approaching = true;
        Vector3 moveDir = GetMoveDir(playerTransform.position);

        rigbod.velocity = moveDir.normalized* approachSpeed;
        Vector3 target = moveDir + transform.position;
        while (true) {
            if (playerTransform) {
                if (Vector3.Distance(playerTransform.position, transform.position) < swingDistance) {
                    CardinalDirection movementCardDir = GetUpDown(playerTransform.position);
                    StartCoroutine(Attack(movementCardDir));
                    break;
                }
                else if (Vector3.Distance(transform.position, target) < 0.2f) {
                    StartCoroutine(TravelToNextSquare());
                    break;
                }
                yield return null;
            }
            else {
                break;
            }

        }
        approaching = false;
        yield return null;
    }

    Vector3 GetMoveDir(Vector3 playerPosition) {
        Vector3 separation = playerPosition - transform.position;
        bool goX = Mathf.Abs(separation.x) > Mathf.Abs(separation.y);
        bool goPos = goX ? separation.x > 0 : separation.y > 0;
        int sign = goPos ? 1 : -1;
        return (goX ? new Vector3(sign, 0f, 0f) : new Vector3(0f, sign, 0f)).normalized;
    }

    CardinalDirection GetUpDown(Vector3 playerPosition) {
        Vector3 attackingColPosition1 = transform.position + attackPositions[0] * (1 / transform.lossyScale.x);
        Vector3 attackingColPosition2 = transform.position + attackPositions[1] * (1 / transform.lossyScale.x);
        if (Vector3.Distance(playerPosition, attackingColPosition1) < Vector3.Distance(playerPosition, attackingColPosition2)) {
            return CardinalDirection.Down;
        }
        return CardinalDirection.Up;
    }
    
    enum AnimState {
        RunningDown=0,
        RunningUp=1,
        CockingDown=2,
        AttackingDown=3,
        CockingUp=4,
        AttackingUp = 5,
        Idle=6
    }
}
