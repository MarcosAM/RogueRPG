using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Revive Ally")]
public class AllyRevive : AllyAssist
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && tile.CharacterIsKnockOut() && user.IsPlayable() == tile.GetSide() && user != tile.GetCharacter(); }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return target == tile; }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        EffectAnimation(target, skillAnimation);
        effect.Affect(user, target.GetCharacter());
    }
}
