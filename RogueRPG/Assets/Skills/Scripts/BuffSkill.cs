using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Buffs")]
public class BuffSkill : Skill {

	public override void UniqueEffect (Character user, Character target)
	{
		base.UniqueEffect (user, target);
		Buff buff = Instantiate (buffPrefab);
		switch (buffType) {
		case Buff.BuffType.Dodge:
			buff.Initialize ((int)value, buffDuration, Buff.BuffType.Dodge, target);
			break;
		case Buff.BuffType.Precision:
			buff.Initialize ((int)value, buffDuration, Buff.BuffType.Precision, target);
			break;
		case Buff.BuffType.Critic:
			buff.Initialize ((int)value, buffDuration, Buff.BuffType.Critic, target);
			break;
		default:
			buff.Initialize ((int)value, buffDuration, Buff.BuffType.Dodge, target);
			break;
		}
	}
}
