using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAssistMove : Skill
{
    [SerializeField]
    Assist assist;
    [SerializeField]
    Move move;

    public override bool IsTargetable(Character user, Tile tile) { return move.IsTargetable(user, tile); }
    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return assist.WillBeAffected(user.GetTile(), target, tile) || move.WillBeAffected(user.GetTile(), target, tile); }

    protected override void UniqueEffect(Character user, Tile tile)
    {
        assist.Act(user, tile, animationPrefab);
        move.Act(user, tile, animationPrefab);
    }
}