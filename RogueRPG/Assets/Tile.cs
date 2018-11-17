using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Character character;
    [SerializeField] int index;
    [SerializeField] bool side;
    bool available = true;
    Battleground battleground;
    [SerializeField] RectTransform portraitHandler;

    void Start()
    {
        this.battleground = GetComponentInParent<Battleground>();
        this.index = battleground.GetTiles().IndexOf(this);
        this.side = this.index >= (battleground.GetTiles().Count / 2) ? true : false;
        if (this.side)
            portraitHandler.localScale = new Vector3(-1, 1, 1);
    }

    public bool CharacterIsAlive() { return character != null ? character.isAlive() : false; }
    public void SetOccupant(Character occupant)
    {
        if (occupant != null)
        {
            if (occupant.IsPlayable() != side)
            {
                battleground.GetTiles().Find(t => t.isFromHero() != side && t.GetRow() == GetRow()).SetOccupant(occupant);
                return;
            }
        }

        foreach (Transform transform in portraitHandler.transform)
        {
            transform.SetParent(null);
        }
        if (occupant != null)
        {
            Tile oldTile = occupant.GetTile();
            if (oldTile != null)
            {
                occupant.GetTile().SetOccupant(null);
                oldTile.SetOccupant(this.character);
            }
        }

        this.character = occupant;
        if (occupant != null)
        {
            this.character.transform.SetParent(portraitHandler);
            this.character.transform.localPosition = new Vector3(0, 0);
        }
        //battleground.ShowCharactersToThePlayer();
    }
    public Character getOccupant() { return character; }
    public int GetRow() { return index % (battleground.GetTiles().Count / 2); }
    public Vector2 getLocalPosition()
    {
        return transform.localPosition;
    }
    public bool isFromHero() { return side; }
    public List<Tile> GetAlliesTiles() { return battleground.GetAvailableTilesFrom(side); }
    public List<Tile> GetEnemiesTiles() { return battleground.GetAvailableTilesFrom(!side); }
    public int GetIndex() { return index; }
    public bool IsEnabled() { return available; }
    public Battleground GetBattleground() { return battleground; }

    public void SetEnabled(bool enabled)
    {
        if (!enabled && this.available != enabled)
        {
            if (character == null)
            {
                this.available = enabled;
            }
            else
            {
                List<Character> characters = new List<Character>();
                var mySide = battleground.GetAvailableTilesFrom(side);
                foreach (Tile tile in mySide)
                {
                    characters.Add(tile.getOccupant());
                    tile.SetOccupant(null);
                }
                this.available = enabled;
                for (int i = 0; i < characters.Count; i++)
                {
                    if (characters[i] != null && !mySide[i].IsEnabled())
                    {
                        characters.RemoveAt(characters.FindIndex(c => c == null));
                        break;
                    }
                }
                for (int i = 0; i < battleground.GetAvailableTilesFrom(side).Count; i++)
                {
                    battleground.GetAvailableTilesFrom(side)[i].SetOccupant(characters[i]);
                }
            }
        }
        else
        {
            this.available = enabled;
        }
    }
}