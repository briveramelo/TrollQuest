using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum DiagonalDirections {
    Null=-1,
    UpRight =0,
    DownRight=1,
    DownLeft=2,
    UpLeft=3
}

public class Hog : MonoBehaviour {

    [SerializeField] Rigidbody2D rigbod;
    [SerializeField] Collider2D myAttackingCol;
    [SerializeField] Animator myAnimator;
    [SerializeField] Stats myStats;
    [SerializeField] EnemyWeapon myEnemyWeapon;

    public float moveSpeed;
    public float approachSpeed;
    public float chargeSpeed;
    public bool charging;
    public float relX;
    public float relY;
    public float wanderUnitDistance;
    public float overShootDistance;
    public float breatherTime;
    Vector3[] targets;
    void Awake() {
        targets = new Vector3[] {
            new Vector3(-relX, relY,0f),
            new Vector3(relX, 2*relY,0f),
            new Vector3(-relX, 3*relY,0f)
        };
        StartCoroutine(Wander());
    }

    Dictionary<DiagonalDirections, Vector2> moveDirections = new Dictionary<DiagonalDirections, Vector2>() {
        { DiagonalDirections.UpRight, Vector2.one.normalized},
        { DiagonalDirections.DownRight, new Vector2(1,-1).normalized},
        { DiagonalDirections.DownLeft, -Vector2.one.normalized},
        { DiagonalDirections.UpLeft, new Vector2(-1, 1).normalized},
    };

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.layer == Layers.player && !charging) {
            StopAllCoroutines();
            StartCoroutine(Charge(col.gameObject.transform));
        }
    }

    DiagonalDirections lastDirection = DiagonalDirections.Null;
    IEnumerator Wander() {
        DiagonalDirections newDirection = (DiagonalDirections)Random.Range(0, 3);
        while (newDirection == lastDirection) {
            newDirection = (DiagonalDirections)Random.Range(0, 3);
        }
        yield return StartCoroutine(WanderInNextDirection(newDirection));
        lastDirection = newDirection;
        StartCoroutine(Wander());
    }

    IEnumerator Charge(Transform targetPlayer) {
        charging = true;
        myEnemyWeapon.SetAttack(myStats.attack);
        myAttackingCol.enabled = true;
        myAnimator.SetInteger("AnimState", (int)AnimState.Running);
        int timesToCross = Random.Range(3, 5);

        for (int i = 0; i < timesToCross; i++) {
            if (targetPlayer) {
                bool final = i == (timesToCross - 1);
                Vector3 target = (!final ? GetTarget(targetPlayer, i) : GetDirectChargeTarget(targetPlayer.position));
                yield return StartCoroutine(ChargeInNextDirection(target, final));
            }
            else {
                break;
            }
        }
        myAttackingCol.enabled = false;
        yield return StartCoroutine(TakeABreather());
        charging = false;
    }

    IEnumerator TakeABreather() {
        rigbod.velocity = Vector2.zero;
        myAnimator.SetInteger("AnimState", (int)AnimState.Idle);
        yield return new WaitForSeconds(breatherTime);
        StartCoroutine(Wander());
    }

    enum AnimState {
        Wandering=0,
        Running=1,
        Idle=2
    }

    IEnumerator WanderInNextDirection(DiagonalDirections nextDirection) {
        rigbod.velocity = moveDirections[nextDirection] * moveSpeed;
        FaceForward(rigbod.velocity.x > 0);
        myAnimator.SetInteger("AnimState", (int)AnimState.Wandering);
        Vector3 target = transform.position + (Vector3)moveDirections[nextDirection] * wanderUnitDistance;
        while (Vector3.Distance(transform.position, target) > 0.2f) {
            yield return null;
        }
    }

    IEnumerator ChargeInNextDirection(Vector3 target, bool isFinal) {
        rigbod.velocity = (target - transform.position).normalized * (isFinal ? chargeSpeed : approachSpeed);
        FaceForward(rigbod.velocity.x > 0);
        while (Vector3.Distance(transform.position, target) > 0.2f) {
            yield return null;
        }
    }

    Vector3 GetTarget(Transform playerTransform, int chargeIndex) {
        float zRotation = GetZRotation(playerTransform);
        Quaternion toRotation = Quaternion.Euler(0f, 0f, zRotation);
        transform.rotation = toRotation;
        Vector3 relXAxis = targets[chargeIndex].x * transform.right;
        Vector3 relYAxis = targets[chargeIndex].y * transform.up;
        Vector3 offset = relXAxis + relYAxis;
        Vector3 target = transform.position + offset;
        transform.rotation = Quaternion.identity;
        return target;
    }

    float GetZRotation(Transform playerTransform) {
        Vector3 lookDirection = playerTransform.position - transform.position;
        float angle = Vector3.Angle(transform.up, lookDirection);
        if (GetDistance(playerTransform, angle) < GetDistance(playerTransform, -angle)) {
            return angle;
        }
        return -angle;
    }

    float GetDistance(Transform playerTransform, float zRotation) {
        Quaternion toRotation = Quaternion.Euler(0f, 0f, zRotation);
        transform.rotation = toRotation;
        Vector3 optionA = transform.position + transform.up;
        return Vector3.Distance(optionA, playerTransform.position);
    }

    Vector3 GetDirectChargeTarget(Vector3 playerPosition) {
        Vector3 moveDir = (playerPosition - transform.position).normalized * overShootDistance;
        Vector3 target = playerPosition + moveDir;
        return target;
    }

    void FaceForward(bool forward) {
        transform.localScale = new Vector3(forward?1:-1, 1f, 1f);
    }
}
