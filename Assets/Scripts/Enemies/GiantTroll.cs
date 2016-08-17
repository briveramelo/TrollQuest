using UnityEngine;
using System.Collections;

public class GiantTroll : MonoBehaviour {

    public string[] speechLines;
    [SerializeField] Collider2D myCol;
    [SerializeField] Sprite myFaceSprite;

    public void SpeakNextLine(int speechIndex) {
        DialogueBox.Instance.Activate();
        DialogueBox.Instance.InsertNewText(speechLines[speechIndex]);
        //DialogueBox.Instance.SetFace(myFaceSprite);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == Layers.player) {
            SpeakNextLine(0);
            Destroy(myCol);
        }
    }

    void OnDestroy() {
        if (DialogueBox.Instance) {
            DialogueBox.Instance.DeActivate();
        }
    }
}
