using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainRevive : RainAssist
{
    [SerializeField]
    [Range(1, 5)]
    protected int area;

    public override void Act(Character user, Tile target, SkillAnimation animationPrefab)
    {
        foreach (Tile tile in user.GetAlliesTiles())
        {
            if (WillBeAffected(user, target, tile))
            {
                EffectAnimation(target, animationPrefab);
                if (tile.CharacterIsKnockOut())
                    effect.Affect(user, target.GetCharacter());
            }
        }
    }

    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return Mathf.Abs(target.GetRow() - tile.GetRow()) <= area && user.IsPlayable() == tile.GetSide(); }
}
