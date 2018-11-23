using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        attack.Act(user, tile, animationPrefab);
    }
}