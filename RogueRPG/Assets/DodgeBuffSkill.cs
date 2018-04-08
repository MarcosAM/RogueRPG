using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBuffSkill : Skill {

	void Awake(){
		isSingleTarget = true;
		targets = Targets.Self;
	}

	public override void UniqueEffect (Combatant user, Combatant target)
	{
		base.UniqueEffect (user, target);
		Buff buff = Instantiate(buffPrefab);
		buff.Initialize((int)value,3,Buff.BuffType.Dodge,target);
	}
}
