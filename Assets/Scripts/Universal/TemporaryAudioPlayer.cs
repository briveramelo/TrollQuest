using UnityEngine;
using System.Collections;

public class TemporaryAudioPlayer : MonoBehaviour {

    [SerializeField] AudioSource mySoundBox;
    [SerializeField] float timeToDestroy;

    public void SetClip(AudioClip myClip) {
        mySoundBox.clip = myClip;
        mySoundBox.Play();
    }
    void Awake() {
        Destroy(gameObject, timeToDestroy);
    }

}
