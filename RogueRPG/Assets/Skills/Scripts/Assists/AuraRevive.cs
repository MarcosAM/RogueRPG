using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Aura Revive")]
public class AuraRevive : AuraAssist
{
    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        foreach (Tile tile in user.GetAlliesTiles())
        {
            if (WillBeAffected(user, target, tile))
            {
                EffectAnimation(target, skillAnimation);
                if (tile.CharacterIsKnockOut())
                    effect.Affect(user, target.GetCharacter());
            }
        }
    }
}
