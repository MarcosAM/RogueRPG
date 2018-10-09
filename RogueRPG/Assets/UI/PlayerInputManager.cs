using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{

    private CombatBehavior combatBehavior;
    private EquipToggleManager equipTogglerManager;
    private CombHUDManager combHUDManager;
    private SkillPreviewManager skillPreviewManager;
    private BattleGuide battleGuide;

    void Awake()
    {
        equipTogglerManager = FindObjectOfType<EquipToggleManager>();
        combHUDManager = FindObjectOfType<CombHUDManager>();
        skillPreviewManager = FindObjectOfType<SkillPreviewManager>();
        battleGuide = FindObjectOfType<BattleGuide>();
        equipTogglerManager.HideEquipToggles();
        skillPreviewManager.hideSkillPreviews();
    }

    public void ShowUIFor(CombatBehavior combatBehavior)
    {
        battleGuide.gameObject.SetActive(true);
        this.combatBehavior = combatBehavior;
        equipTogglerManager.ShowEquipTogglesFor(combatBehavior.GetCharacter(), false);
        combHUDManager.startTurnOf(combatBehavior.GetCharacter());
    }

    public void ReportNewSelectedEquipToggle(EquipToggle equipToggle)
    {
        if (equipToggle != null)
        {
            battleGuide.setText("CHOOSE YOUR TARGET");
            battleGuide.setAnimatorTrigger("PointLeftRight");
            combatBehavior.GetCharacter().changeEquipObject(combatBehavior.GetCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetBackEquip(), combatBehavior.GetCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetFrontEquip());
            combatBehavior.GetCharacter().getHUD().getAnimator().SetBool("Equiped", true);
            combatBehavior.GetCharacter().getHUD().getAnimator().SetTrigger("ChangeEquip");
            combHUDManager.ShowTargetBtns(combatBehavior.GetCharacter(), SelectedEquip);
            skillPreviewManager.showSkillPreviewsOf(combatBehavior.GetCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()]);
        }
        else
        {
            battleGuide.setText("CHOOSE YOUR EQUIPMENT");
            battleGuide.setAnimatorTrigger("PointDown");
            combatBehavior.GetCharacter().getHUD().getAnimator().SetBool("Equiped", false);
            combHUDManager.HideTargetBtns(false);
            skillPreviewManager.hideSkillPreviews();
            combHUDManager.startTurnOf(combatBehavior.GetCharacter());
        }
    }

    public void ReturnEquipAndTarget(Battleground.Tile target)
    {
        battleGuide.gameObject.SetActive(false);
        combHUDManager.HideTargetBtns(false);
        //TODO todos os golpes de herois está sendo como momentum
        combatBehavior.UseEquip(equipTogglerManager.GetSelectedEquipIndex(), target, false);
        equipTogglerManager.SetAllEquipTogglesOff();
        equipTogglerManager.HideEquipToggles();
    }

    public void HoverTargetBtnEnter(TargetBtn targetBtn)
    {
        if (equipTogglerManager.AnyToggleOne())
            combHUDManager.PreviewTargets(combatBehavior.GetCharacter(), SelectedEquip, targetBtn.getTile());
    }

    public void HoverTargetBtnExit(TargetBtn targetBtn)
    {
        if (equipTogglerManager.AnyToggleOne())
            combHUDManager.ShowTargetBtns(combatBehavior.GetCharacter(), SelectedEquip);
    }

    public Equip SelectedEquip
    {
        get
        {
            return combatBehavior.GetCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()];
        }
    }
}