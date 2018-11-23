using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Melee")]
public class MeleeAttack : Attack
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && tile.CharacterIs(true) && user.IsPlayable() != tile.GetSide(); }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return target == tile; }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        GenerateNewAttack(user);
        EffectAnimation(target, skillAnimation);
        if (target.CharacterIs(true))
            damage.TryToDamage(user, target.GetCharacter(), attack);
    }
}
