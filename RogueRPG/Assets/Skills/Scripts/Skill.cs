﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

	public enum Targets{Self, Allies, Enemies, All, Location};
	public enum Types{Melee, Ranged};
	
	[SerializeField]protected string skillName;
	[SerializeField]protected int value;
	[SerializeField]protected float energyCost;
	[SerializeField]protected float precision;
	[SerializeField]protected float criticRate;
	[SerializeField]protected Types type;
	[Range(0,3)]
	[SerializeField]protected int range;
	[SerializeField]protected Buff.BuffType buffType;
	[SerializeField]protected int buffDuration;
	[SerializeField]protected Buff buffPrefab;
	[SerializeField]protected SkillAnimation animationPrefab;
	[SerializeField]protected bool isSingleTarget;
	[SerializeField]protected Targets targets;
	protected int howManyTargets;
	protected int targetsHited;
	protected bool endable;

	public void Effect (Character user, Character target){
		endable = true;
		FindObjectOfType<Narration>().Appear(user.getName(), skillName);
		EffectAnimation (target);
		user.SpendEnergy(energyCost);
		UniqueEffect(user,target);
	}

//	public void Effect (Character user, int targetIndex){
//		endable = true;
//		FindObjectOfType<Narration> ().Appear (user.getName(), skillName);
//		user.SpendEnergy (energyCost);
//		Character target;
//		switch(targets){
//		case Targets.Allies:
//			if (user.getIsHero) {
//				target = FindObjectOfType<Battleground> ().getHeroSide () [targetIndex];
//			} else {
//				target = FindObjectOfType<Battleground> ().getEnemySide () [targetIndex];
//			}
//			break;
//		case Targets.Enemies:
//			if (user.getIsHero) {
//				target = FindObjectOfType<Battleground> ().getEnemySide () [targetIndex];
//			} else {
//				target = FindObjectOfType<Battleground> ().getHeroSide () [targetIndex];
//			}
//			break;
//		case 
//		}
//
//		UniqueEffect ();
//	}

	public void Effect (Character user){
		endable = true;
		FindObjectOfType<Narration>().Appear(user.getName(), skillName);
		user.SpendEnergy(energyCost);
		List<Character> myTargets = new List<Character>();
		Character[] temporaryTargets;
		if(user.getIsHero()){
			switch(targets){
			case Targets.Allies:
				temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
				break;
			case Targets.Enemies:
				temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
				break;
			case Targets.All:
				temporaryTargets = FindObjectsOfType<Character> ();
				break;
			default:
				temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
				break;
			}
		}else{
			switch(targets){
			case Targets.Allies:
				temporaryTargets = FindObjectsOfType<NonPlayableCharacter> ();
				break;
			case Targets.Enemies:
				temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
				break;
			case Targets.All:
				temporaryTargets = FindObjectsOfType<Character> ();
				break;
			default:
				temporaryTargets = FindObjectsOfType<PlayableCharacter> ();
				break;
			}
		}
		for (int i = 0; i<temporaryTargets.Length; i++){
			if(temporaryTargets[i].isAlive()){
				myTargets.Add (temporaryTargets[i]);
			}
		}
		howManyTargets = myTargets.Count;
		targetsHited = 0;
		foreach (Character target in myTargets) {
			EffectAnimation(target);
			UniqueEffect (user, target);
		}
	}

	public void EffectAnimation(Character target){
		SkillAnimation skillAnimation = Instantiate (animationPrefab);
		skillAnimation.transform.SetParent (FindObjectOfType<Canvas>().transform,false);
		skillAnimation.PlayAnimation (this,target);
	}

	public void EndSkill(){
		if (isSingleTarget) {
			FindObjectOfType<Narration>().Disappear();
			EventManager.SkillUsed ();
		} else {
			targetsHited++;
			if(targetsHited>=howManyTargets){
				FindObjectOfType<Narration>().Disappear();
				EventManager.SkillUsed ();
			}
		}
	}

	public virtual void UniqueEffect (Character user, Character target){
	}

	public string getSkillName (){
		return skillName;
	}

	public float getPrecision (){
		return precision;
	}

	public float getCriticRate (){
		return criticRate;
	}

	public bool getIsSingleTarget (){
		return isSingleTarget;
	}

	public Targets getTargets (){
		return targets;
	}

	public Buff.BuffType getBuffType(){
		return buffType;
	}

	public Types getSkillType(){
		return type;
	}

	public int getRange(){
		return range;
	}
}
