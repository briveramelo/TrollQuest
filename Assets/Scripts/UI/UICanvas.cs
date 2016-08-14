using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {

    public static UICanvas Instance;
    [SerializeField] HealthBar healthBar;

    [SerializeField] SpriteRenderer weaponSprite;

    void Awake() {
        Instance = this;
    }

    public void SetHealth(int currentHealth, int maxHealth) {
        float healthFraction = ((float)currentHealth) / ((float)maxHealth);
        healthBar.ActivateHealthBars(healthFraction);
    }

    public void SetWeapon(Weapon weapon) {
        weaponSprite.sprite = WeaponSprites.Instance.GetWeaponSprite(weapon);
    }
}
