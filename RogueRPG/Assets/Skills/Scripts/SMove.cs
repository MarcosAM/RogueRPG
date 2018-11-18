using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Move")]
public class SMove : Skill
{

    protected override void UniqueEffect(Character user, Tile tile)
    {
        tile.SetCharacter(user);
    }

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile)
    {
        if (target == tile)
        {
            return tile.CharacterIsAlive() ? true : Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range;
        }
        else
            return false;
    }
}
