using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKSkill : Skill {

	void Awake(){
		isSingleTarget = true;
		targets = Targets.Enemies;
	}

	public override void UniqueEffect (Combatant user, Combatant target)
	{
		base.UniqueEffect (user, target);
		user.Attack(target,value,this);
	}

}
