﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant {

	public override void StartTurn ()
	{
		base.StartTurn ();
		ChooseSkill();
	}

	public override void ChooseSkill ()
	{
		base.ChooseSkill ();
		ReadySkill (skills[Random.Range(0,skills.Length)]);
	}

	public override void ReadySkill (Skill s)
	{
		base.ReadySkill (s);
		choosedSkill = s;
		ChooseTarget();
	}

	public override void ChooseTarget ()
	{
		base.ChooseTarget ();
		Hero[] heroes = FindObjectsOfType<Hero>();
		ReadyTarget(heroes[Random.Range(0,heroes.Length)]);
	}

	public override void ReadyTarget (Combatant c)
	{
		base.ReadyTarget (c);
		UseSkill(this,c);
	}

	public override void UseSkill (Combatant u, Combatant t)
	{
		base.UseSkill (u, t);
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect(this,t);
	}

}
