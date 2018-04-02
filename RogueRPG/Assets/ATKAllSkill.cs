using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKAllSkill : Skill {

	void Awake(){
		isSingleTarget = false;
	}

	public override void Effect (Combatant user)
	{
		base.Effect (user);
		user.SpendEnergy (energyCost);
		Combatant[] targets;
		if (user.getIsHero ()) {
			targets = FindObjectsOfType<Enemy> ();
		} else {
			targets = FindObjectsOfType<Hero> ();
		}
		for(int i = 0;i<targets.Length;i++){
			user.AttackMagic (targets[i],value,this);
			print (user.getName() + " acertou " + (i+1) + " vezes!");
		}
		EventManager.SkillUsed ();
	}
}