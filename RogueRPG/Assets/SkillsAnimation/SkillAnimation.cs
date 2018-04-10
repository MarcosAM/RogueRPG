using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : MonoBehaviour, IPlaySkillAnimation {

	private Animator animator;
	private RectTransform rectTransform;
	private Skill mySkill;

	void Awake (){
		animator = GetComponent<Animator> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	public void PlayAnimation (Skill skill, Combatant target){
		rectTransform.localPosition = target.getHUD ().getRectTransform ().localPosition;
		mySkill = skill;
		animator.SetTrigger ("play");
	}

	public void End (){
		mySkill.EndSkill ();
		Destroy (gameObject);
	}
}
