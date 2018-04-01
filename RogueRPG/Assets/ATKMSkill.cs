using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKMSkill : Skill {

	public override void Effect (Combatant user, Combatant target)
	{
		user.SpendEnergy (energyCost);
		user.AttackMagic (target,value,this);
		EventManager.SkillUsed ();
	}

}
