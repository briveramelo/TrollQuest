using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void ReturnToMainMenu() {
        SceneManager.LoadScene((int)GameScene.MainMenu);
    }

    public void StartNewGame() {
        Hero.lives = 3;
        SceneManager.LoadScene((int)GameScene.MainWorld);
    }

    public void ViewCredits() {
        SceneManager.LoadScene((int)GameScene.Credits);
    }

    public void ViewAchievements() {
        SceneManager.LoadScene((int)GameScene.Achievements);
    }

    public void CloseGame() {
        Application.Quit();
    }
}
