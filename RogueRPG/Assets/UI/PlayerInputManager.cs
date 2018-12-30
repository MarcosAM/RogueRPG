using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{

    private CombatBehavior combatBehavior;
    private EquipToggleManager equipTogglerManager;
    private TargetBtnsManager combHUDManager;
    SkillToggleManager skillToggleManager;
    private BattleGuide battleGuide;

    void Awake()
    {
        equipTogglerManager = FindObjectOfType<EquipToggleManager>();
        combHUDManager = FindObjectOfType<TargetBtnsManager>();
        battleGuide = FindObjectOfType<BattleGuide>();
        equipTogglerManager.HideEquipToggles();
        skillToggleManager = FindObjectOfType<SkillToggleManager>();
        skillToggleManager.HideSkillToggleMananger();
    }

    public void ShowUIFor(CombatBehavior combatBehavior)
    {
        battleGuide.gameObject.SetActive(true);
        this.combatBehavior = combatBehavior;
        equipTogglerManager.ShowEquipTogglesFor(combatBehavior.GetCharacter(), false);
    }

    public void ReportNewSelectedEquipToggle(EquipToggle equipToggle)
    {
        if (equipToggle != null)
        {
            battleGuide.setText("CHOOSE YOUR TARGET");
            battleGuide.setAnimatorTrigger("PointLeftRight");
            combatBehavior.GetCharacter().changeEquipObject(combatBehavior.GetCharacter().GetEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetBackEquip(), combatBehavior.GetCharacter().GetEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetFrontEquip());
            combatBehavior.GetCharacter().getAnimator().SetBool("Equiped", true);
            combatBehavior.GetCharacter().getAnimator().SetTrigger("ChangeEquip");
            skillToggleManager.ShowSkillTogglesFor(SelectedEquip);
        }
        else
        {
            battleGuide.setText("CHOOSE YOUR EQUIPMENT");
            battleGuide.setAnimatorTrigger("PointDown");
            combatBehavior.GetCharacter().getAnimator().SetBool("Equiped", false);
            FindObjectOfType<SkillToggleManager>().HideSkillToggleMananger();
        }
    }

    public void ReportNewSelectedSkillToggle(SkillToggle skillToggle)
    {
        if (skillToggle != null)
        {
            combHUDManager.ShowTargetBtns(combatBehavior.GetCharacter(), SelectedSkill);
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
        combatBehavior.UseEquip(equipTogglerManager.GetSelectedEquipIndex(), target, false, skillToggleManager.GetSelectedSkillIndex());
        equipTogglerManager.SetAllEquipTogglesOff();
        equipTogglerManager.HideEquipToggles();
    }

    public void HoverTargetBtnEnter(TargetBtn targetBtn)
    {
        if (skillToggleManager.AnyToggleOne())
            combHUDManager.PreviewTargets(combatBehavior.GetCharacter(), SelectedSkill, targetBtn.getTile());
    }

    public void HoverTargetBtnExit(TargetBtn targetBtn)
    {
        if (skillToggleManager.AnyToggleOne())
            combHUDManager.ShowTargetBtns(combatBehavior.GetCharacter(), SelectedSkill);
    }

    public Equip SelectedEquip
    {
        get
        {
            return combatBehavior.GetCharacter().GetEquips()[equipTogglerManager.GetSelectedEquipIndex()];
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