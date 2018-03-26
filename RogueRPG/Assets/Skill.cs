using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {

	public string skillName;
	public float value;

	public virtual void Effect (Combatant user, Combatant target)
	{
	}

	public string getSkillName ()
	{
		return skillName;
	}
}
