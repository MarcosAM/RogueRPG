using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Single")]
public class SingleAssist : Assist
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && tile.CharacterIsAlive() && user.IsPlayable() != tile.GetSide(); }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return target == tile; }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        EffectAnimation(target, skillAnimation);
        effect.Start(user, target.GetCharacter());
    }
}
