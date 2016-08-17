using UnityEngine;
using System.Collections.Generic;

public enum Achievement {
    KillTheTroll = 0,
    FindThePond = 1,
    DoTheThing = 2
}

public class AchievementSensor : MonoBehaviour {

    public static AchievementSensor Instance;
    Dictionary<Achievement, bool> completeAchievements = new Dictionary<Achievement, bool>();
    void Start() {
        Instance = this;
        DataSave copiedDataSave = DataSaver.Instance.CopyCurrentDataSave();
        completeAchievements = copiedDataSave.completeAchievements;
    }

    public void EarnAchievement(Achievement earnedAchievement) {
        completeAchievements[earnedAchievement] = true;
        DataSaver.Instance.PromptSave(completeAchievements);
    }
}
