using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();
    CombHUDManager cHUDManager;

    void Awake()
    {
        cHUDManager = FindObjectOfType<CombHUDManager>();
        tiles.Capacity = 8;
        CreateTiles();
    }

    public void MoveCharacterTo(Character character, int position)
    {
        int oldPosition = character.IsPlayable() ? character.GetTile().GetRow() + tiles.Count / 2 : character.GetTile().GetIndex();
        int newPosition = position;
        if (character.IsPlayable())
        {
            newPosition = position + tiles.Count / 2;
        }
        tiles[oldPosition].setOccupant(tiles[newPosition].getOccupant());
        tiles[newPosition].setOccupant(character);
        ShowCharactersToThePlayer();
    }

    public void ShowCharactersToThePlayer()
    {
        cHUDManager.ShowCombatants(tiles);
    }

    public int GetRow(Character character)
    {
        return tiles.Find(t => t.getOccupant() == character).GetRow() % (tiles.Count / 2);
    }

    public int HowManyHeroes()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? t.getOccupant().IsPlayable() : false).Count;
    }

    public int HowManyEnemies()
    {
        return tiles.FindAll(t => t.getOccupant() != null ? !t.getOccupant().IsPlayable() : false).Count;
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

    public Tile[] getHeroesTiles()
    {
        return tiles.FindAll(t => t.isFromHero()).ToArray();
    }

    public Tile[] getEnemiesTiles()
    {
        return tiles.FindAll(t => !t.isFromHero()).ToArray();
    }

    public Tile[] getMySideTiles(bool side)
    {
        return side ? getHeroesTiles() : getEnemiesTiles();
    }

    public Tile[] getMyEnemiesTiles(bool side)
    {
        return getMySideTiles(!side);
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
        if (sideIsPlayers)
        {
            for (int i = 0; i < side.Count; i++)
            {
                tiles[i + tiles.Count / 2].setOccupant(side[i]);
            }
        }
        else
        {
            for (int i = 0; i < side.Count; i++)
            {
                tiles[i].setOccupant(side[i]);
            }
        }
    }

    void CreateTiles()
    {
        for (int i = 0; i < tiles.Capacity; i++)
        {
            if (i < tiles.Capacity / 2)
            {
                tiles.Add(new Tile(i, false, this));
            }
            else
            {
                tiles.Add(new Tile(i, true, this));
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

    public List<Tile> GetTiles() { return tiles; }

    public Tile[] GetAliveOpponents(Character character)
    {
        return character.IsPlayable() ? GetAliveNPCTiles() : GetAlivePCTiles();
    }

    public class Tile
    {
        [SerializeField] Character occupant;
        int index;
        bool fromHero;
        Battleground battleground;
        Tile[] side;
        Tile[] otherSide;

        public bool IsOccupied()
        {
            if (occupant != null)
            {
                return occupant.isAlive() ? true : false;
            }
            else
            {
                return false;
            }
        }
        public bool IsYourCharacter(Character character) { return character == occupant ? true : false; }
        public Tile(int index, bool fromHero, Battleground battleground)
        {
            this.index = index;
            this.fromHero = fromHero;
            this.battleground = battleground;
        }
        public void setOccupant(Character occupant) { this.occupant = occupant; }
        public Character getOccupant() { return occupant; }
        public int GetRow() { return index % (battleground.tiles.Count / 2); }
        public Vector2 getLocalPosition()
        {
            return FindObjectOfType<CombHUDManager>().GetTileUIs()[index].getRectTransform().localPosition;
            //if (fromHero)
            //{
            //    return FindObjectOfType<CombHUDManager>().getHeroesCombatantHUD()[GetRow()].getRectTransform().localPosition;
            //}
            //else
            //{
            //    return FindObjectOfType<CombHUDManager>().getEnemiesCombatantHUD()[GetRow()].getRectTransform().localPosition;
            //}
        }
        public bool isFromHero() { return fromHero; }
        public Tile[] GetAlliesTiles() { return fromHero ? battleground.getHeroesTiles() : battleground.getEnemiesTiles(); }
        public Tile[] GetEnemiesTiles() { return fromHero ? battleground.getEnemiesTiles() : battleground.getHeroesTiles(); }
        public int GetIndex() { return index; }
    }
}