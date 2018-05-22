using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Dodge Buffs")]
public class DodgeBuffSkill : Skill {

	public override void UniqueEffect (Character user, Character target)
	{
		base.UniqueEffect (user, target);
		Buff buff = Instantiate(buffPrefab);
		buff.Initialize((int)value,3,Buff.BuffType.Dodge,target);
	}
}
