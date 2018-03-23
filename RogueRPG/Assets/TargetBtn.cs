using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBtn : MonoBehaviour {

	Button btn;
	public Combatant combatant;

	void Start () {
		btn = GetComponent<Button> ();
		btn.interactable = false;
		btn.onClick.AddListener (EventManager.ChooseTarget);
	}

	void OnClicked(){
		btn.interactable = false;
	}

	void Enable (){
		btn.interactable = true;
	}

	void OnEnable(){
		EventManager.OnClickedTargetBtn += OnClicked;
		EventManager.OnClickedSkillBtn += Enable;
	}

	void OnDisable(){
		EventManager.OnClickedTargetBtn -= OnClicked;
		EventManager.OnClickedSkillBtn += Enable;
	}
}
