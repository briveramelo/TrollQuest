using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Magic : Weapon {

    [SerializeField] GameObject magicBlast;

    Dictionary<CardinalDirection, Vector2> cardMoveDir = new Dictionary<CardinalDirection, Vector2>() {
        {CardinalDirection.Up,Vector2.up},
        {CardinalDirection.Right,Vector2.right},
        {CardinalDirection.Down,Vector2.down},
        {CardinalDirection.Left,Vector2.left}
    };

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

    public float shootSpeed;
    public override IEnumerator Attack(int wielderAttack, CardinalDirection attackDir) {
        int numClips = myWeaponStats.myAudioClips.Length;
        int levelInd = myWeaponStats.level - 1;
        if (levelInd < numClips)
        {
            mySoundBox.PlayOneShot(myWeaponStats.myAudioClips[levelInd]);
        }
        MagicBlast myMagicBlast = (Instantiate(magicBlast, transform.position, Quaternion.identity) as GameObject).GetComponent<MagicBlast>();
        myMagicBlast.SetMagicType(myWeaponStats.level);
        int attack = myWeaponStats.attack + wielderAttack;
        myMagicBlast.Launch(cardMoveDir[attackDir] * shootSpeed, attack);
        myMagicBlast.SetOrientation(attackDir);

        yield return new WaitForSeconds((1f / (float)myWeaponStats.strikesPerSecond));
    }

}
