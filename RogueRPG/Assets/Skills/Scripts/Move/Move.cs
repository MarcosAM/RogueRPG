using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class Move : Actions
{
    public override void Act(Character user, Tile target, SkillAnimation animationPrefab)
    {
        target.SetCharacter(user);
    }

    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetPosition() - tile.GetRow()) <= range && user.IsPlayable() == tile.GetSide(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }
    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground)
    {
        List<Tile> alliesTiles = battleground.GetAvailableTilesFrom(user.IsPlayable()).FindAll(t => !t.CharacterIs(true) ? IsTargetable(user, t) : t.GetCharacter() == user);
        if (alliesTiles.Count > 0)
        {
            List<Tile> aliveOpponentTiles = battleground.GetTilesFromAliveCharactersOf(user.IsPlayable());
            if (user.GetStatValue(Stat.Stats.Def) > DungeonManager.getInstance().GetDefAvg())
            {
                alliesTiles.Sort((t1, t2) => GetBetterTile(t2, t1, aliveOpponentTiles));
            }
            else
            {
                alliesTiles.Sort((t1, t2) => GetBetterTile(t1, t2, aliveOpponentTiles));
            }
            if (alliesTiles[0] != user.GetTile())
                return new TurnSugestion(1, alliesTiles[0].GetIndex());
        }
        return new TurnSugestion(0);
    }

    int GetSmallerDistance(Tile tile, List<Tile> tiles)
    {
        tiles.Sort((t1, t2) => Mathf.Abs(tile.GetRow() - t1.GetRow()) - Mathf.Abs(tile.GetRow() - t2.GetRow()));
        return Mathf.Abs(tile.GetRow() - tiles[0].GetRow());
    }

    int GetBetterTile(Tile tile1, Tile tile2, List<Tile> tiles)
    {
        int tile1Value = 0;
        int tile2Value = 0;
        foreach (Tile tile in tiles)
        {
            tile1Value += Mathf.Abs(tile1.GetRow() - tile.GetRow());
            tile2Value += Mathf.Abs(tile2.GetRow() - tile.GetRow());
        }
        if (tile1Value != tile2Value)
        {
            return tile2Value - tile1Value;
        }
        else
        {
            tile1Value = GetSmallerDistance(tile1, tiles);
            tile2Value = GetSmallerDistance(tile2, tiles);
            return tile2Value - tile1Value;
        }
    }
}