using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {

    public static UICanvas Instance;
    [SerializeField] HealthBar healthBar;

    [SerializeField] SpriteRenderer weaponSprite;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SetHealth(Stats stats) {
        float healthFraction = ((float)stats.currentHealth) / ((float)stats.health);
        healthBar.ActivateHealthBars(healthFraction);
    }

    public void SetWeapon(WeaponStats weaponStats) {
        weaponSprite.sprite = WeaponSprites.Instance.GetWeaponSprite(weaponStats);
    }
}
