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
		List<Combatant> myTargets = new List<Combatant>();
		Combatant[] temporaryTargets;
		if(user.getIsHero()){
			switch(targets){
			case Targets.Allies:
				temporaryTargets = FindObjectsOfType<Hero> ();
				break;
			case Targets.Enemies:
				temporaryTargets = FindObjectsOfType<Enemy> ();
				break;
			case Targets.All:
				temporaryTargets = FindObjectsOfType<Combatant> ();
				break;
			default:
				temporaryTargets = FindObjectsOfType<Enemy> ();
				break;
			}
		}else{
			switch(targets){
			case Targets.Allies:
				temporaryTargets = FindObjectsOfType<Enemy> ();
				break;
			case Targets.Enemies:
				temporaryTargets = FindObjectsOfType<Hero> ();
				break;
			case Targets.All:
				temporaryTargets = FindObjectsOfType<Combatant> ();
				break;
			default:
				temporaryTargets = FindObjectsOfType<Hero> ();
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
