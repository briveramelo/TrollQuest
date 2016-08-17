using UnityEngine;
using UnityEngine.EventSystems;

public class EventChecker : MonoBehaviour {

    void OnEnable() {
        if (!FindObjectOfType<EventSystem>()) {
            gameObject.AddComponent<StandaloneInputModule>();
        }
    }
}
