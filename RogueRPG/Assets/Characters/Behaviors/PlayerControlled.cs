using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : CombatBehavior {

	public override void StartTurn ()
	{
		base.StartTurn ();
		character.StartTurn();
		ChooseSkill ();
	}

	public void ChooseSkill ()
	{
		EventManager.OnPlayerChoosedSkill += ReadySkill;
		EventManager.ShowSkillsOf (character);
	}

	public void ReadySkill (Skill skill)
	{
		choosedSkill = skill;
		EventManager.OnPlayerChoosedSkill -= ReadySkill;
		if (choosedSkill.getIsSingleTarget ()) {
			EventManager.OnUnchoosedSkill += UnchooseSkill;
			ChooseTarget ();
		} else {
			UseSkill();
		}
	}

	public void UnchooseSkill(){
		EventManager.OnPlayerChoosedTarget -= ReadyTarget;
		ChooseSkill ();
	}

	public void ChooseTarget (){
		if (choosedSkill.getTargets () != Skill.Targets.Location)
			EventManager.OnPlayerChoosedTarget += ReadyTarget;
		else
			EventManager.OnPlayerChoosedLocation += MoveTo;
		EventManager.ShowTargetsOf (character,choosedSkill);
	}

	public void MoveTo(int position){
		EventManager.OnPlayerChoosedLocation -= MoveTo;
		character.DelayBy (1);
		character.getMovement ().MoveTo (position);
		EndTurn ();
	}

	public void ReadyTarget (Character c)
	{
		UseSkill (character,c);
		EventManager.OnPlayerChoosedTarget -= ReadyTarget;
		EventManager.OnUnchoosedSkill -= UnchooseSkill;
	}
		
	public void UseSkill (Character u, Character t)
	{
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (u,t);
	}

	public void UseSkill ()
	{
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (character);
	}

	void EndTurn(){
		EventManager.OnSkillUsed -= EndTurn;
		character.EndTurn ();
	}
}
