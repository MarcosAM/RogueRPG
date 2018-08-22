using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageE : Effect {

	public override void effect (Character target)
	{
		base.effect (target);
		target.loseHpBy(value);
	}
}
