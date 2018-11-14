using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Character occupant;
    int index;
    bool fromHero;
    [SerializeField] bool enabled;
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
        this.enabled = enabled;
    }
    public void SetOccupant(Character occupant)
    {
        if (this.occupant != null)
        {
            foreach (Transform transform in portraitHandler.transform)
            {
                transform.SetParent(null);
            }
        }
        if (!fromHero)
        {
            portraitHandler.localScale = new Vector3(-1, 1, 1);
        }
        this.occupant = occupant;
        if (occupant != null)
        {
            this.occupant.transform.SetParent(portraitHandler);
            this.occupant.transform.localPosition = new Vector3(0, 0);
        }
        //this.occupant = occupant;
    }
    public Character getOccupant() { return occupant; }
    public int GetRow() { return index % (battleground.GetTiles().Count / 2); }
    public Vector2 getLocalPosition()
    {
        return transform.localPosition;
        //return FindObjectOfType<CombHUDManager>().GetTileUIs()[index].getRectTransform().localPosition;
    }
    public bool isFromHero() { return fromHero; }
    public Tile[] GetAlliesTiles() { return fromHero ? battleground.GetHeroesTiles() : battleground.GetEnemiesTiles(); }
    public Tile[] GetEnemiesTiles() { return fromHero ? battleground.GetEnemiesTiles() : battleground.GetHeroesTiles(); }
    public int GetIndex() { return index; }
    public bool IsEnabled() { return enabled; }
}