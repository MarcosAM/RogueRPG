using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Move")]
public class SMove : Skill
{

    public override void UniqueEffect(Character user, Tile tile)
    {
        tile.SetCharacter(user);
    }

    public override bool WillBeAffected(Character user, Tile target, Tile tile)
    {
        if (target == tile)
        {
            return tile.CharacterIsAlive() ? true : Mathf.Abs(user.getPosition() - tile.GetRow()) <= range;
        }
        else
            return false;
    }
}
