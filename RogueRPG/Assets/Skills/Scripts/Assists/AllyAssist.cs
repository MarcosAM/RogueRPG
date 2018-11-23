using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Ally")]
public class AllyAssist : Assist
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && tile.CharacterIs(AffectKnockOut) && user.IsPlayable() == tile.GetSide() && user != tile.GetCharacter(); }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return target == tile; }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        EffectAnimation(target, skillAnimation);
        effect.Affect(user, target.GetCharacter());
    }
}
