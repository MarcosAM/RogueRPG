using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {

	public enum Targets{Self, Allies, Enemies, All};
	
	[SerializeField]protected string skillName;
	[SerializeField]protected int value;
	[SerializeField]protected float energyCost;
	[SerializeField]protected float precision;
	[SerializeField]protected float criticRate;
	[SerializeField]protected Buff buffPrefab;
	[SerializeField]protected SkillAnimation animationPrefab;
	protected bool isSingleTarget;
	protected Targets targets;
	protected int howManyTargets;
	protected int targetsHited;
	protected bool endable;

	public void Effect (Combatant user, Combatant target){
		endable = true;
		FindObjectOfType<Narration>().Appear(user.getName(), skillName);
		EffectAnimation (target);
		user.SpendEnergy(energyCost);
		UniqueEffect(user,target);
	}

	public void Effect (Combatant user){
		endable = true;
		FindObjectOfType<Narration>().Appear(user.getName(), skillName);
		user.SpendEnergy(energyCost);
		Combatant[] myTargets;
		if(user.getIsHero()){
			switch(targets){
			case Targets.Allies:
				myTargets = FindObjectsOfType<Hero> ();
				break;
			case Targets.Enemies:
				myTargets = FindObjectsOfType<Enemy> ();
				break;
			case Targets.All:
				myTargets = FindObjectsOfType<Combatant> ();
				break;
			default:
				myTargets = FindObjectsOfType<Enemy> ();
				print ("Bugou!");
				break;
			}
		}else{
			switch(targets){
			case Targets.Allies:
				myTargets = FindObjectsOfType<Enemy> ();
				break;
			case Targets.Enemies:
				myTargets = FindObjectsOfType<Hero> ();
				break;
			case Targets.All:
				myTargets = FindObjectsOfType<Combatant> ();
				break;
			default:
				myTargets = FindObjectsOfType<Hero> ();
				print ("Bugou!");
				break;
			}
		}
		howManyTargets = myTargets.Length;
		targetsHited = 0;
		foreach (Combatant target in myTargets) {
			EffectAnimation(target);
			UniqueEffect (user, target);
		}
	}

	public void EffectAnimation(Combatant target){
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

	public virtual void UniqueEffect (Combatant user, Combatant target){
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
}
