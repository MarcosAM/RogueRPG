using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour {

	private CombatBehavior combatBehavior;
	private EquipToggleManager equipTogglerManager;
	private CombHUDManager combHUDManager;
	private SkillPreviewManager skillPreviewManager;
	private BattleGuide battleGuide;

	void Awake(){
		equipTogglerManager = FindObjectOfType<EquipToggleManager> ();
		combHUDManager = FindObjectOfType<CombHUDManager> ();
		skillPreviewManager = FindObjectOfType<SkillPreviewManager> ();
		battleGuide = FindObjectOfType<BattleGuide> ();
		equipTogglerManager.hideEquipToggles();
		skillPreviewManager.hideSkillPreviews();
	}

	public void showUIFor(CombatBehavior combatBehavior){
		battleGuide.gameObject.SetActive(true);
		this.combatBehavior = combatBehavior;
		equipTogglerManager.showEquipTogglesFor (combatBehavior.getCharacter(), false);
		combHUDManager.startTurnOf(combatBehavior.getCharacter());
	}

	public void reportNewSelectedEquipToggle(EquipToggle equipToggle){
		if (equipToggle != null) {
			battleGuide.setText("CHOOSE YOUR TARGET");
			battleGuide.setAnimatorTrigger("PointLeftRight");
			combatBehavior.getCharacter().changeEquipmentSprite(combatBehavior.getCharacter().getSkills()[equipTogglerManager.getSelectedEquipIndex()].getSprite());
			combatBehavior.getCharacter().getHUD().getAnimator().SetTrigger("ChangeEquip");
			combatBehavior.getCharacter().getHUD().getAnimator().SetBool("Equiped",true);
			combHUDManager.ShowTargetBtns (combatBehavior.getCharacter (), combatBehavior.getCharacter ().getSkills () [equipTogglerManager.getSelectedEquipIndex ()], false);
			skillPreviewManager.showSkillPreviewsOf (combatBehavior.getCharacter ().getSkills () [equipTogglerManager.getSelectedEquipIndex ()]);
		} else {
			battleGuide.setText("CHOOSE YOUR EQUIPMENT");
			battleGuide.setAnimatorTrigger("PointDown");
			combatBehavior.getCharacter().getHUD().getAnimator().SetBool("Equiped",false);
			combHUDManager.HideTargetBtns (false);
			skillPreviewManager.hideSkillPreviews();
			combHUDManager.startTurnOf(combatBehavior.getCharacter());
		}
	}

	public void returnEquipAndTarget(Battleground.Tile target){
		battleGuide.gameObject.SetActive(false);
		combHUDManager.HideTargetBtns (false);
		combatBehavior.useEquip (equipTogglerManager.getSelectedEquipIndex(), target);
		equipTogglerManager.setAllEquipTogglesOff ();
		equipTogglerManager.hideEquipToggles ();
	}
}