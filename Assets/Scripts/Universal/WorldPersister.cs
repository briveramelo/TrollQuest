using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldPersister : MonoBehaviour {

    public static WorldPersister Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void OnLevelWasLoaded(int level) {
        bool destroyThis =
            level == (int)GameScene.Achievements ||
            level == (int)GameScene.Credits ||
            level == (int)GameScene.MainMenu;
        if (destroyThis) {
            Destroy(gameObject);
        }
    }
}
