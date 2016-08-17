using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour {

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sensingDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spacingDistance);
    }

    [SerializeField] Animator myAnimator;
    float sensingDistance;
    public float spacingDistance = 3f;
    public float moveSpeed = 3f;
    public float launchSpeed = 2f;
    public float shotPeriod = 1.5f;
    bool attacking = false;
    [SerializeField] Rigidbody2D rigbod;
    [SerializeField] CircleCollider2D myCirCol;
    [SerializeField] GameObject witchBlast;
    [SerializeField] Stats myStats;


    enum AnimState {
        Idle=0,
        Approach=1,
        Cast=2
    }
	// Use this for initialization
	void Start () {
        sensingDistance = myCirCol.radius * transform.lossyScale.x;
    }

    float distanceAway;
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.layer == Layers.player) {
            distanceAway = Vector3.Distance(col.gameObject.transform.position, transform.position);
            if (distanceAway < spacingDistance) {
                MoveTowardYou(col.gameObject.transform.position, false);
            }
            else {
                MoveTowardYou(col.gameObject.transform.position, true);
            }
            if (!attacking) {
                Vector3 attackVec = (col.gameObject.transform.position - transform.position).normalized * launchSpeed;
                StartCoroutine(Attack(attackVec));
            }
        }
    }

    IEnumerator Attack(Vector3 attackVec) {
        attacking = true;
        Projectile myBlast = (Instantiate(witchBlast, transform.position, Quaternion.identity) as GameObject).GetComponent<Projectile>();
        myBlast.Launch(attackVec, myStats.attack);
        myAnimator.SetInteger("AnimState", (int)AnimState.Cast);
        yield return new WaitForSeconds(shotPeriod);
        attacking = false;
    }


    void Update() {
        if (distanceAway > sensingDistance) {
            Wander();
        }
    }

    float moveFloat;
    void MoveTowardYou(Vector3 heroPosition, bool moveToward) {
        moveFloat = Mathf.MoveTowards(moveFloat, moveToward ? .6f : -.8f, moveToward ? 0.006f: 0.0275f);
        Vector3 moveDir = moveFloat * (heroPosition - transform.position).normalized;
        rigbod.velocity = moveDir * moveSpeed;
        FaceForward(transform.position.x-heroPosition.x < 0);
        myAnimator.SetInteger("AnimState", (int)AnimState.Idle);
    }

    public float wanderSpeed;
    void Wander() {
        Vector2 targetMove = Vector2.ClampMagnitude(wanderSpeed * new Vector2(GetNewDirection(ref x), GetNewDirection(ref y)), wanderSpeed);
        rigbod.velocity = Vector2.MoveTowards(rigbod.velocity, targetMove, 0.5f);
        FaceForward(rigbod.velocity.x > 0);
        myAnimator.SetInteger("AnimState", (int)AnimState.Idle);
    }

    void FaceForward(bool forward) {
        transform.localScale = new Vector3(forward ? 1f:-1f, 1f, 1f);
    }

    [SerializeField] DirectionProperties x = new DirectionProperties(true);
    [SerializeField] DirectionProperties y = new DirectionProperties(false);

    float GetNewDirection(ref DirectionProperties direction) {
        if (!direction.waitingOn) {
            if (direction.isX) {
                StartCoroutine(HoldForX());
            }
            else {
                StartCoroutine(HoldForY());
            }
        }
        direction.current = Mathf.MoveTowards(direction.current, direction.target, 0.02f);
        float timing = Time.time / x.period;
        return Mathf.Cos(timing);
    }

    IEnumerator HoldForX() {
        x.waitingOn = true;
        x.period = Random.Range(4, 7);
        x.target = Random.Range(1, x.period);
        yield return new WaitForSeconds(x.period);
        x.waitingOn = false;
    }

    IEnumerator HoldForY() {
        y.waitingOn = true;
        y.period = Random.Range(4, 7);
        y.target = Random.Range(1, y.period);
        yield return new WaitForSeconds(y.period);
        y.waitingOn = false;
    }
}
