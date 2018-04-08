using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKMSkill : Skill {

	void Awake(){
		isSingleTarget = true;
		targets = Targets.Enemies;
	}

	public override void UniqueEffect (Combatant user, Combatant target)
	{
		base.UniqueEffect (user, target);
		user.AttackMagic (target,value,this);
	}
}
