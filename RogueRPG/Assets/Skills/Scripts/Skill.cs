using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

	public enum Targets{Self, Allies, Enemies, All, Location};
	public enum Types{Melee, Ranged};
	
	[SerializeField]protected string sName;
	[SerializeField]protected int sValue;
	[SerializeField]protected float sDelay;
	[SerializeField]protected float sPrecision;
	[SerializeField]protected float sCriticRate;
	[SerializeField]protected Types sType;
	[Range(0,3)]
	[SerializeField]protected int sRange;
	[SerializeField]protected SkillAnimation sAnimationPrefab;
	[SerializeField]protected bool sSingleTarget;
	[SerializeField]protected Targets sTargets;
	protected int howManyTargets;
	protected int targetsHited;
	protected bool endable;

	public void Effect (Character user, Character target){
		endable = true;
		FindObjectOfType<Narration>().Appear(user.getName(), sName);
		EffectAnimation (target);
		user.DelayBy(sDelay);
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
		FindObjectOfType<Narration>().Appear(user.getName(), sName);
		user.DelayBy(sDelay);
		List<Character> myTargets = new List<Character>();
		Character[] temporaryTargets;
		if(user.isPlayable()){
			switch(sTargets){
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
			switch(sTargets){
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
		SkillAnimation skillAnimation = Instantiate (sAnimationPrefab);
		skillAnimation.transform.SetParent (FindObjectOfType<Canvas>().transform,false);
		skillAnimation.PlayAnimation (this,target);
	}

	public void EndSkill(){
		if (sSingleTarget) {
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
		return sName;
	}

	public float getPrecision (){
		return sPrecision;
	}

	public float getCriticRate (){
		return sCriticRate;
	}

	public bool getIsSingleTarget (){
		return sSingleTarget;
	}

	public Targets getTargets (){
		return sTargets;
	}

	public Types getSkillType(){
		return sType;
	}

	public int getRange(){
		return sRange;
	}
}
