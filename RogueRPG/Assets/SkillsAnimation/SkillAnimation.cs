using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : MonoBehaviour, IPlaySkillAnimation {

	private Animator animator;
	private RectTransform rectTransform;
	private Equip mySkill;
	private SkillEffect mySkillEffect;
	private IWaitForAnimation requester;

	void Awake (){
		animator = GetComponent<Animator> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	public void PlayAnimation (Equip skill, Battleground.Tile tile){
		rectTransform.localPosition = tile.getLocalPosition () + new Vector2(0,20);
		mySkill = skill;
		animator.SetTrigger ("play");
	}

	public void PlayAnimation (SkillEffect skillEffect, Battleground.Tile tile){
		rectTransform.localPosition = tile.getLocalPosition () + new Vector2(0,50);
		mySkillEffect = skillEffect;
		animator.SetTrigger ("play");
	}

	public void PlayAnimation (IWaitForAnimation requester, Vector2 animationPosition){
		rectTransform.localPosition = animationPosition + new Vector2(0,50);
		this.requester = requester;
		animator.SetTrigger ("play");
	}

	public void End (){
//		mySkillEffect.EndSkill ();
		requester.resumeFromAnimation();
		Destroy (gameObject);
	}
}
