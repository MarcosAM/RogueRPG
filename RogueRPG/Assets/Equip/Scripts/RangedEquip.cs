using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equip/Ranged")]
public class RangedEquip : Equip
{
    public override Battleground.Tile GetBestTarget(Character user)
    {
        List<Battleground.Tile> alliedTiles = DungeonManager.getInstance().getBattleground().GetTiles().FindAll(t => t.isFromHero() == user.IsPlayable() && !t.IsOccupied());
        List<Battleground.Tile> aliveOpponentTiles = DungeonManager.getInstance().getBattleground().GetTiles().FindAll(t => t.isFromHero() != user.IsPlayable() && t.IsOccupied());
        alliedTiles.Sort((t1, t2) => GetBetterTile(t1, t2, aliveOpponentTiles));
        return alliedTiles.Find(t => Mathf.Abs(t.GetRow() - user.getPosition()) <= alliesSkill.GetRange());
    }

    int GetSmallerDistance(Battleground.Tile tile, List<Battleground.Tile> tiles)
    {
        tiles.Sort((t1, t2) => Mathf.Abs(tile.GetRow() - t1.GetRow()) - Mathf.Abs(tile.GetRow() - t2.GetRow()));
        return Mathf.Abs(tile.GetRow() - tiles[0].GetRow());
    }

    int GetBetterTile(Battleground.Tile tile1, Battleground.Tile tile2, List<Battleground.Tile> tiles)
    {
        int tile1Value = 0;
        int tile2Value = 0;
        foreach (Battleground.Tile tile in tiles)
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