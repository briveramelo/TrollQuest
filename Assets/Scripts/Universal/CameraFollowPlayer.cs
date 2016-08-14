using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	[SerializeField] Transform heroTransform;
    public static CameraFollowPlayer Instance;


    void Awake() {
        Instance = this;
    }

    void Update() {
        if (heroTransform) {
            transform.position = heroTransform.position - Vector3.forward * 10;
        }
    }

    public void getNewHero(Transform newHeroTransform) {
        heroTransform = newHeroTransform;
    }
}
