using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKSkill : Skill {

	public override void Effect (Combatant user, Combatant target)
	{
		float damage = Random.Range(0f,3f);
		energyCost = 1+damage;
		user.SpendEnergy (energyCost);
		user.Attack(target,damage);
		EventManager.SkillUsed ();
	}

	ATKSkill (string n, float v)
	{
		skillName = n;
		value = v;
	}
}
