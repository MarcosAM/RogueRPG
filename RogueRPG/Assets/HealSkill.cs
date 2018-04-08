using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill {

	void Awake(){
		isSingleTarget = true;
		targets = Targets.Allies;
	}

	public override void Effect (Combatant user, Combatant target)
	{
		base.Effect (user, target);
		user.SpendEnergy (energyCost);
		target.Heal (value+user.getAtkm());
		EventManager.SkillUsed ();
	}
}
