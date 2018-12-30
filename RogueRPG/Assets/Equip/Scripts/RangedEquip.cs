using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equip/Ranged")]
public class RangedEquip : Equip
{
    //public override Tile GetBestTarget(Character user)
    //{
    //    List<Tile> alliedTiles = DungeonManager.getInstance().getBattleground().GetAvailableTiles().FindAll(t => t.GetSide() == user.IsPlayable() && (!t.CharacterIs(true) || t.GetCharacter() == user));
    //    List<Tile> aliveOpponentTiles = DungeonManager.getInstance().getBattleground().GetAvailableTiles().FindAll(t => t.GetSide() != user.IsPlayable() && t.CharacterIs(true));
    //    alliedTiles.Sort((t1, t2) => GetBetterTile(t1, t2, aliveOpponentTiles));
    //    Tile targetTile = alliedTiles.Find(t => Mathf.Abs(t.GetRow() - user.GetPosition()) <= alliesSkill.GetRange());
    //    if (targetTile == user.GetTile())
    //    {
    //        if (user.IsDebuffed())
    //        {
    //            if (selfSkill is SAssist)
    //            {
    //                if (((SAssist)selfSkill).GetAssist().GetEffect() is BuffEffect)
    //                {
    //                    if (user.IsDebuffed(((BuffEffect)(((SAssist)selfSkill).GetAssist().GetEffect())).GetStats()))
    //                    {
    //                        if (user.GetBuffIntensity(((BuffEffect)(((SAssist)selfSkill).GetAssist().GetEffect())).GetStats()) < ((BuffEffect)(((SAssist)selfSkill).GetAssist().GetEffect())).GetIntensity())
    //                        {
    //                            return targetTile;
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        if (aliveOpponentTiles.Exists(t => Mathf.Abs(user.GetTile().GetRow() - t.GetRow()) > meleeSkill.GetRange()))
    //        {
    //            aliveOpponentTiles.RemoveAll(t => Mathf.Abs(user.GetTile().GetRow() - t.GetRow()) <= meleeSkill.GetRange());
    //        }
    //        targetTile = aliveOpponentTiles[Random.Range(0, aliveOpponentTiles.Count)];
    //    }
    //    return targetTile;
    //}

    //int GetSmallerDistance(Tile tile, List<Tile> tiles)
    //{
    //    tiles.Sort((t1, t2) => Mathf.Abs(tile.GetRow() - t1.GetRow()) - Mathf.Abs(tile.GetRow() - t2.GetRow()));
    //    return Mathf.Abs(tile.GetRow() - tiles[0].GetRow());
    //}

    //int GetBetterTile(Tile tile1, Tile tile2, List<Tile> tiles)
    //{
    //    int tile1Value = 0;
    //    int tile2Value = 0;
    //    foreach (Tile tile in tiles)
    //    {
    //        tile1Value += Mathf.Abs(tile1.GetRow() - tile.GetRow());
    //        tile2Value += Mathf.Abs(tile2.GetRow() - tile.GetRow());
    //    }
    //    if (tile1Value != tile2Value)
    //    {
    //        return tile2Value - tile1Value;
    //    }
    //    else
    //    {
    //        tile1Value = GetSmallerDistance(tile1, tiles);
    //        tile2Value = GetSmallerDistance(tile2, tiles);
    //        return tile2Value - tile1Value;
    //    }
    //}
}