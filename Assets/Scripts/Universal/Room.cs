using UnityEngine;
using System.Collections;

public enum GameScene {
    MainMenu=0,
    Achievements=1,
    Credits=2,
    MainWorld=3,
    Hogs=4,
    Witches=5,
    Orcs=6,
    Troll=7
}

public class Room : MonoBehaviour {

    [SerializeField] GameScene myScene;

    public int GetScene() {
        return (int)myScene;
    }
}
