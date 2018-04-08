using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKAllSkill : Skill {

	void Awake(){
		isSingleTarget = false;
		targets = Targets.Enemies;
	}

	public override void UniqueEffect (Combatant user)
	{
		base.UniqueEffect (user);
		Combatant[] targets;
		if (user.getIsHero ()) {
			targets = FindObjectsOfType<Enemy> ();
		} else {
			targets = FindObjectsOfType<Hero> ();
		}
		for(int i = 0;i<targets.Length;i++){
			user.AttackMagic (targets[i],value,this);
		}
	}

}