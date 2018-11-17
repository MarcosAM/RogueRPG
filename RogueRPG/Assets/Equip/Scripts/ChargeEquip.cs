using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equip/Charge")]
public class ChargeEquip : Equip
{
    public override Tile GetBestTarget(Character user)
    {

        if (selfSkill is SBuff)
        {
            SBuff buff = (SBuff)selfSkill;
            if (user.GetBuffIntensity(buff.GetStats()) < buff.GetIntensity() && user.GetBuffIntensity(buff.GetStats()) != Stat.Intensity.None)
            {
                return user.GetTile();
            }
            else if (Random.value < 0.85 && user.GetBuffIntensity(buff.GetStats()) <= buff.GetIntensity())
            {
                return user.GetTile();
            }
        }

        List<Tile> aliveOpponentTiles = DungeonManager.getInstance().getBattleground().GetAvailableTiles().FindAll(t => t.GetSide() != user.IsPlayable() && t.CharacterIsAlive());
        if (aliveOpponentTiles.Exists(t => Mathf.Abs(t.GetRow() - user.GetTile().GetRow()) <= meleeSkill.GetRange()))
        {
            aliveOpponentTiles.RemoveAll(t => Mathf.Abs(t.GetRow() - user.GetTile().GetRow()) > meleeSkill.GetRange());
            return aliveOpponentTiles[Random.Range(0, aliveOpponentTiles.Count - 1)];
        }


        List<Tile> alliedTiles = DungeonManager.getInstance().getBattleground().GetAvailableTiles().FindAll(t => t.GetSide() == user.IsPlayable() && !t.CharacterIsAlive());

        alliedTiles.Sort((t1, t2) => GetSmallerDistance(t1, aliveOpponentTiles) - GetSmallerDistance(t2, aliveOpponentTiles));
        alliedTiles.RemoveAll(t => Mathf.Abs(t.GetRow() - user.GetTile().GetRow()) > alliesSkill.GetRange());
        if (alliedTiles.Count > 0)
            return alliedTiles[0];
        else
            return user.GetTile();
    }

    int GetSmallerDistance(Tile tile, List<Tile> tiles)
    {
        tiles.Sort((t1, t2) => Mathf.Abs(tile.GetRow() - t1.GetRow()) - Mathf.Abs(tile.GetRow() - t2.GetRow()));
        return Mathf.Abs(tile.GetRow() - tiles[0].GetRow());
    }
}