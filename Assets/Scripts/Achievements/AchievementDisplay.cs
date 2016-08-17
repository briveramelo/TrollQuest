using UnityEngine;
using System.Collections.Generic;

public class AchievementDisplay : MonoBehaviour {

    public GameObject[] textGameObjects;
    Dictionary<Achievement, string> achievementWords = new Dictionary<Achievement, string>() {
        {Achievement.KillTheTroll , "Kill The Troll"},
        {Achievement.FindThePond , "Find The Pond"},
        {Achievement.DoTheThing , "Do The Thing"},
    };
    Dictionary<Achievement, bool> completeAchievements;
    DataSave copiedDataSave;
    Dictionary<Achievement, GameObject> achievementTextGameObjects;

    void Start() {
        copiedDataSave = DataSaver.Instance.CopyCurrentDataSave();
        completeAchievements = copiedDataSave.completeAchievements;
        achievementTextGameObjects = new Dictionary<Achievement, GameObject>() {
            {Achievement.KillTheTroll , textGameObjects[0]},
            {Achievement.FindThePond , textGameObjects[1]},
            {Achievement.DoTheThing , textGameObjects[2]},
        };
        DisplayAchievements();
    }

    void DisplayAchievements() {
        foreach (KeyValuePair<Achievement, bool> achievementEntry in completeAchievements) {
            bool completed = achievementEntry.Value ? true : false;
            //achievementTextGameObjects[achievementEntry.Key].SetActive(completed);
            string textToDisplay = completed ? achievementWords[achievementEntry.Key] : "???";
            achievementTextGameObjects[achievementEntry.Key].GetComponent<UnityEngine.UI.Text>().text = textToDisplay;
        }
    }

}
