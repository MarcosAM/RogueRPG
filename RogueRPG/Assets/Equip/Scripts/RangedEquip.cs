using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equip/Ranged")]
public class RangedEquip : Equip
{
    public override Battleground.Tile GetBestTarget(Character user)
    {
        List<Battleground.Tile> alliedTiles = DungeonManager.getInstance().getBattleground().GetTiles().FindAll(t => t.isFromHero() == user.IsPlayable());
        List<Battleground.Tile> aliveOpponentTiles = DungeonManager.getInstance().getBattleground().GetTiles().FindAll(t => t.isFromHero() != user.IsPlayable() && t.IsOccupied());
        alliedTiles.Sort((t1, t2) => GetSmallerDistance(t2, aliveOpponentTiles) - GetSmallerDistance(t1, aliveOpponentTiles));
        return alliedTiles.Find(t => Mathf.Abs(t.GetRow() - user.getPosition()) <= alliesSkill.GetRange());
    }

    int GetSmallerDistance(Battleground.Tile tile, List<Battleground.Tile> tiles)
    {
        tiles.Sort((t1, t2) => Mathf.Abs(tile.GetRow() - t2.GetRow()) - Mathf.Abs(tile.GetRow() - t1.GetRow()));
        return Mathf.Abs(tile.GetRow() - tiles[0].GetRow());
    }
}