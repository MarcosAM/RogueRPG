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

        character.GetAttributes().CheckRegenerationAndPoison();
        character.GetAttributes().SpendEffects();
        character.GetInventory().CheckIfEquipsShouldBeRefreshed();

        if (!character.GetAttributes().IsAlive())
        {
            dungeonManager.StartCoroutine(dungeonManager.NextTurn());
            return;
        }

        if (character.Playable)
            playerInputManager.ShowUIFor(currentCharacter);
        else
        {
            var index = character.GetInventory().GetUsableEquips()[Random.Range(0, character.GetInventory().GetUsableEquips().Count)];
            character.GetInventory().GetAvailableEquips()[index] = false;
            character.GetInventory().GetEquips()[index].UseEquipment(character, this, index);
        }
    }
    public virtual void UseEquip(int equipIndex, Tile target, int skill)
    {
        currentCharacter.GetInventory().GetAvailableEquips()[equipIndex] = false;
        currentCharacter.GetInventory().GetEquips()[equipIndex].UseEquipmentOn(currentCharacter, target, this, skill);
    }

    public virtual void ResumeFromEquipment()
    {
        dungeonManager.StartCoroutine(dungeonManager.NextTurn());
        //TODO eu não sei se é muito seguro isso, talvez possa buggar com várias pessoas pedindo para essa courotine começar
    }
}