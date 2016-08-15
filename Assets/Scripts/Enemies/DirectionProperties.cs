using UnityEngine;
using System.Collections;

public class DirectionProperties : MonoBehaviour {

    public float current=1;
    public float target =1;
    public float period = 7;
    public bool waitingOn;
    public bool isX;
    public DirectionProperties(bool isX) { this.isX = isX; }
}
