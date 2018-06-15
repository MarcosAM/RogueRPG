using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Skills/Buffs")]
public class BuffSkill : Skill {

	[SerializeField]protected Buff.BuffType sBuffType;
	[SerializeField]protected int sBuffDuration;
	[SerializeField]protected Buff sBuffPrefab;

	public override void UniqueEffect (Character user, Character target)
	{
		base.UniqueEffect (user, target);
		Buff buff = Instantiate (sBuffPrefab);
		switch (sBuffType) {
		case Buff.BuffType.Dodge:
			buff.Initialize ((int)sValue, sBuffDuration, Buff.BuffType.Dodge, target);
			break;
		case Buff.BuffType.Precision:
			buff.Initialize ((int)sValue, sBuffDuration, Buff.BuffType.Precision, target);
			break;
		case Buff.BuffType.Critic:
			buff.Initialize ((int)sValue, sBuffDuration, Buff.BuffType.Critic, target);
			break;
		default:
			buff.Initialize ((int)sValue, sBuffDuration, Buff.BuffType.Dodge, target);
			break;
		}
	}
}
