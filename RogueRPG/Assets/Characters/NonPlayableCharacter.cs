using System.Collections;
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
//		combatBehavior = GetComponent<CombatBehavior> ();
		movement = GetComponent<IMovable> ();
		movement.Initialize (this);
//		combatBehavior.setCharacter (this);
		playable = false;
	}

	protected override void FillStats (){
		base.FillStats ();
//		atk.setStatBase (stats.getAtk ());
//		atkm.setStatBase (stats.getAtkm ());
//		def.setStatBase (stats.getDef ());
//		defm.setStatBase (stats.getDefm ());
		skills = stats.getSkills ();
//		maxHp = stats.getHp ();
//		hp = maxHp;
		currentStamina = 0;
		maxStamina = stats.getStamina ();
		characterName = stats.getName();
		portrait = stats.getPortrait();
//		foreach(Skill skill in skills){
//			skill.setUser(this);
//		}
		if(combatBehavior != null){
			Destroy (combatBehavior.gameObject);
		}
		combatBehavior = stats.getCombatBehavior();
		combatBehavior.setCharacter (this);

		int hp = 0;
		int atk = 0;
		int atkm = 0;
		int def = 0;
		int defm = 0;
		foreach (Skill skill in skills) {
			hp += skill.getHp ();
			atk += skill.getAtk ();
			atkm += skill.getAtkm ();
			def += skill.getDef ();
			defm += skill.getDefm ();
		}

		this.atk.setStatBase (atk);
		this.atkm.setStatBase (atkm);
		this.def.setStatBase (def);
		this.defm.setStatBase (defm);
		this.maxHp = hp;
		this.hp = maxHp;
	}
}
