using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Assist
{
    public override void Act(Character user, Tile target, SkillAnimation animationPrefab)
    {
        target.SetCharacter(user);
    }

    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && user.IsPlayable() != tile.GetSide(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }
}