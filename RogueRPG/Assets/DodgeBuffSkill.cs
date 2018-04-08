using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBuffSkill : Skill {

	void Awake(){
		isSingleTarget = true;
	}

	public override void Effect (Combatant user, Combatant target)
	{
		base.Effect (user, target);
		Buff buff = Instantiate(buffPrefab);
		buff.Initialize((int)value,3,Buff.BuffType.Dodge,target);
		EventManager.SkillUsed ();
	}
}
