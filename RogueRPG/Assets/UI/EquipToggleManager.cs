using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipToggleManager : MonoBehaviour {

	private CombatBehavior combatBehavior;
	[SerializeField]private List<EquipToggle> equipToggles = new List<EquipToggle>();

	public void onAnyToggleChange(EquipToggle equipToggle){
		if(equipToggle.getToggle().isOn){
			combatBehavior.skillBtnPressed (equipToggles.IndexOf(equipToggle));
		}
	}

	public void showEquipTogglesFor(Character character, bool asPreview){
		for(int i = 0;i<equipToggles.Count;i++){
			equipToggles[i].getText().text = character.getSkills()[i].getSkillName();
			if (asPreview) {
				equipToggles [i].getToggle ().interactable = false;
			} else {
				equipToggles [i].getToggle ().interactable = true;
			}
		}
	}
}