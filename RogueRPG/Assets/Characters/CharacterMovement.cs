using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IMovable
{

    [SerializeField] Character character;
    [SerializeField] Battleground battleground;

    public void MoveTo(int destination)
    {
        if (battleground == null)
        {
            battleground = FindObjectOfType<Battleground>();
            //battleground.MoveCharacterTo(character, destination);
        }
        battleground.MoveCharacterTo(character, destination);
    }

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public int getPosition()
    {
        if (battleground == null)
        {
            battleground = FindObjectOfType<Battleground>();
        }
        return battleground.getPositionOf(character);
    }

    public Battleground.Tile GetTile()
    {
        if (battleground == null)
        {
            battleground = FindObjectOfType<Battleground>();
        }
        return battleground.GetTile(character);
    }
}