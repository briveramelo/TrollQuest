using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	[SerializeField] GameObject healthBarParent;
    GameObject[] healthBars;
    public bool reset;
    public float spacing;
    public int healthBarCount;

    void Awake() {
        healthBarCount = healthBarParent.transform.childCount;
        healthBars = new GameObject[healthBarCount];
        DefineHealthBars();
        ResetPosition();
    }

    void DefineHealthBars() {
        int i = 0;
        foreach (Transform childTran in healthBarParent.transform) {
            if (i<healthBarCount) {
                healthBars[i] = childTran.gameObject;
            }
            i++;
        }
    }

    public void ActivateHealthBars(float healthFraction) {
        for (int i = 0; i < healthBarCount; i++){
            float currentIndexFraction = ((float)i) / ((float)healthBarCount);
            if (currentIndexFraction < healthFraction){
                healthBars[i].SetActive(true);
            }
            else {
                healthBars[i].SetActive(false);
            }
        }
    }

    void ResetPosition() {
        int i = 0;
        Vector3 startPosition = Vector3.zero;
        foreach (Transform childTran in healthBarParent.transform) {
            if (i == 0) {
                startPosition = childTran.localPosition;
            }
            else if (i<healthBarCount) {
                healthBars[i].transform.localPosition = startPosition + Vector3.right * i * spacing;
            }
            i++;
        }
    }

    void Update() {
        if (reset) {
            reset = false;
            ResetPosition();
        }
    }
}
