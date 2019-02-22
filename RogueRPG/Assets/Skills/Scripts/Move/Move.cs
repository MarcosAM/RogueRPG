using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class Move : Actions
{
    public override void Act(Character user, Tile target, SkillEffect skillEffect)
    {
        user.Move(target);
    }

    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetRow() - tile.GetRow()) <= range && user.Playable == tile.GetSide(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }
    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground, SkillEffect skillEffect)
    {
        var availableTiles = battleground.GetAvailableTilesFrom(user.Playable).FindAll(t => IsTargetable(user, t) ? !t.CharacterIs(true) : false);

        if (availableTiles.Count > 0)
        {
            var enemiesTiles = battleground.GetTilesFromAliveCharactersOf(!user.Playable);
            enemiesTiles.Sort((e1, e2) => ClosestToTile(e1, e2, user.GetTile()));

            if (CombatRules.GetDistance(enemiesTiles[0], user.GetTile()) > 0)
            {
                availableTiles.Sort((t1, t2) => ClosestToTile(t1, t2, enemiesTiles[0]));

                return new TurnSugestion(1, availableTiles[0].GetIndex());
            }
        }

        return new TurnSugestion(0);
    }

    int ClosestToTile(Tile tile1, Tile tile2, Tile tile)
    {
        return CombatRules.GetDistance(tile1, tile) - CombatRules.GetDistance(tile2, tile);
    }

    public override string GetTargetDescription() { return "A tile up to " + range + " tile(s)"; }
}