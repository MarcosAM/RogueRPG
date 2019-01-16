using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class Move : Actions
{
    public override void Act(Character user, Tile target, SkillEffect skillEffect)
    {
        target.MoveCharacter(user);
    }

    //TODO impedir de clicar em si mesmo
    public override bool IsTargetable(Character user, Tile tile) { return Mathf.Abs(user.GetRow() - tile.GetRow()) <= range && user.IsPlayable() == tile.GetSide(); }
    public override bool WillBeAffected(Tile user, Tile target, Tile tile) { return target == tile; }
    public override TurnSugestion GetTurnSugestion(Character user, Battleground battleground, SkillEffect skillEffect)
    {
        if (user.GetInventory().Archetype == Archetypes.Archetype.Brute && user.GetTile().GetTileInFront().GetCharacter())
            return new TurnSugestion(0);

        var targetables = battleground.GetAvailableTilesFrom(user.IsPlayable());

        switch (user.GetInventory().Archetype)
        {
            case Archetypes.Archetype.Brute:
            case Archetypes.Archetype.Agressive:
                targetables.RemoveAll(t => !FilterForBrutesAndAgressives(user, t));
                break;
            case Archetypes.Archetype.Infantry:
            case Archetypes.Archetype.MInfantry:
                targetables.RemoveAll(t => !FilterForInfantry(user, t));
                break;
            case Archetypes.Archetype.Offensive:
            case Archetypes.Archetype.MOffensive:
                targetables.RemoveAll(t => !FilterForOffensive(user, t));
                break;
            default:
                targetables.RemoveAll(t => !FilterForNoneToDisablers(user, t));
                break;
        }

        if (targetables.Count > 0)
        {
            List<Tile> aliveOpponentTiles = battleground.GetTilesFromAliveCharactersOf(!user.IsPlayable());
            var mySideTiles = battleground.GetAvailableTilesFrom(user.IsPlayable());

            bool shouldMove;
            switch (user.GetInventory().Archetype)
            {
                case Archetypes.Archetype.Agressive:
                case Archetypes.Archetype.Brute:
                    targetables.Sort((t2, t1) => GetBetterTile(t1, t2, aliveOpponentTiles));
                    mySideTiles.Sort((t2, t1) => GetBetterTile(t1, t2, aliveOpponentTiles));
                    shouldMove = targetables[0] != user.GetTile();
                    break;
                case Archetypes.Archetype.Infantry:
                    targetables.Sort((t1, t2) => SortByStat(t1.GetTileInFront(), t2.GetTileInFront(), Attribute.Type.Atk));
                    mySideTiles.Sort((t1, t2) => SortByStat(t1.GetTileInFront(), t2.GetTileInFront(), Attribute.Type.Atk));
                    shouldMove = targetables[0] != user.GetTile();
                    break;
                case Archetypes.Archetype.MInfantry:
                    targetables.Sort((t1, t2) => SortByStat(t1.GetTileInFront(), t2.GetTileInFront(), Attribute.Type.Atkm));
                    mySideTiles.Sort((t1, t2) => SortByStat(t1.GetTileInFront(), t2.GetTileInFront(), Attribute.Type.Atkm));
                    shouldMove = targetables[0] != user.GetTile();
                    break;
                default:
                    targetables.Sort((t1, t2) => GetBetterTile(t1, t2, aliveOpponentTiles));
                    mySideTiles.Sort((t1, t2) => GetBetterTile(t1, t2, aliveOpponentTiles));
                    shouldMove = GetBetterTile(targetables[0], user.GetTile(), aliveOpponentTiles) != 0;
                    break;
            }

            if (shouldMove)
                return new TurnSugestion(TurnSugestion.maxProbability - mySideTiles.IndexOf(targetables[0]), targetables[0].GetIndex());
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


    bool FilterForNoneToDisablers(Character user, Tile tile)
    {
        return !tile.CharacterIs(true) ? IsTargetable(user, tile) : tile.GetCharacter() == user;
    }

    bool FilterForOffensive(Character user, Tile tile)
    {
        return !tile.CharacterIs(true) ? IsTargetable(user, tile) : IsTargetable(user, tile) && (tile.GetCharacter() == user || tile.GetCharacter().GetInventory().Archetype >= Archetypes.Archetype.MInfantry);
    }

    bool FilterForInfantry(Character user, Tile tile)
    {
        return !tile.CharacterIs(true) ? IsTargetable(user, tile) : IsTargetable(user, tile) && (tile.GetCharacter() == user || tile.GetCharacter().GetInventory().Archetype < Archetypes.Archetype.MInfantry);
    }

    bool FilterForBrutesAndAgressives(Character user, Tile tile)
    {
        return IsTargetable(user, tile);
    }

    int SortByStat(Tile tile1, Tile tile2, Attribute.Type stats)
    {
        var tile1Atk = 0;
        var tile2Atk = 0;

        if (tile1.GetCharacter())
            tile1Atk = (int)tile1.GetCharacter().GetAttributes().GetAttributeValue(stats);

        if (tile2.GetCharacter())
            tile2Atk = (int)tile2.GetCharacter().GetAttributes().GetAttributeValue(stats);

        return tile2Atk - tile1Atk;
    }

    public override string GetTargetDescription() { return "A tile up to " + range + " tile(s)"; }
}