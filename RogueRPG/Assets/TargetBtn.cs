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

	void ChooseTarget(){
		EventManager.ChooseTarget (combatant);
	}

	override public void Appear (){
		button.interactable = true;
	}

	override public void Disappear(){
		button.interactable = false;
	}

	void OnEnable(){
		EventManager.OnShowTargetsOf += Appear;
		EventManager.OnClickedTargetBtn += Disappear;
	}

	void OnDisable(){
		EventManager.OnShowTargetsOf -= Appear;
		EventManager.OnClickedTargetBtn -= Disappear;
	}
}
