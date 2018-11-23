using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Rain")]
public class RainAssist : Assist
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
                if (tile.CharacterIs(AffectKnockOut))
                    effect.Affect(user, target.GetCharacter());
            }
        }
    }

    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && user.IsPlayable() == tile.GetSide(); }
    public override bool WillBeAffected(Character user, Tile target, Tile tile) { return Mathf.Abs(target.GetRow() - tile.GetRow()) <= area && user.IsPlayable() == tile.GetSide(); }
}
