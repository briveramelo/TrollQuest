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
    [SerializeField] Animator myAnimator;
    public bool isOpen;

    void Awake() {
        AnimState animState = isOpen ? AnimState.Open : AnimState.Closed;
        myAnimator.SetInteger("AnimState", (int)animState);
    }

    enum AnimState {
        Closed=0,
        Open=1
    }

    public int GetScene() {
        return (int)myScene;
    }

    public void OpenDoor() {
        isOpen = true;
        myAnimator.SetInteger("AnimState", (int)AnimState.Open);
    }
}
