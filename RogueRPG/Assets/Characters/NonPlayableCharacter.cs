﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : Character {

	void Awake (){
		atk = new Stat();
		atkm = new Stat();
		def = new Stat();
		defm = new Stat();
		critic = new Stat();
		precision = new Stat();
		dodge = new Stat();
		if(stats != null){
			FillStats ();
		}
		combatBehavior = GetComponent<CombatBehavior> ();
		movement = GetComponent<IMovable> ();
		movement.Initialize (this);
		combatBehavior.setCharacter (this);
		playable = false;
	}

	protected override void FillStats (){
		base.FillStats ();
		atk.setStatBase (stats.getAtk ());
		atkm.setStatBase (stats.getAtkm ());
		def.setStatBase (stats.getDef ());
		defm.setStatBase (stats.getDefm ());
		skills = stats.getSkills ();
		maxHp = stats.getHp ();
		hp = maxHp;
		characterName = stats.getName();
		portrait = stats.getPortrait();
		foreach(Skill skill in skills){
			skill.setUser(this);
		}
	}
}
