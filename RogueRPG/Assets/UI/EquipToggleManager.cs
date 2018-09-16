using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipToggleManager : MonoBehaviour {

	private CombatBehavior combatBehavior;
	[SerializeField]private List<EquipToggle> equipToggles = new List<EquipToggle>();
	private PlayerInputManager playerInputManager;
	private ToggleGroup toggleGroup;

	void Awake(){
		playerInputManager = FindObjectOfType<PlayerInputManager> ();
		toggleGroup = GetComponent<ToggleGroup> ();
	}

	public void onAnyToggleChange(EquipToggle equipToggle){
		if(equipToggle.getToggle().isOn){
			playerInputManager.reportNewSelectedEquipToggle(equipToggle);
			return;
		}
		playerInputManager.reportNewSelectedEquipToggle (null);
	}

	public void setAllEquipTogglesOff(){
		toggleGroup.SetAllTogglesOff ();
	}

	public void showEquipTogglesFor(Character character, bool asPreview){
		gameObject.SetActive (true);
		for(int i = 0;i<equipToggles.Count;i++){
			equipToggles[i].getText().text = character.getEquips()[i].getSkillName();
			if (asPreview) {
				equipToggles [i].getToggle ().interactable = false;
			} else {
				if (character.getBehavior().IsEquipAvailable(i)) {
					equipToggles [i].getToggle ().interactable = true;
				} else {
					equipToggles [i].getToggle ().interactable = false;
				}
			}
		}
	}

	public int getSelectedEquipIndex(){
		for(int i = 0;i<equipToggles.Count;i++){
			if (equipToggles [i].getToggle ().isOn) {
				return i;
			}
		}
		return 1;
	}

	public void hideEquipToggles(){
		gameObject.SetActive (false);
	}
}