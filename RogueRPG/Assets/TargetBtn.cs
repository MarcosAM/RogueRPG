using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : MonoBehaviour {

	[SerializeField]Button btn;
	public Combatant combatant;

	void Start () {
		btn = GetComponent<Button> ();
		btn.interactable = false;
		btn.onClick.AddListener (ChooseTarget);
	}

	void Disable(){
		btn.interactable = false;
	}

	void Enable (){
		btn.interactable = true;
	}

	void ChooseTarget(){
		EventManager.target = combatant;
		EventManager.ChooseTarget ();
	}

	void OnEnable(){
		EventManager.OnClickedTargetBtn += Disable;
		EventManager.OnClickedSkillBtn += Enable;
		EventManager.OnSetSkills += Enable;
		CombatManager.OnTurnEnded += Disable;
	}

	void OnDisable(){
		EventManager.OnClickedTargetBtn -= Disable;
		EventManager.OnClickedSkillBtn -= Enable;
		EventManager.OnSetSkills -= Enable;
		CombatManager.OnTurnEnded -= Disable;
	}
}
