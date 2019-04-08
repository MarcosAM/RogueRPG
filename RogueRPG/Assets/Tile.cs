using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    Character character;
    [SerializeField] int index;
    [SerializeField] bool side;
    bool available = true;
    Battleground battleground;
    [SerializeField] RectTransform portraitHandler;
    Image shadow;

    public void Initialize(Battleground battleground)
    {
        this.battleground = battleground;
        this.index = battleground.GetTiles().IndexOf(this);
        this.side = this.index >= (battleground.GetTiles().Count / 2) ? true : false;
        this.shadow = GetComponentInChildren<Image>();
    }

    public Character GetCharacter() { return character; }

    public void MoveCharacter(Character character)
    {
        if (character.Playable != side)
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
            if (!this.character.Playable)
            {
                //TODO organize all of this. Look for quarteion and vector3 e vector 2
                this.character.transform.rotation = Quaternion.Euler(0, -180, 0);
                this.GetCharacter().GetAvatarImg().transform.localScale = new Vector3(-0.8f, 0.8f, -0.8f);
            }
        }
    }

    public bool CharacterIs(bool alive) { return character != null ? character.GetAttributes().IsAlive() == alive : false; }

    public int GetIndex() { return index; }
    public int GetRow() { return index % (battleground.GetTiles().Count / 2); }
    public bool GetSide() { return side; }
    public List<Tile> GetAlliesTiles() { return battleground.GetAvailableTilesFrom(side); }
    public List<Tile> GetEnemiesTiles() { return battleground.GetAvailableTilesFrom(!side); }
    public Vector2 GetLocalPosition() { return transform.localPosition; }
    public Tile GetTileInFront() { return battleground.GetTiles().Find(t => t.GetSide() != side && t.GetRow() == GetRow()); }

    public bool IsAvailable() { return available; }

    public void SetA(bool available)
    {
        shadow.gameObject.SetActive(available);

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