﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : MonoBehaviour, IPlaySkillAnimation {

	private Animator animator;
	private RectTransform rectTransform;
	private Skill mySkill;
	private SkillEffect mySkillEffect;

	void Awake (){
		animator = GetComponent<Animator> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	public void PlayAnimation (Skill skill, Battleground.Tile tile){
		rectTransform.localPosition = tile.getLocalPosition () + new Vector2(0,20);
		mySkill = skill;
		animator.SetTrigger ("play");
	}

	public void PlayAnimation (SkillEffect skillEffect, Battleground.Tile tile){
		rectTransform.localPosition = tile.getLocalPosition () + new Vector2(0,50);
		mySkillEffect = skillEffect;
		animator.SetTrigger ("play");
	}

	public void End (){
		mySkillEffect.EndSkill ();
		Destroy (gameObject);
	}
}
