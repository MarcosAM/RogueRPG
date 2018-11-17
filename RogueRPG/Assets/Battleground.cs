using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                            tiles[i].SetAvailable(true);
                        else
                            tiles[i].SetAvailable(false);
                        break;
                    case BattlegroundSize.Big:
                        if (i % 6 > 0 && i % 6 <= 5)
                            tiles[i].SetAvailable(true);
                        else
                            tiles[i].SetAvailable(false);
                        break;
                    default:
                        tiles[i].SetAvailable(true);
                        break;
                }
            }
        }
    }

    CombHUDManager cHUDManager;

    void Awake()
    {
        cHUDManager = FindObjectOfType<CombHUDManager>();
        tiles = GetComponentsInChildren<Tile>().OfType<Tile>().ToList();
        tiles.Capacity = tiles.Count;
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

        List<Tile> availableTiles = tiles.FindAll(t => t.IsAvailable());

        if (sideIsPlayers)
        {
            for (int i = 0; i < side.Count; i++)
            {
                availableTiles[i + availableTiles.Count / 2].SetCharacter(side[i]);
            }
        }
        else
        {
            for (int i = 0; i < side.Count; i++)
            {
                availableTiles[i].SetCharacter(side[i]);
            }
        }
    }

    public List<Tile> GetTiles() { return tiles; }
    public List<Tile> GetAvailableTiles() { return tiles.FindAll(t => t.IsAvailable()); }
    public List<Tile> GetAvailableTilesFrom(bool side) { return tiles.FindAll(t => t.GetSide() == side && t.IsAvailable()); }
    public List<Tile> GetTilesFromAliveCharactersOf(bool side) { return tiles.FindAll(t => t.GetCharacter() != null ? t.GetCharacter().IsPlayable() == side && t.GetCharacter().isAlive() : false); }

    List<Character> GetAllTilesOccupants() { return tiles.ConvertAll(t => t.GetCharacter()); }
    public List<Character> GetAliveCharactersFrom(bool side) { return GetAllTilesOccupants().FindAll(c => c != null ? c.IsPlayable() == side : false); }
}