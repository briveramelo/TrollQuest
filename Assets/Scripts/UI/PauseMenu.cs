using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static PauseMenu Instance;
    [SerializeField] GameObject QuitConfirmationWindow;
    [SerializeField] GameObject CloseConfirmationWindow;

    [SerializeField] Image weaponImage;
    [SerializeField] Text weaponLevelText;
    [SerializeField] Text weaponAttackText;
    [SerializeField] Text attackText;
    [SerializeField] Text defenseText;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }

    public void SetStats(Stats playerStats) {
        attackText.text = playerStats.attack.ToString();
        defenseText.text = playerStats.defense.ToString();
    }

    public void SetWeapon(WeaponStats weaponStats) {
        weaponLevelText.text = "Lvl. " + weaponStats.level.ToString();
        weaponAttackText.text = "Atk +" + weaponStats.attack.ToString();
        //weaponImage.sprite = WeaponSprites.Instance.GetWeaponSprite(weapon);
    }

    public void Continue() {
        Pauser.Instance.UnPause();
    }

    public void Quit() {
        Pauser.Instance.UnPause();
        SceneManager.LoadScene((int)GameScene.MainMenu);
    }

    public void DisplayQuitConfirmation() {
        QuitConfirmationWindow.SetActive(true);
    }

    public void HideQuiteConfirmation() {
        QuitConfirmationWindow.SetActive(false);
    }

    public void Close() {
        Application.Quit();
    }

    public void DisplayCloseConfirmation() {
        CloseConfirmationWindow.SetActive(true);
    }

    public void HideCloseConfirmation() {
        CloseConfirmationWindow.SetActive(false);
    }


}
