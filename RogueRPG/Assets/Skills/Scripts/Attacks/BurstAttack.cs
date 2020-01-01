using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attacks/Burst")]
public class BurstAttack : Attack
{
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetRow() - tile.GetRow()) <= range && user.GetTile() != tile; }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return IsTargetable(user.GetCharacter(), tile); }
    public override void Act(Character user, Tile target, SkillEffect skillEffect)
    {
        foreach (Tile tile in FindObjectOfType<Battleground>().GetAvailableTiles())
        {
            if (WillBeAffected(user.GetTile(), target, tile))
            {
                skillEffect.EffectAnimation(tile);
                if (tile.CharacterIs(true))
                    skillEffect.TryToAffect(user, tile.GetCharacter(), GenerateNewAttack(user));
            }
        }
    }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground, SkillEffect skillEffect)
    {
        List<Tile> enemies = battleground.GetTilesFromAliveCharactersOf(!user.Playable);
        List<Tile> allies = battleground.GetTilesFromAliveCharactersOf(user.Playable);
        enemies.RemoveAll(t => !IsTargetable(user, t));
        allies.RemoveAll(t => !IsTargetable(user, t));

        return new TurnSugestion(TurnSugestion.maxProbability - allies.Count, enemies[0].GetIndex());
    }

    public override string GetTargetDescription() { return "Burst " + range + ". Precision: " + precision * 100 + "%"; }
}