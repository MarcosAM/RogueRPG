using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehavior : MonoBehaviour {

	[SerializeField]protected Character character;
	protected Skill choosedSkill;

	public virtual void StartTurn(){}

	public void setCharacter(Character character) {this.character = character;}
}
