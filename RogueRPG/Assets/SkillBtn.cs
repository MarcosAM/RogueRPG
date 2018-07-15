using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillBtn : CombatBtn, IPointerEnterHandler, IPointerExitHandler {

	Text text;
	public Skill skill;

	void Awake () {
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text>();
		Disappear();
		button.onClick.AddListener (onClick);
	}

	public void RefreshSelf (Character c)
	{
		skill = c.getSkills () [number];
		if (skill.getCharactersThatCantUseMe ().Contains (c)) {
			button.interactable = false;
			text.text = skill.getSkillName();
			text.color = new Color (text.color.r,text.color.g,text.color.b,1f);
		} else {
			Appear ();
		}
	}

	void onClick (){
		CombHUDManager.getInstance().onSkillBtnPressed(this);
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

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		CombHUDManager.getInstance ().onSkillBtnHoverEnter (this);
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
		CombHUDManager.getInstance ().onSkillBtnHoverExit (this);
	}

	public Skill getSkill() {return skill;}

	void OnEnable(){
//		EventManager.OnShowSkillsOf += RefreshSelf;
		EventManager.OnClickedSkillBtn += Disappear;
	}

	void OnDisable(){
//		EventManager.OnShowSkillsOf -= RefreshSelf;
		EventManager.OnClickedSkillBtn -= Disappear;
	}
}
