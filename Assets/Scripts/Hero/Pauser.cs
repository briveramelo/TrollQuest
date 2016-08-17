using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

    public static Pauser Instance;
    bool paused = false;
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject uiCanvas;

    void Start() {
        pauseMenuCanvas = PauseMenu.Instance.gameObject;
        uiCanvas = UICanvas.Instance.gameObject;
        Instance = this;
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
        uiCanvas.SetActive(false);
        paused = !paused;
    }

    public void UnPause() {
        Time.timeScale = 1;
        pauseMenuCanvas.SetActive(false);
        uiCanvas.SetActive(true);
        paused = !paused;
    }
}
