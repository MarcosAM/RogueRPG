using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[CreateAssetMenu(menuName = "Actions/Attacks/Explosion")]
public class ExplosionAttack : ConeAttack
{
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return Mathf.Abs(target.GetRow() - tile.GetRow()) <= area; }

    public override void Act(Character user, Tile target, SkillEffect skillEffect)
    {
        GenerateNewAttack(user);

        foreach (Tile tile in FindObjectOfType<Battleground>().GetAvailableTiles())
        {
            if (WillBeAffected(user.GetTile(), target, tile))
            {
                skillEffect.EffectAnimation(tile);
                if (tile.CharacterIs(true))
                    skillEffect.TryToAffect(user, tile.GetCharacter(), attack);
            }
        }
    }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground, SkillEffect skillEffect)
    {
        List<Tile> enemies = battleground.GetTilesFromAliveCharactersOf(!user.Playable);
        List<Tile> allies = battleground.GetTilesFromAliveCharactersOf(user.Playable);
        List<Tile> possibleTargets = enemies.FindAll(t => IsTargetable(user, t));
        if (possibleTargets.Count > 0)
        {
            Dictionary<Tile, int> targetsAndAmount = new Dictionary<Tile, int>();
            foreach (Tile tile in possibleTargets)
            {
                targetsAndAmount.Add(tile, enemies.FindAll(t => WillBeAffected(user.GetTile(), tile, t)).Count - allies.FindAll(t => WillBeAffected(user.GetTile(), tile, t)).Count);
            }
            targetsAndAmount.Values.OrderByDescending(x => x);

            Tile target = GetRandomTarget(targetsAndAmount.Keys.ToList());
            return new TurnSugestion(TurnSugestion.maxProbability - (area - targetsAndAmount[target]), target.GetIndex());
        }
        else
        {
            return new TurnSugestion(0);
        }
    }

    public override string GetTargetDescription() { return "Explosion " + area + ". Range: " + (range + 1) + ". Precision: " + precision * 100 + "%"; }

}