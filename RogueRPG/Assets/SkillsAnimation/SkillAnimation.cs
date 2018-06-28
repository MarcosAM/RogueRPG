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

	public void PlayAnimation (Skill skill, Battleground.Tile tile){
		rectTransform.localPosition = tile.getLocalPosition ();
		mySkill = skill;
		animator.SetTrigger ("play");
	}

	public void End (){
		mySkill.EndSkill ();
		Destroy (gameObject);
	}
}
