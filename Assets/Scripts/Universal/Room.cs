using UnityEngine;
using System.Collections;

public enum GameScene {
    MainMenu=0,
    Achievements=1,
    Credits=2,
    MainWorld=3,
    Tree=4,
}

public class Room : MonoBehaviour {

    [SerializeField] GameScene myScene;

    public int GetScene() {
        return (int)myScene;
    }
}
