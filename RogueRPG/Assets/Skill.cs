using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {

	public enum Targets{Self, Allies, Enemies};
	
	[SerializeField]protected string skillName;
	[SerializeField]protected float value;
	[SerializeField]protected float energyCost;
	[SerializeField]protected float precision;
	[SerializeField]protected float criticRate;
	[SerializeField]protected Buff buffPrefab;
	protected bool isSingleTarget;
	protected Targets targets;

	public virtual void Effect (Combatant user, Combatant target)
	{
	}

	public virtual void Effect (Combatant user)
	{
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
