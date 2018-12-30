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
        if (!this.side)
            portraitHandler.localScale = new Vector3(-1, 1, 1);
    }

    public Character GetCharacter() { return character; }

    public void MoveCharacter(Character character)
    {
        if (character.IsPlayable() != side)
        {
            GetTileInFront().MoveCharacter(character);
            return;
        }

        if (this.character == character)
            return;

        if (this.character)
        {
            character.GetTile().SetC(this.character);
        }

        SetC(character);
    }

    public void SetC(Character character)
    {
        if (this.character == character)
            return;

        if (this.character)
        {
            this.character.SetTile(null);

            foreach (Transform transform in portraitHandler.transform)
            {
                transform.SetParent(null);
            }
        }

        if (character)
        {
            if (character.GetTile())
                if (character.GetTile().GetCharacter() == character)
                {
                    character.GetTile().SetC(null);
                }
        }

        this.character = character;

        if (this.character)
        {
            this.character.SetTile(this);
            this.character.transform.SetParent(portraitHandler);
            this.character.transform.localPosition = new Vector3(0, 0);
        }
    }

    //public void SetCharacter(Character character)
    //{
    //    if (character != null)
    //    {
    //        if (character.IsPlayable() != side)
    //        {
    //            GetTileInFront().SetCharacter(character);
    //            return;
    //        }
    //    }

    //    foreach (Transform transform in portraitHandler.transform)
    //    {
    //        transform.SetParent(null);
    //    }

    //    if (character != null)
    //    {
    //        Tile oldTile = character.GetTile();
    //        if (oldTile != null)
    //        {
    //            oldTile.SetCharacter(null);
    //            oldTile.SetCharacter(this.character);
    //        }
    //    }

    //    this.character = character;
    //    if (this.character != null)
    //        this.character.SetTile(this);

    //    if (character != null)
    //    {
    //        this.character.transform.SetParent(portraitHandler);
    //        this.character.transform.localPosition = new Vector3(0, 0);
    //    }
    //}
    public bool CharacterIs(bool alive) { return character != null ? character.IsAlive() == alive : false; }

    public int GetIndex() { return index; }
    public int GetRow() { return index % (battleground.GetTiles().Count / 2); }
    public bool GetSide() { return side; }
    public List<Tile> GetAlliesTiles() { return battleground.GetAvailableTilesFrom(side); }
    public List<Tile> GetEnemiesTiles() { return battleground.GetAvailableTilesFrom(!side); }
    public Vector2 GetLocalPosition() { return transform.localPosition; }
    public Tile GetTileInFront() { return battleground.GetTiles().Find(t => t.GetSide() != side && t.GetRow() == GetRow()); }

    public bool IsAvailable() { return available; }

    //public void SetAvailable(bool available)
    //{
    //    if (!available && this.available != available)
    //    {
    //        if (character == null)
    //        {
    //            this.available = available;
    //        }
    //        else
    //        {
    //            List<Character> characters = new List<Character>();
    //            var mySide = battleground.GetAvailableTilesFrom(side);
    //            foreach (Tile tile in mySide)
    //            {
    //                characters.Add(tile.GetCharacter());
    //                tile.SetCharacter(null);
    //            }
    //            this.available = available;
    //            for (int i = 0; i < characters.Count; i++)
    //            {
    //                if (characters[i] != null && !mySide[i].IsAvailable())
    //                {
    //                    characters.RemoveAt(characters.FindIndex(c => c == null));
    //                    break;
    //                }
    //            }
    //            for (int i = 0; i < battleground.GetAvailableTilesFrom(side).Count; i++)
    //            {
    //                battleground.GetAvailableTilesFrom(side)[i].SetCharacter(characters[i]);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        this.available = available;
    //    }
    //}

    public void SetA(bool available)
    {
        if (!available && this.available != available)
        {
            if (this.character)
            {
                List<Tile> availableTilesFromMySide = battleground.GetAvailableTilesFrom(side);
                List<Character> characters = availableTilesFromMySide.ConvertAll<Character>(t => t.GetCharacter());

                if (availableTilesFromMySide.Count - 1 >= characters.FindAll(c => c).Count)
                {
                    var index = availableTilesFromMySide.IndexOf(this);

                    if (index != 0 && index != availableTilesFromMySide.Count - 1)
                        return;

                    var orientationDown = index == 0;

                    this.available = available;
                    SetC(null);

                    if (orientationDown)
                    {
                        for (int i = 0; i < characters.Count; i++)
                        {
                            if (!characters[i])
                            {
                                characters.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = characters.Count - 1; i >= 0; i--)
                        {
                            if (!characters[i])
                            {
                                characters.RemoveAt(i);
                                break;
                            }
                        }
                    }

                    List<Tile> newAvailableTilesFromMySide = battleground.GetAvailableTilesFrom(side);

                    for (int i = 0; i < newAvailableTilesFromMySide.Count; i++)
                    {
                        newAvailableTilesFromMySide[i].SetC(characters[i]);
                    }
                }
                else
                    return;
            }
        }

        this.available = available;
    }

}