using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Attacks")]
public class ATKSkill : Skill {

	void Awake(){
		isSingleTarget = true;
		targets = Targets.Enemies;
	}

	public override void UniqueEffect (Character user, Character target)
	{
		base.UniqueEffect (user, target);
		user.Attack(target,value,this);
	}

}
