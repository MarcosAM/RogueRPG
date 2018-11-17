using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Character occupant;
    int index;
    bool fromHero;
    [SerializeField] bool available;
    Battleground battleground;
    Tile[] side;
    Tile[] otherSide;
    [SerializeField] RectTransform portraitHandler;

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
    public void Initialize(int index, bool fromHero, Battleground battleground, bool enabled)
    {
        this.index = index;
        this.fromHero = fromHero;
        this.battleground = battleground;
        this.available = enabled;
        if (!fromHero)
        {
            portraitHandler.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void SetOccupant(Character occupant)
    {
        if (occupant != null)
        {
            if (occupant.IsPlayable() != fromHero)
            {
                battleground.GetTiles().Find(t => t.isFromHero() != fromHero && t.GetRow() == GetRow()).SetOccupant(occupant);
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
                oldTile.SetOccupant(this.occupant);
            }
        }

        this.occupant = occupant;
        if (occupant != null)
        {
            this.occupant.transform.SetParent(portraitHandler);
            this.occupant.transform.localPosition = new Vector3(0, 0);
        }
        //battleground.ShowCharactersToThePlayer();
    }
    public Character getOccupant() { return occupant; }
    public int GetRow() { return index % (battleground.GetTiles().Count / 2); }
    public Vector2 getLocalPosition()
    {
        return transform.localPosition;
    }
    public bool isFromHero() { return fromHero; }
    public List<Tile> GetAlliesTiles() { return battleground.GetAvailableTilesFrom(fromHero); }
    public List<Tile> GetEnemiesTiles() { return battleground.GetAvailableTilesFrom(!fromHero); }
    public int GetIndex() { return index; }
    public bool IsEnabled() { return available; }
    public Battleground GetBattleground() { return battleground; }

    public void SetEnabled(bool enabled)
    {
        if (!enabled && this.available != enabled)
        {
            if (occupant == null)
            {
                this.available = enabled;
            }
            else
            {
                List<Character> characters = new List<Character>();
                var mySide = battleground.GetAvailableTilesFrom(fromHero);
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
                for (int i = 0; i < battleground.GetAvailableTilesFrom(fromHero).Count; i++)
                {
                    battleground.GetAvailableTilesFrom(fromHero)[i].SetOccupant(characters[i]);
                }
            }
        }
        else
        {
            this.available = enabled;
        }
    }
}