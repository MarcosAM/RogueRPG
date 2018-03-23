using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour {

	Button btn;

	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (EventManager.ChooseSkill);
	}

	void OnClicked(){
		btn.interactable = false;
	}

	void Enable (){
		btn.interactable = true;
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
