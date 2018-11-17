using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour
{
    public enum BattlegroundSize { Normal, Big, Large }
    [SerializeField] List<Tile> tiles = new List<Tile>();
    BattlegroundSize size = BattlegroundSize.Normal;
    public BattlegroundSize Size
    {
        get
        {
            return size;
        }
        set
        {
            this.size = value;
            for (int i = 0; i < tiles.Capacity; i++)
            {
                switch (this.size)
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
                            tiles[i].SetEnabled(false);
                        break;
                    default:
                        tiles[i].SetEnabled(true);
                        break;
                }
            }
        }
    }

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
            switch (Size)
            {
                case BattlegroundSize.Normal:
                    if (i % (tiles.Capacity / 2) > 0 && i % (tiles.Capacity / 2) < 5)
                        isTileEnabled = true;
                    else
                        isTileEnabled = false;
                    break;
                case BattlegroundSize.Big:
                    if (i % (tiles.Capacity / 2) > 0 && i % (tiles.Capacity / 2) <= 5)
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

    public void SetAvailableSide(List<Character> side)
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

    public List<Tile> GetTiles() { return tiles; }
    public List<Tile> GetAvailableTiles() { return tiles.FindAll(t => t.IsEnabled()); }
    public List<Tile> GetAvailableTilesFrom(bool side) { return tiles.FindAll(t => t.isFromHero() == side && t.IsEnabled()); }
    public List<Tile> GetTilesFromAliveCharactersOf(bool side) { return tiles.FindAll(t => t.getOccupant() != null ? t.getOccupant().IsPlayable() == side && t.getOccupant().isAlive() : false); }

    List<Character> GetAllTilesOccupants() { return tiles.ConvertAll(t => t.getOccupant()); }
    public List<Character> GetAliveCharactersFrom(bool side) { return GetAllTilesOccupants().FindAll(c => c != null ? c.IsPlayable() == side : false); }
}