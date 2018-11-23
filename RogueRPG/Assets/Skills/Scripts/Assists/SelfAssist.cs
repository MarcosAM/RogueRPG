using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Self")]
public class SelfAssist : Assist
{
    public override void Act(Character user, Tile target, SkillAnimation animationPrefab)
    {
        EffectAnimation(target, animationPrefab);
        effect.Affect(user, target.GetCharacter());
    }

    public override bool IsTargetable(Character user, Tile tile) { return user == tile.GetCharacter(); }

    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }
}
