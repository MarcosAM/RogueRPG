using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : CombatBehavior {

	public override void StartTurn ()
	{
		base.StartTurn ();
		character.StartTurn();
		ChooseSkill ();
		ChooseTarget ();
	}

	public void ChooseSkill ()
	{
		choosedSkill = character.skills[Random.Range(0,character.skills.Length)];
	}

	public void ChooseTarget ()
	{

	}

	public override void UseSkill ()
	{
		base.UseSkill ();
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (character,targetTile);
	}

	void EndTurn(){
		EventManager.OnSkillUsed -= EndTurn;
		character.EndTurn ();
	}
}
