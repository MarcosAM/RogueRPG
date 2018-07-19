using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnAction {

	int priority;
	float probability;

	public virtual void defineEquipSkillTargetFor (CombatBehavior combatBehavior){}
}
