using UnityEngine;
using System.Collections;

public class GiantTroll : MonoBehaviour {

    public string[] speechLines;
    [SerializeField] Collider2D myCol;

    public void SpeakNextLine(int speechIndex) {
        DialogueBox.Instance.Activate();
        DialogueBox.Instance.InsertNewText(speechLines[speechIndex]);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.player) {
            SpeakNextLine(0);
            Destroy(myCol);
        }
    }
}
