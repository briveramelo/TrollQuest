using UnityEngine;
using System.Collections;

public class PackFollowHero : MonoBehaviour {

	[SerializeField] Transform neighborToFollow;
    [SerializeField] Hero heroScript;
    [SerializeField] WeaponPack myWeaponPack;
    
    void Awake() {

    }

	void Update () {
        bool faceRight = GetFaceRight();
        transform.position = neighborToFollow.position;
        if (!myWeaponPack.attacking){
            transform.localScale = GetLocalScale(faceRight);
        }
    }

    bool GetFaceRight() {
        if (heroScript.myLastX == CardinalDirection.Left) {
            return false;
        }
        return true;
    }

    Vector3 GetLocalScale(bool faceRight) {
        return new Vector3((faceRight ? 1 : -1), 1f, 1f);
    }
}
