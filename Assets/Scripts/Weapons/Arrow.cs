using UnityEngine;
using System.Collections.Generic;

public class Arrow : Projectile {

	[SerializeField] SpriteRenderer mySpriteRenderer;

    public void SetArrowType(int arrowChoice) {
        mySpriteRenderer.sprite = WeaponSprites.Instance.arrowSprites[arrowChoice];
    }


    Dictionary<CardinalDirection, float> arrowZRotation = new Dictionary<CardinalDirection, float>() {
        {CardinalDirection.Up, 90f},
        {CardinalDirection.Right, 0f},
        {CardinalDirection.Down, 270f},
        {CardinalDirection.Left, 180f},
    };

    public void SetOrientation(CardinalDirection shootDir) {
        transform.rotation = Quaternion.Euler(0,0f, arrowZRotation[shootDir]);
    }
}
