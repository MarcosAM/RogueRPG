using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : CombatBtn {

	Text text;
	Combatant combatant;

	void Awake () {
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text>();
		Disappear ();
		button.onClick.AddListener (ChooseTarget);
	}

	void ChooseTarget(){
		EventManager.ChooseTarget (combatant);
	}

	public void Initialize (Combatant c){
		combatant = c;
		text.text = c.getName();
	}

	override public void Appear (){
		button.interactable = true;
	}

	override public void Disappear(){
		button.interactable = false;
		text.color = new Color (text.color.r,text.color.g,text.color.b,0f);
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
