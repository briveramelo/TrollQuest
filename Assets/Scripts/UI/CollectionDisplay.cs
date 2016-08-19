using UnityEngine;
using System.Collections;

public class CollectionDisplay : MonoBehaviour {

    public float viewTime;
    [SerializeField] TextMesh myTextMesh;
    [SerializeField] MeshRenderer myMeshRenderer;
    [SerializeField] Rigidbody2D rigbod;

    public void SetText(string newText, Color textColor) {
        myTextMesh.text = newText;
        myTextMesh.color = textColor;
        if (true) { }
    }

    IEnumerator Start() {
        myMeshRenderer.sortingOrder = 2;
        rigbod.velocity = Vector2.up * .5f;
        yield return new WaitForSeconds(viewTime);
        Destroy(gameObject);
    }
}
