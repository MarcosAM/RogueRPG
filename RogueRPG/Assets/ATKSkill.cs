using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKSkill : Skill {

	public override void Effect (Combatant user, Combatant target)
	{
		user.Attack(target,value);
		EventManager.SkillUsed ();
	}

	ATKSkill (string n, float v)
	{
		skillName = n;
		value = v;
	}
}
