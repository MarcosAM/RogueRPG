using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : CombatBtn {

	public Combatant combatant;

	void Awake () {
		button = GetComponent<Button> ();
		Disappear ();
		button.onClick.AddListener (ChooseTarget);
	}

	override public void Disappear(){
		button.interactable = false;
	}

	override public void Appear (){
		button.interactable = true;
	}

	void ChooseTarget(){
		EventManager.target = combatant;
		EventManager.ChooseTarget ();
	}

	void OnEnable(){
		EventManager.OnClickedTargetBtn += Disappear;
		EventManager.OnClickedSkillBtn += Appear;
		CombatManager.OnTurnEnded += Disappear;
	}

	void OnDisable(){
		EventManager.OnClickedTargetBtn -= Disappear;
		EventManager.OnClickedSkillBtn -= Appear;
		CombatManager.OnTurnEnded -= Disappear;
	}
}
