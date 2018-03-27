using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour {

	public int number;
	Button btn;
	EventManager.ChoosedSkill cs;

	Text text;
	public Skill skill;

	void Start () {
		btn = GetComponent<Button> ();
//		Combatant[] combatants = FindObjectsOfType<Combatant> ();
//		foreach(Combatant c in combatants){
//			if(c.playable){
//				cs = c.Attack;
//				break;
//			}
//		}
		OnClicked();

		text = GetComponentInChildren<Text>();
		text.text = skill.getSkillName();
		btn.onClick.AddListener (ChooseSkill);
	}

	void OnClicked(){
		btn.interactable = false;
	}

	void Enable (){
		btn.interactable = true;
	}

	void ChooseSkill(){
		EventManager.selectedSkill = skill;
		EventManager.ChooseSkill();
	}

	void OnEnable(){
		EventManager.OnClickedSkillBtn += OnClicked;
		EventManager.OnClickedTargetBtn += Enable;
	}

	void OnDisable(){
		EventManager.OnClickedSkillBtn -= OnClicked;
		EventManager.OnClickedTargetBtn -= Enable;
	}
}
