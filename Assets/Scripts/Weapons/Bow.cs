using UnityEngine;
using System.Collections;
using System;

public class Bow : Weapon {
    public override IEnumerator Attack(int wielderAttack) {
        yield return null;
    }
}
