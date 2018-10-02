using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : CombatBehavior
{

    public override void StartTurn()
    {
        base.StartTurn();
        character.StartTurn();
        choosedEquip = null;
        targetTile = null;
        if (character.IsPlayable())
            FindObjectOfType<PlayerInputManager>().ShowUIFor(this);
        else
        {
            choosedEquip = character.getUsableEquips()[Random.Range(0, character.getUsableEquips().Count)];
            targetTile = choosedEquip.GetBestTarget(character);
            choosedEquip.UseEquipmentOn(character, targetTile, this);
        }
    }

    public override void UseEquip(int equip, Battleground.Tile target)
    {
        base.UseEquip(equip, target);
        availableEquips[equip] = false;
        character.getEquips()[equip].UseEquipmentOn(character, target, this);
    }

    public override void ResumeFromEquipment()
    {
        character.getHUD().getAnimator().SetBool("Equiped", false);
        EndTurn();
    }

    void EndTurn()
    {
        choosedEquip = null;
        character.EndTurn();
    }
}
