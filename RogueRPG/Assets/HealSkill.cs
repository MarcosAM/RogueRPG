using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill {

	void Awake(){
		isSingleTarget = true;
		targets = Targets.Allies;
	}

	public override void UniqueEffect (Combatant user, Combatant target)
	{
		base.UniqueEffect (user, target);
		target.Heal (value+user.getAtkm());
	}

}
