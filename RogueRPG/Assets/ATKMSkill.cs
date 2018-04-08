using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKMSkill : Skill {

	void Awake(){
		isSingleTarget = true;
		targets = Targets.Enemies;
	}

	public override void Effect (Combatant user, Combatant target)
	{
		base.Effect (user, target);
		user.SpendEnergy (energyCost);
		user.AttackMagic (target,value,this);
		EventManager.SkillUsed ();
	}

}
