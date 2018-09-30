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
		regenerationManager = new Character.RegenerationAndPoisonManager (this);

		if(stats != null){
			FillStats ();
		}

        inventory = GetComponent<Inventory>();
        inventory.SetCharacter(this);
        movement = GetComponent<IMovable> ();
		movement.Initialize (this);
		playable = false;
	}

	protected override void FillStats (){
		base.FillStats ();
		equips = stats.getSkills ();
		currentStamina = 0;
		maxStamina = stats.getStamina ();
		characterName = stats.getName();
		portrait = stats.getPortrait();
		if(inventory != null){
			Destroy (inventory.gameObject);
		}
		inventory.SetCharacter (this);

		int hp = 0;
		int atk = 0;
		int atkm = 0;
		int def = 0;
		int defm = 0;
		foreach (Equip skill in equips) {
			hp += skill.GetHp ();
			atk += skill.GetAtk ();
			atkm += skill.GetAtkm ();
			def += skill.GetDef ();
			defm += skill.GetDefm ();
		}

		this.atk.setStatBase (atk);
		this.atkm.setStatBase (atkm);
		this.def.setStatBase (def);
		this.defm.setStatBase (defm);
		this.maxHp = hp;
		this.hp = maxHp;
	}
}
