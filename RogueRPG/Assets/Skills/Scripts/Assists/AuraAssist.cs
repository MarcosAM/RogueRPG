using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Aura")]
public class AuraAssist : AllyAssist
{
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return user.GetTile() != tile && user.IsPlayable() == tile.GetSide() && Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range; }

    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        foreach (Tile tile in user.GetAlliesTiles())
        {
            if (WillBeAffected(user, target, tile))
            {
                EffectAnimation(target, skillAnimation);
                if (tile.CharacterIsAlive())
                    effect.Affect(user, target.GetCharacter());
            }
        }
    }
}
