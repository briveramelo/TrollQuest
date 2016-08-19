using UnityEngine;
using System.Collections.Generic;

public class MagicBlast : Projectile {

	[SerializeField] Animator myAnimator;

    public void SetMagicType(int magicLevel) {
        myAnimator.SetInteger("AnimState", magicLevel);
    }

    Dictionary<CardinalDirection, float> magicZRotation = new Dictionary<CardinalDirection, float>() {
        {CardinalDirection.Up, 90f},
        {CardinalDirection.Right, 0f},
        {CardinalDirection.Down, 270f},
        {CardinalDirection.Left, 180f},
    };

    public void SetOrientation(CardinalDirection shootDir) {
        transform.rotation = Quaternion.Euler(0,0f, magicZRotation[shootDir]);
    }
}
