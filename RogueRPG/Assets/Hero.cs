using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Combatant {

	public override void StartTurn(){
		base.StartTurn();
		ChooseSkill ();
	}

	public override void ChooseSkill ()
	{
		base.ChooseSkill ();
		EventManager.OnPlayerChoosedSkill += ReadySkill;
		EventManager.ShowSkillsOf (this);
	}

	public override void ReadySkill (Skill s)
	{
		base.ReadySkill (s);
		choosedSkill = s;
		EventManager.OnPlayerChoosedSkill -= ReadySkill;
		ChooseTarget ();
	}

	public override void ChooseTarget ()
	{
		base.ChooseTarget ();
		EventManager.OnPlayerChoosedTarget += ReadyTarget;
		EventManager.ShowTargetsOf ();
	}

	public override void ReadyTarget (Combatant c)
	{
		base.ReadyTarget (c);
		UseSkill (this,c);
		EventManager.OnPlayerChoosedTarget -= ReadyTarget;
	}



	public override void UseSkill (Combatant u, Combatant t)
	{
		base.UseSkill (u, t);
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (u,t);
	}

}
