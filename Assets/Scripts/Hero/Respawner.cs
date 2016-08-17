using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

    public static Respawner Instance;
    [SerializeField] Transform respawnTransform;

    [SerializeField] GameObject hero;

    void Start() {
        Instance = this;
    }

    public void Respawn() {
        StartCoroutine(RespawnHero());
    }

    IEnumerator RespawnHero() {
        yield return new WaitForSeconds(2f);
        Instantiate(hero, respawnTransform);
    }

}
