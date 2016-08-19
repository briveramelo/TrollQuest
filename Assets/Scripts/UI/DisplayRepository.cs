using UnityEngine;
using System.Collections;

public enum Display {
    Stats = 0,
    Achievement = 1,
    Damage = 2
}

public class DisplayRepository : MonoBehaviour {

    public static DisplayRepository Instance;
    [SerializeField] GameObject[] displays;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void CreateDisplay(Display displayChoice, Vector3 spawnPoint, string textDisplay, Color textColor) {
        CollectionDisplay myDisplay = (Instantiate(displays[0], spawnPoint, Quaternion.identity) as GameObject).GetComponent<CollectionDisplay>();
        myDisplay.SetText(textDisplay, textColor);
    }

    
}
