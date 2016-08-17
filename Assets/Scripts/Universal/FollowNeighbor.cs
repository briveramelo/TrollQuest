using UnityEngine;
using System.Collections;

public class FollowNeighbor : MonoBehaviour {

	[SerializeField] Transform neighborToFollow;
    
	void Update () {
        transform.localPosition = neighborToFollow.localPosition;
    }
}
