using UnityEngine;
using System.Collections;

public class SwordFollowNeighbor : MonoBehaviour {

	[SerializeField] Transform neighborToFollow;
    [SerializeField] Vector3 leftStartingPosition;
    [SerializeField] Vector3 rightStartingPosition;
    [SerializeField] Hero heroScript;
    
    void Awake() {
        rightStartingPosition = transform.localPosition;
    }

    Vector3 lastNeighborPosition;
	void Update () {
        bool faceRight = GetFaceRight();
        Vector3 correctedPosition = (faceRight ? rightStartingPosition : leftStartingPosition);
        transform.localPosition = correctedPosition + neighborToFollow.localPosition;
        transform.localScale = GetLocalScale(faceRight);
        lastNeighborPosition = transform.localPosition;
    }

    bool GetFaceRight() {
        if (heroScript.myLastDir == CardinalDirection.Left) {
            return false;
        }
        return true;
    }

    Vector3 GetLocalScale(bool faceRight) {
        return new Vector3((faceRight ? 1 : -1), 1f, 1f);
    }
}
