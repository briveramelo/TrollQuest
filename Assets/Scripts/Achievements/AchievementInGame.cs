using UnityEngine;
using System.Collections;

public class AchievementInGame : MonoBehaviour {

    public static AchievementInGame Instance;
    [SerializeField] GameObject achievementObject;
    [SerializeField] TextMesh achievementTextMesh;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void DisplayAchievement(string achievementText) {
        StartCoroutine(DisplayAchievementTime(achievementText));
    }

    IEnumerator DisplayAchievementTime(string achievementText) {
        achievementObject.SetActive(true);
        achievementTextMesh.text = achievementText;
        yield return new WaitForSeconds(4f);
        achievementObject.SetActive(false);
    }
}
