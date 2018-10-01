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

    public void checkForNextStep()
    {
        if (choosedEquip == null)
        {
            FindObjectOfType<EquipToggleManager>().ShowEquipTogglesFor(character, false);
        }
        else
        {
            CombHUDManager.getInstance().HideSkillsBtn();
            if (targetTile == null)
            {
                CombHUDManager.getInstance().ShowTargetBtns(character, choosedEquip, false);
            }
            else
            {
                CombHUDManager.getInstance().HideTargetBtns(false);
                //UseSkill();
            }
        }
    }

    public void ChooseTarget()
    {
        EventManager.OnPlayerChoosedTarget2 += ReadyTarget;

        //TODO ALTERAR ISSO AQUI LOLOLOLOLO
        EventManager.ShowTargetsOf(character, choosedEquip);
    }

    public void ReadyTarget(Battleground.Tile tile)
    {
        this.targetTile = tile;
        character.getHUD().UseSkillAnimation();
    }

    public void UseSkillAnimation()
    {
        character.getHUD().UseSkillAnimation();
    }

    public override void UseEquip(int equip, Battleground.Tile target)
    {
        base.UseEquip(equip, target);
        availableEquips[equip] = false;
        character.getEquips()[equip].UseEquipmentOn(character, target, this);
    }

    public override void resumeFromEquipment()
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
