using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour, IWaitForEquipment
{
    Character currentCharacter;
    DungeonManager dungeonManager;
    PlayerInputManager playerInputManager;

    void Awake()
    {
        dungeonManager = GetComponent<DungeonManager>();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    public virtual void StartTurn(Character character)
    {
        this.currentCharacter = character;

        character.GetAttributes().SpendBuffs();
        character.GetInventory().CheckIfEquipsShouldBeRefreshed();

        if (character.IsPlayable())
            playerInputManager.ShowUIFor(currentCharacter);
        else
        {
            var index = Random.Range(0, character.GetInventory().GetUsableEquips().Count);
            character.GetInventory().GetUsableEquips()[index].UseEquipment(character, this, character.GetInventory().IsMomentumEquip(character.GetInventory().GetUsableEquips()[index]));
        }
    }
    public virtual void UseEquip(int equipIndex, Tile target, bool momentum, int skill)
    {
        currentCharacter.GetInventory().GetAvailableEquips()[equipIndex] = false;
        currentCharacter.GetInventory().GetEquips()[equipIndex].UseEquipmentOn(currentCharacter, target, this, momentum, skill);
    }

    public virtual void ResumeFromEquipment()
    {
        currentCharacter.GetAnimator().SetBool("Equiped", false);
        dungeonManager.NextTurn();
    }
}