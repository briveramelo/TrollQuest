using UnityEngine;
using System.Collections;

public class DialogueBox : MonoBehaviour {

    public static DialogueBox Instance;
    [SerializeField] TextMesh[] myTextMeshes;
    [SerializeField] SpriteRenderer speakerSpriteRenderer;
    string[] textLines;
    public int[] maxCharacters = new int[] { 26, 32, 32 };

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        textLines = new string[myTextMeshes.Length];
    }

    public void DeActivate() {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Activate() {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SetFace(Sprite speakerSprite) {
        speakerSpriteRenderer.sprite = speakerSprite;
    }

    public void InsertNewText(string newText) {
        int sum = 0;
        for (int i = 0; i < maxCharacters.Length; i++) {
            sum += maxCharacters[i];
        }

        if (newText.Length > sum) {
            Debug.LogError("Text is too long");
            return;
        }
        for (int i = 0; i < myTextMeshes.Length; i++) {

            myTextMeshes[i].text = "";
            textLines[i] = GetCharactersOnLine(newText, i);
            int startPoint = textLines[i].Length;
            int length = newText.Length - startPoint;
            if (length > 0) {
                newText = newText.Substring(startPoint, length).Trim();
            }
            else {
                newText = "";
            }
        }
        StopAllCoroutines();
        StartCoroutine (PrintAllLines());
    }

    IEnumerator PrintAllLines() {
        for (int i = 0; i < textLines.Length; i++) {
            yield return StartCoroutine(TickerPrintText(textLines[i], i));
        }
        yield return new WaitForSeconds(3f);
        DeActivate();
    }

    public float lettersPerSecond;
    public float pauseForPeriod;
    public float pauseForComma;
    IEnumerator TickerPrintText(string newText, int textLine) {
        myTextMeshes[textLine].text = "";
        for (int i = 0; i < newText.Length; i++) {
            string nextChar = newText[i].ToString();
            myTextMeshes[textLine].text += nextChar;
            if (nextChar == "." || nextChar == "!" || nextChar == "?") {
                yield return new WaitForSeconds(pauseForPeriod);
            }
            else if (nextChar == ",") {
                yield return new WaitForSeconds(pauseForComma);
            }
            else {
                yield return new WaitForSeconds(1 / lettersPerSecond);
            }
        }
    }

    string GetCharactersOnLine(string newText, int lineNumber) {
        int thisMaxCharacters = maxCharacters[lineNumber];
        int stringLength = (thisMaxCharacters < newText.Length) ? thisMaxCharacters : newText.Length;
        for (int i = stringLength - 1; i >-1; i--) {
            if (newText[i].ToString() == " " && i!=0) {
                string characters = (newText.Substring(0, i)).Trim();
                return characters;
            }
        }
        return newText;
    }
}
