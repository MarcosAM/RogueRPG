using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : MonoBehaviour, IPlaySkillAnimation {

	private Animator animator;
	private RectTransform rectTransform;

	void Awake (){
		animator = GetComponent<Animator> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	public void PlayAnimation (Skill skill, Combatant target){
		rectTransform.localPosition = target.getHUD ().getRectTransform ().localPosition;
		animator.SetTrigger ("play");
	}

	public void End (){
		Destroy (gameObject);
	}
}
