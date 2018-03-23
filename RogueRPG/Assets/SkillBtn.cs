using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour {

	Button btn;
	EventManager.ChoosedSkill cs;

	void Start () {
		btn = GetComponent<Button> ();
		Combatant[] combatants = FindObjectsOfType<Combatant> ();
		foreach(Combatant c in combatants){
			if(c.playable){
				cs = c.Attack;
				break;
			}
		}
		btn.onClick.AddListener (ChooseSkill);
	}

	void OnClicked(){
		btn.interactable = false;
	}

	void Enable (){
		btn.interactable = true;
	}

	void ChooseSkill(){
		EventManager.ChooseSkill (cs);
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
