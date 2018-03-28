using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : CombatBtn {

	Text text;
	public Skill skill;

	void Awake () {
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text>();
		text.text = skill.getSkillName();
		Disappear();
		button.onClick.AddListener (ChooseSkill);
	}

	override public void Disappear(){
		button.interactable = false;
		text.color = new Color (text.color.r,text.color.g,text.color.b,0f);
	}

	override public void Appear (){
		button.interactable = true;
		text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
	}

	void RefreshSelf(Combatant c){
		skill = c.getSkills()[number];
		text.text = skill.getSkillName();
		Appear ();
	}

	void ChooseSkill(){
		EventManager.selectedSkill = skill;
		EventManager.ChooseSkill();
	}

	void OnEnable(){
		EventManager.OnClickedSkillBtn += Disappear;
		EventManager.OnHeroTurnStarted += RefreshSelf;
	}

	void OnDisable(){
		EventManager.OnClickedSkillBtn -= Disappear;
		EventManager.OnHeroTurnStarted -= RefreshSelf;
	}
}
