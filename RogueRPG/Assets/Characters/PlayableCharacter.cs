using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character {

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
//		combatBehavior = GetComponent<CombatBehavior> ();
		movement = GetComponent<IMovable> ();
		movement.Initialize (this);
//		combatBehavior.setCharacter (this);
		playable = true;
//		DontDestroyOnLoad (transform.parent.gameObject);
//		DontDestroyOnLoad (gameObject);
	}

	protected override void FillStats ()
	{
		base.FillStats ();
		atk.setStatBase (stats.getAtk ());
		atkm.setStatBase (stats.getAtkm ());
		def.setStatBase (stats.getDef ());
		defm.setStatBase (stats.getDefm ());
		skills = stats.getSkills ();
		momentumSkill = stats.getMomentumSkill ();
		maxHp = stats.getHp ();
		hp = maxHp;
		currentStamina = 0;
		maxStamina = stats.getStamina ();
//		foreach(Skill skill in skills){
//			skill.setUser(this);
//		}
		if(combatBehavior != null){
			Destroy (combatBehavior.gameObject);
		}
		combatBehavior = stats.getCombatBehavior();
		combatBehavior.setCharacter (this);
	}
}
