using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : CombatBtn {

	Text text;
	[SerializeField]Character combatant;

	void Awake () {
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text>();
		Disappear ();
		button.onClick.AddListener (ChooseTarget);
	}

	void ChooseTarget(){
		EventManager.ChooseTarget (combatant);
	}

	public void Initialize (Character c){
		combatant = c;
		text.text = c.getName();
		combatant.OnMyTurnStarts += ActiveCombatantOn;
		combatant.OnMyTurnEnds += ActiveCombatantOff;
	}

	public void ActiveCombatantOn (){
		text.color = Color.blue;
	}

	public void ActiveCombatantOff(){
		text.color = new Color (0.2f,0.2f,0.2f,1f);
	}

	public void Appear (Character user,Skill skill){
		//TODO Só aparecer quando tiver alvo para poder aparecer
		if(combatant.isAlive()){
			switch(skill.getTargets()){
				case Skill.Targets.Allies:
				if(combatant.isPlayable() && Mathf.Abs(combatant.getPosition()-user.getPosition())<=skill.getRange()){
					button.interactable = true;
					text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
				}
				break;
				case Skill.Targets.Enemies:
				if(!combatant.isPlayable() && Mathf.Abs(combatant.getPosition()-user.getPosition())<=skill.getRange()){
					button.interactable = true;
					text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
				}
				break;
				case Skill.Targets.Self:
				if(combatant==user){
					button.interactable = true;
					text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
				}
				break;
				case Skill.Targets.Location:
				break;
				default:
				button.interactable = true;
				text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
				break;
			}
		}
	}

	override public void Disappear(){
		button.interactable = false;
	}

	void OnEnable(){
		EventManager.OnShowTargetsOf += Appear;
		EventManager.OnClickedTargetBtn += Disappear;
		EventManager.OnUnchoosedSkill += Disappear;
	}

	void OnDisable(){
		EventManager.OnShowTargetsOf -= Appear;
		EventManager.OnClickedTargetBtn -= Disappear;
		EventManager.OnUnchoosedSkill -= Disappear;
		if(combatant != null){
			combatant.OnMyTurnStarts += ActiveCombatantOn;
			combatant.OnMyTurnEnds += ActiveCombatantOff;
		}
	}
}
