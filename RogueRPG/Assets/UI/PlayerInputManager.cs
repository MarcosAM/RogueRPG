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

	public void showTargetsBtn(){
		combHUDManager.ShowTargetBtns (combatBehavior.getCharacter(),combatBehavior.getCharacter().getSkills()[equipTogglerManager.getSelectedEquipIndex()],false);
	}

	public void returnEquipAndTarget(Battleground.Tile target){
		if(combatBehavior == null){
			print ("É nulo");
		}
		combatBehavior.useEquip (equipTogglerManager.getSelectedEquipIndex(), target);
	}
}