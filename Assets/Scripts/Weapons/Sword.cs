using UnityEngine;
using System.Collections;

public class Sword : Weapon {

    bool attacking = false;

    public override void Attack(){
        if (!attacking) {

        }
        Debug.Log("Attacked it");
    }

    IEnumerator Swing() {
        attacking = true;
        myCol.enabled = true;
        yield return new WaitForSeconds(.3f);
        myCol.enabled = false;
        yield return new WaitForSeconds(.2f);
        attacking = false;
    }
}
