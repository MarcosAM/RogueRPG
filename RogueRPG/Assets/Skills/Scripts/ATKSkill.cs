using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Attacks")]
public class ATKSkill : Skill {

	public override void UniqueEffect (Character user, Character target)
	{
		base.UniqueEffect (user, target);
		if (target.isPlayable () != user.isPlayable ()) {
			user.Attack(target,sValue,this);
		} else {
			user.getMovement().MoveTo(target.getPosition());
		}
	}
}
