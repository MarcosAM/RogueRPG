using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Movement")]
public class Movement : Skill
{
    [SerializeField]
    Move move;

    public override bool IsTargetable(Character user, Tile tile) { return move.IsTargetable(user, tile); }

    public override bool UniqueEffectWillAffect(Character user, Tile target, Tile tile) { return move.WillBeAffected(user.GetTile(), target, tile); }

    protected override void UniqueEffect(Character user, Tile tile) { move.Act(user, tile); }

    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground) { return move.GetTurnSugestion(user, battleground); }
}
