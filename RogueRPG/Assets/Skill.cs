using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {

	[SerializeField]protected string skillName;
	[SerializeField]protected float value;
	[SerializeField]protected float energyCost;

	public virtual void Effect (Combatant user, Combatant target)
	{
	}

	public string getSkillName ()
	{
		return skillName;
	}
}
