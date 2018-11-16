using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour
{
    public enum BattlegroundSize { Normal, Big, Large }
    [SerializeField] List<Tile> tiles = new List<Tile>();
    BattlegroundSize size = BattlegroundSize.Normal;

    CombHUDManager cHUDManager;

    void Awake()
    {
        cHUDManager = FindObjectOfType<CombHUDManager>();
        tiles.Capacity = 12;
        InitializeTiles();
    }

    void InitializeTiles()
    {
        bool isTileEnabled;
        for (int i = 0; i < tiles.Capacity; i++)
        {
            switch (size)
            {
                case BattlegroundSize.Normal:
                    if (i % 6 > 0 && i % 6 < 5)
                        isTileEnabled = true;
                    else
                        isTileEnabled = false;
                    break;
                case BattlegroundSize.Big:
                    if (i % 6 > 0 && i % 6 <= 5)
                        isTileEnabled = true;
                    else
                        isTileEnabled = false;
                    break;
                default:
                    isTileEnabled = true;
                    break;
            }
            if (i < tiles.Capacity / 2)
            {
                tiles[i].Initialize(i, false, this, isTileEnabled);
            }
            else
            {
                tiles[i].Initialize(i, true, this, isTileEnabled);
            }
        }
    }

    public int GetRow(Character character)
    {
        return tiles.Find(t => t.getOccupant() == character).GetRow() % (tiles.Count / 2);
    }

    public int HowManyCharacters(bool alignment)
    {
        return tiles.FindAll(t => t.getOccupant() != null ? t.getOccupant().IsPlayable() == alignment : false).Count;
    }

    public List<Character> getHeroSide()
    {
        List<Character> characters = new List<Character>();
        for (int i = tiles.Count / 2; i < tiles.Count; i++)
        {
            characters.Add(tiles[i].getOccupant());
        }
        return characters;
    }
    public List<Character> getEnemySide()
    {
        List<Character> characters = new List<Character>();
        for (int i = 0; i < tiles.Count / 2; i++)
        {
            characters.Add(tiles[i].getOccupant());
        }
        return characters;
    }

    public Tile[] GetHeroesTiles()
    {
        return tiles.FindAll(t => t.isFromHero() && t.IsEnabled()).ToArray();
    }

    public Tile[] GetEnemiesTiles()
    {
        return tiles.FindAll(t => !t.isFromHero() && t.IsEnabled()).ToArray();
    }

    public Tile[] GetMySideTiles(bool side)
    {
        return side ? GetHeroesTiles() : GetEnemiesTiles();
    }

    public Tile[] GetMyEnemiesTiles(bool side)
    {
        return GetMySideTiles(!side);
    }

    public void ClearAndSetASide(List<Character> side)
    {
        bool sideIsPlayers = false;
        int sideSize = side.Count;
        for (int i = 0; i < side.Count; i++)
        {
            if (side[i] != null)
            {
                sideIsPlayers = side[i].IsPlayable();
            }
        }

        List<Tile> availableTiles = tiles.FindAll(t => t.IsEnabled());

        if (sideIsPlayers)
        {
            for (int i = 0; i < side.Count; i++)
            {
                availableTiles[i + availableTiles.Count / 2].SetOccupant(side[i]);
            }
        }
        else
        {
            for (int i = 0; i < side.Count; i++)
            {
                availableTiles[i].SetOccupant(side[i]);
            }
        }
    }

    public Tile GetTile(Character character)
    {
        return tiles.Find(t => t.getOccupant() == character);
    }

    public Tile[] GetAlivePCTiles()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? t.getOccupant().IsPlayable() && t.getOccupant().isAlive() : false).ToArray();
    }

    public Tile[] GetAliveNPCTiles()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? !t.getOccupant().IsPlayable() && t.getOccupant().isAlive() : false).ToArray();
    }

    public List<Tile> GetEnabledTiles() { return tiles.FindAll(t => t.IsEnabled()); }
    public List<Tile> GetTiles() { return tiles; }

    public Tile[] GetAliveOpponents(Character character)
    {
        return character.IsPlayable() ? GetAliveNPCTiles() : GetAlivePCTiles();
    }

    public void SetSize(Battleground.BattlegroundSize size)
    {
        for (int i = 0; i < tiles.Capacity; i++)
        {
            switch (size)
            {
                case BattlegroundSize.Normal:
                    if (i % 6 > 0 && i % 6 < 5)
                        tiles[i].SetEnabled(true);
                    else
                        tiles[i].SetEnabled(false);
                    break;
                case BattlegroundSize.Big:
                    if (i % 6 > 0 && i % 6 <= 5)
                        tiles[i].SetEnabled(true);
                    else
                    {
                        print("Mais um falso");
                        tiles[i].SetEnabled(false);
                    }
                    break;
                default:
                    tiles[i].SetEnabled(true);
                    break;
            }
        }
    }
}