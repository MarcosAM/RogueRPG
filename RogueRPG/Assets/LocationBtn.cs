using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationBtn : MonoBehaviour {

	Button button;
	[SerializeField]int position;
	RectTransform rectTransform;

	void Awake(){
		rectTransform = GetComponent<RectTransform> ();
		button = GetComponent<Button> ();
		button.onClick.AddListener (ChooseNewLocation);
		Disappear ();
	}

	public void Initialize (int position){
		this.position = position;
	}

	void ChooseNewLocation(){
		EventManager.ChooseLocation (position);
	}

	void Disappear(){
		button.interactable = false;
	}

	void Appear(Character user,Skill skill){
		if(skill.getTargets() == Skill.Targets.Location){
			button.interactable = true;
		}
	}

	public RectTransform getRectTransform(){
		return rectTransform;
	}

	void OnEnable(){
		EventManager.OnShowTargetsOf += Appear;
		EventManager.OnClickedTargetBtn += Disappear;
		EventManager.OnUnchoosedSkill += Disappear;
	}

	void OnDisable(){
		EventManager.OnShowTargetsOf -= Appear;
		EventManager.OnClickedTargetBtn -= Disappear;
		EventManager.OnUnchoosedSkill -= Disappear;
	}
}
