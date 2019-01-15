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
    SkillDescription skillDescription;

    void Awake()
    {
        equipTogglerManager = FindObjectOfType<EquipToggleManager>();
        combHUDManager = FindObjectOfType<TargetBtnsManager>();
        turnManager = FindObjectOfType<TurnManager>();
        skillDescription = FindObjectOfType<SkillDescription>();

        skillDescription.HideDescription();
        equipTogglerManager.HideEquipToggles();
        skillToggleManager = FindObjectOfType<SkillToggleManager>();
        skillToggleManager.HideSkillToggleMananger();
    }

    public void ShowUIFor(Character currentCharacter)
    {
        this.currentCharacter = currentCharacter;
        equipTogglerManager.ShowEquipTogglesFor(this.currentCharacter.GetInventory());
    }

    public void ReportNewSelectedEquipToggle(EquipToggle equipToggle)
    {
        if (equipToggle != null)
        {
            currentCharacter.ChangeEquipObject(equipTogglerManager.GetSelectedEquipIndex());
            skillToggleManager.ShowSkillTogglesFor(SelectedEquip);
        }
        else
        {
            FindObjectOfType<SkillToggleManager>().HideSkillToggleMananger();
        }
    }

    public void ReportNewSelectedSkillToggle(SkillToggle skillToggle)
    {
        if (skillToggle != null)
        {
            combHUDManager.ShowTargetBtns(currentCharacter, SelectedSkill);
            if (skillDescription)
                skillDescription.UpdateDescription(SelectedSkill.GetDescription());
            else
                print("Não tem skill description! o-o");
        }
        else
        {
            combHUDManager.HideTargetBtns();
        }
    }

    public void ReturnEquipAndTarget(Tile target)
    {
        skillDescription.HideDescription();
        combHUDManager.HideTargetBtns();
        turnManager.UseEquip(equipTogglerManager.GetSelectedEquipIndex(), target, skillToggleManager.GetSelectedSkillIndex());
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