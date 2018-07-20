using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnAction {

	protected int priority;
	protected float probability;
	protected Character character;
	protected CombatBehavior combatBehavior;

	public virtual void defineEquipSkillTargetFor (){}
	public int getPriotity() {return priority;}
	public float getProbability() {return probability;}
}
