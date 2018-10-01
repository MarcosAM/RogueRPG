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
        equipTogglerManager.ShowEquipTogglesFor(combatBehavior.getCharacter(), false);
        combHUDManager.startTurnOf(combatBehavior.getCharacter());
    }

    public void ReportNewSelectedEquipToggle(EquipToggle equipToggle)
    {
        if (equipToggle != null)
        {
            battleGuide.setText("CHOOSE YOUR TARGET");
            battleGuide.setAnimatorTrigger("PointLeftRight");
            combatBehavior.getCharacter().changeEquipObject(combatBehavior.getCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetBackEquip(), combatBehavior.getCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()].GetFrontEquip());
            combatBehavior.getCharacter().getHUD().getAnimator().SetBool("Equiped", true);
            combatBehavior.getCharacter().getHUD().getAnimator().SetTrigger("ChangeEquip");
            combHUDManager.ShowTargetBtns(combatBehavior.getCharacter(), SelectedEquip, false);
            skillPreviewManager.showSkillPreviewsOf(combatBehavior.getCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()]);
        }
        else
        {
            battleGuide.setText("CHOOSE YOUR EQUIPMENT");
            battleGuide.setAnimatorTrigger("PointDown");
            combatBehavior.getCharacter().getHUD().getAnimator().SetBool("Equiped", false);
            combHUDManager.HideTargetBtns(false);
            skillPreviewManager.hideSkillPreviews();
            combHUDManager.startTurnOf(combatBehavior.getCharacter());
        }
    }

    public void ReturnEquipAndTarget(Battleground.Tile target)
    {
        battleGuide.gameObject.SetActive(false);
        combHUDManager.HideTargetBtns(false);
        combatBehavior.UseEquip(equipTogglerManager.GetSelectedEquipIndex(), target);
        equipTogglerManager.SetAllEquipTogglesOff();
        equipTogglerManager.HideEquipToggles();
    }

    public void HoverTargetBtnEnter(TargetBtn targetBtn)
    {
        if (equipTogglerManager.AnyToggleOne())
            combHUDManager.PreviewTargets(combatBehavior.getCharacter(), SelectedEquip, targetBtn.getTile());
    }

    public void HoverTargetBtnExit(TargetBtn targetBtn)
    {
        if (equipTogglerManager.AnyToggleOne())
            combHUDManager.ShowTargetBtns(combatBehavior.getCharacter(), SelectedEquip, false);
    }

    public Equip SelectedEquip
    {
        get
        {
            return combatBehavior.getCharacter().getEquips()[equipTogglerManager.GetSelectedEquipIndex()];
        }
    }
}