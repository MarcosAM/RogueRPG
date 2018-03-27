using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour {

	public int number;
	Button btn;

	Text text;
	public Skill skill;

	void Start () {
		btn = GetComponent<Button> ();
		Disable();
		text = GetComponentInChildren<Text>();
		text.text = skill.getSkillName();
		btn.onClick.AddListener (ChooseSkill);
	}

	void Disable(){
		btn.interactable = false;
	}

	void Enable (){
		btn.interactable = true;
	}

	void RefreshSelf(){
		skill = CombatManager.initiativeList[CombatManager.activeCombatant%CombatManager.initiativeList.Length].skills[number];
		text.text = skill.getSkillName();
	}

	void ChooseSkill(){
		EventManager.selectedSkill = skill;
		EventManager.ChooseSkill();
	}

	void OnEnable(){
		EventManager.OnClickedSkillBtn += Disable;
		EventManager.OnClickedTargetBtn += Enable;
		CombatManager.OnTurnEnded += Enable;
		CombatManager.OnHeroTurnBegin += Enable;
		CombatManager.OnHeroTurnBegin += RefreshSelf;
	}

	void OnDisable(){
		EventManager.OnClickedSkillBtn -= Disable;
		EventManager.OnClickedTargetBtn -= Enable;
		CombatManager.OnTurnEnded -= Enable;
		CombatManager.OnHeroTurnBegin -= Enable;
		CombatManager.OnHeroTurnBegin -= RefreshSelf;
	}
}
