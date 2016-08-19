using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	[SerializeField] Transform heroTransform;
    public static CameraFollowPlayer Instance;


    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (heroTransform) {
            transform.position = heroTransform.position - Vector3.forward * 10;
        }
    }

    public void GetNewHero(Transform newHeroTransform) {
        heroTransform = newHeroTransform;
        Debug.Log(newHeroTransform);
    }
}
