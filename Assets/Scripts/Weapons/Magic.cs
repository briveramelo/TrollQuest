using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Magic : Weapon {

    enum AnimState {
        MagicIdle=0,
        Magic1Blast = 1,
        Magic2Blast = 2,
        Magic3Blast = 3,
    }

    Dictionary<int, AnimState> levelMagicBlastAnimations = new Dictionary<int, AnimState>() {
        {1,AnimState.Magic1Blast},
        {2,AnimState.Magic2Blast},
        {3,AnimState.Magic3Blast}
    };

    public override IEnumerator Attack(int wielderAttack, CardinalDirection attackDir) {
        myAnimator.SetInteger("AnimState", (int)levelMagicBlastAnimations[myWeaponStats.level]);
        yield return StartCoroutine(base.Attack(wielderAttack, attackDir));
        myAnimator.SetInteger("AnimState", (int)AnimState.MagicIdle);
    }

}
