using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

	private CombatBehavior combatBehavior;
	private EquipToggleManager equipTogglerManager;
	private CombHUDManager combHUDManager;

	void Awake(){
		equipTogglerManager = FindObjectOfType<EquipToggleManager> ();
		combHUDManager = FindObjectOfType<CombHUDManager> ();
	}

	public void showUIFor(CombatBehavior combatBehavior){
		this.combatBehavior = combatBehavior;
		equipTogglerManager.showEquipTogglesFor (combatBehavior.getCharacter(), false);
	}

	public void reportNewSelectedEquipToggle(EquipToggle equipToggle){
		if (equipToggle != null) {
			combHUDManager.ShowTargetBtns (combatBehavior.getCharacter (), combatBehavior.getCharacter ().getSkills () [equipTogglerManager.getSelectedEquipIndex ()], false);
			FindObjectOfType<SkillPreviewManager> ().showSkillPreviewsOf (combatBehavior.getCharacter ().getSkills () [equipTogglerManager.getSelectedEquipIndex ()]);
		} else {
			combHUDManager.HideTargetBtns (false);
		}
	}

	public void returnEquipAndTarget(Battleground.Tile target){
		combHUDManager.HideTargetBtns (false);
		combatBehavior.useEquip (equipTogglerManager.getSelectedEquipIndex(), target);
		equipTogglerManager.setAllEquipTogglesOff ();
	}
}