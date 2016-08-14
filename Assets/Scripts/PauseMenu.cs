using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static PauseMenu Instance;
    [SerializeField] GameObject ConfirmationWindow;

    [SerializeField] Image weaponImage;
    [SerializeField] Text weaponLevelText;
    [SerializeField] Text weaponAttackText;
    [SerializeField] Text attackText;
    [SerializeField] Text defenseText;

    void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetStats(Stats playerStats) {
        attackText.text = playerStats.attack.ToString();
        defenseText.text = playerStats.defense.ToString();
    }

    public void SetWeapon(Weapon weapon) {
        weaponLevelText.text = "Lvl. " + weapon.level.ToString();
        weaponAttackText.text = "Atk +" + weapon.attack.ToString();
        weaponImage.sprite = WeaponSprites.Instance.GetWeaponSprite(weapon);
    }


    public void Continue() {
        Pauser.Instance.UnPause();
    }

    public void DisplayQuitConfirmation() {
        ConfirmationWindow.SetActive(true);
    }

    public void HideQuiteConfirmation() {
        ConfirmationWindow.SetActive(false);
    }

    public void Quit() {
        SceneManager.LoadScene((int)GameScene.MainMenu);
    }
}
