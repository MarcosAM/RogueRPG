using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Assist/Aura")]
public class AuraAssist : Assist
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && user.IsPlayable() == tile.GetSide() && user != tile.GetCharacter(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return user != tile && user.GetSide() == tile.GetSide() && Mathf.Abs(user.GetRow() - tile.GetRow()) <= range; }
    public override void Act(Character user, Tile target, SkillAnimation skillAnimation)
    {
        foreach (Tile tile in user.GetAlliesTiles())
        {
            if (WillBeAffected(user.GetTile(), target, tile))
            {
                EffectAnimation(target, skillAnimation);
                if (tile.CharacterIs(!AffectKnockOut))
                    effect.Affect(user, tile.GetCharacter());
            }
        }
    }

    public override TurnSugestion GetTurnSugestion(Character user)
    {
        List<Tile> allies = FindObjectOfType<Battleground>().GetTilesFromAliveCharactersOf(user.IsPlayable());
        List<Tile> possibleTargets = allies.FindAll(t => IsTargetable(user, t));
        if (possibleTargets.Count > 0)
        {
            return new TurnSugestion(TurnSugestion.maxProbability - (allies.Count - possibleTargets.Count), possibleTargets[0].GetIndex());
        }
        else
            return new TurnSugestion(0);
    }
}
