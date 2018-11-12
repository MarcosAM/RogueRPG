using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Character occupant;
    int index;
    bool fromHero;
    bool enabled;
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
    public void Initialize(int index, bool fromHero, Battleground battleground, bool enabled)
    {
        this.index = index;
        this.fromHero = fromHero;
        this.battleground = battleground;
        this.enabled = enabled;
    }
    public void setOccupant(Character occupant) { this.occupant = occupant; }
    public Character getOccupant() { return occupant; }
    public int GetRow() { return index % (battleground.GetTiles().Count / 2); }
    public Vector2 getLocalPosition()
    {
        return FindObjectOfType<CombHUDManager>().GetTileUIs()[index].getRectTransform().localPosition;
    }
    public bool isFromHero() { return fromHero; }
    public Tile[] GetAlliesTiles() { return fromHero ? battleground.getHeroesTiles() : battleground.getEnemiesTiles(); }
    public Tile[] GetEnemiesTiles() { return fromHero ? battleground.getEnemiesTiles() : battleground.getHeroesTiles(); }
    public int GetIndex() { return index; }
    public bool IsEnabled() { return enabled; }
}