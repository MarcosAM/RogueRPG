﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    Character currentCharacter;
    TurnManager turnManager;
    private EquipToggleManager equipTogglerManager;
    private TargetBtnsManager combHUDManager;
    SkillToggleManager skillToggleManager;
    private BattleGuide battleGuide;

    void Awake()
    {
        equipTogglerManager = FindObjectOfType<EquipToggleManager>();
        combHUDManager = FindObjectOfType<TargetBtnsManager>();
        battleGuide = FindObjectOfType<BattleGuide>();
        turnManager = FindObjectOfType<TurnManager>();

        equipTogglerManager.HideEquipToggles();
        skillToggleManager = FindObjectOfType<SkillToggleManager>();
        skillToggleManager.HideSkillToggleMananger();
    }

    public void ShowUIFor(Character currentCharacter)
    {
        battleGuide.gameObject.SetActive(true);
        this.currentCharacter = currentCharacter;
        equipTogglerManager.ShowEquipTogglesFor(this.currentCharacter, false);
    }

    public void ReportNewSelectedEquipToggle(EquipToggle equipToggle)
    {
        if (equipToggle != null)
        {
            battleGuide.setText("CHOOSE YOUR TARGET");
            battleGuide.setAnimatorTrigger("PointLeftRight");
            currentCharacter.changeEquipObject(currentCharacter.GetInventory().GetEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetBackEquip(), currentCharacter.GetInventory().GetEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetFrontEquip());
            currentCharacter.getAnimator().SetBool("Equiped", true);
            currentCharacter.getAnimator().SetTrigger("ChangeEquip");
            skillToggleManager.ShowSkillTogglesFor(SelectedEquip);
        }
        else
        {
            battleGuide.setText("CHOOSE YOUR EQUIPMENT");
            battleGuide.setAnimatorTrigger("PointDown");
            currentCharacter.getAnimator().SetBool("Equiped", false);
            FindObjectOfType<SkillToggleManager>().HideSkillToggleMananger();
        }
    }

    public void ReportNewSelectedSkillToggle(SkillToggle skillToggle)
    {
        if (skillToggle != null)
        {
            combHUDManager.ShowTargetBtns(currentCharacter, SelectedSkill);
        }
        else
        {
            combHUDManager.HideTargetBtns();
        }
    }

    public void ReturnEquipAndTarget(Tile target)
    {
        battleGuide.gameObject.SetActive(false);
        combHUDManager.HideTargetBtns();
        //TODO todos os golpes de herois está sendo como momentum
        turnManager.UseEquip(equipTogglerManager.GetSelectedEquipIndex(), target, false, skillToggleManager.GetSelectedSkillIndex());
        equipTogglerManager.SetAllEquipTogglesOff();
        equipTogglerManager.HideEquipToggles();
    }

    public void HoverTargetBtnEnter(TargetBtn targetBtn)
    {
        if (skillToggleManager.AnyToggleOne())
            combHUDManager.PreviewTargets(currentCharacter, SelectedSkill, targetBtn.getTile());
    }

    public void HoverTargetBtnExit(TargetBtn targetBtn)
    {
        if (skillToggleManager.AnyToggleOne())
            combHUDManager.ShowTargetBtns(currentCharacter, SelectedSkill);
    }

    public Equip SelectedEquip
    {
        get
        {
            return currentCharacter.GetInventory().GetEquips()[equipTogglerManager.GetSelectedEquipIndex()];
        }
    }

    public Skill SelectedSkill
    {
        get
        {
            return SelectedEquip.GetSkills()[skillToggleManager.GetSelectedSkillIndex()];
        }
    }
}