using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Move and Attack")]
public class SMoveAtk : Skill
{
    [SerializeField]
    Move move;
    [SerializeField]
    Attack attack;

    public override bool IsTargetable(Character user, Tile tile) { return move.IsTargetable(user, tile); }

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile)
    {
        return move.WillBeAffected(user.GetTile(), target, tile) || attack.WillBeAffected(target, target.GetTileInFront(), tile);
    }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        move.Act(user, tile, animationPrefab);
        attack.Act(user, tile.GetTileInFront(), animationPrefab);
    }

    public override TurnSugestion GetTurnSugestion(Character user)
    {
        List<Tile> enemies = FindObjectOfType<Battleground>().GetTilesFromAliveCharactersOf(!user.IsPlayable());
        List<Tile> possibleTargets = FindObjectOfType<Battleground>().GetAvailableTilesFrom(user.IsPlayable()).FindAll(t => IsTargetable(user, t));

        if (possibleTargets.Count > 0)
        {
            possibleTargets.Sort((t1, t2) => enemies.FindAll(e => UniqueEffectWillAffect(user, t2, e)).Count - enemies.FindAll(e => UniqueEffectWillAffect(user, t1, e)).Count);

            return new TurnSugestion(TurnSugestion.maxProbability - (enemies.Count - enemies.FindAll(e => UniqueEffectWillAffect(user, possibleTargets[0], e)).Count), possibleTargets[0].GetIndex());
        }

        return new TurnSugestion(0);
    }
}