using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

    public static Pauser Instance;
    bool paused = false;
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject uiCanvas;
    [SerializeField] GameObject eventSystem;

    void Start() {
        pauseMenuCanvas = PauseMenu.Instance.gameObject;
        uiCanvas = UICanvas.Instance.gameObject;
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetButtonDown(Controls.Pause) || Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                Pause();
            }
            else {
                UnPause();
            }
        }
    }

    public void Pause() {
        Time.timeScale = 0;
        pauseMenuCanvas.SetActive(true);
        eventSystem.SetActive(true);
        uiCanvas.SetActive(false);
        paused = !paused;
    }

    public void UnPause() {
        Time.timeScale = 1;
        pauseMenuCanvas.SetActive(false);
        eventSystem.SetActive(false);
        uiCanvas.SetActive(true);
        paused = !paused;
    }
}
