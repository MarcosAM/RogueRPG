using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : CombatBtn {

	Text text;
	[SerializeField]Combatant combatant;

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

	public void Appear (Combatant user,Skill.Targets targets){
		//TODO Só aparecer quando tiver alvo para poder aparecer
		if(combatant.isAlive()){
			switch(targets){
				case Skill.Targets.Allies:
				if(combatant.getIsHero()){
					button.interactable = true;
					text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
				}
				break;
				case Skill.Targets.Enemies:
				if(!combatant.getIsHero()){
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
	}

	void OnDisable(){
		EventManager.OnShowTargetsOf -= Appear;
		EventManager.OnClickedTargetBtn -= Disappear;
	}
}
