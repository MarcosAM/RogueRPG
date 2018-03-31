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

	void RefreshSelf(Combatant c){
		Appear ();
		skill = c.getSkills()[number];
	}

	void ChooseSkill(){
		EventManager.ClickedSkillBtn(skill);
	}

	override public void Appear (){
		button.interactable = true;
		text.text = skill.getSkillName();
		text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
	}

	override public void Disappear(){
		button.interactable = false;
		text.color = new Color (text.color.r,text.color.g,text.color.b,0f);
	}

	void OnEnable(){
		EventManager.OnShowSkillsOf += RefreshSelf;
		EventManager.OnClickedSkillBtn += Disappear;
	}

	void OnDisable(){
		EventManager.OnShowSkillsOf -= RefreshSelf;
		EventManager.OnClickedSkillBtn -= Disappear;
	}
}
