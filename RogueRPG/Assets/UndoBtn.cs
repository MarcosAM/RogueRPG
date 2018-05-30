using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoBtn : MonoBehaviour {

	Image icon;

	void Awake(){
		icon = GetComponent<Image> ();
		Disappear ();
	}

	public void UnchooseSkill(){
		EventManager.UnchooseSkill ();
		Disappear ();
	}

	void Appear(Character character, Skill skill){
		icon.enabled = true;
	}

	void Disappear(){
		icon.enabled = false;
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
