using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant {

	void Awake (){
		isHero = false;
	}

	public override void ChooseSkill ()
	{
		ReadySkill (skills[Random.Range(0,skills.Length)]);
	}

	public override void ReadySkill (Skill s)
	{
		choosedSkill = s;
		if (choosedSkill.getIsSingleTarget ()) {
			ChooseTarget ();
		} else {
			UseSkill();
		}
	}

	public override void ChooseTarget ()
	{
		Hero[] heroes = FindObjectsOfType<Hero>();
		bool found = false;
		//TODO versão que se não tiver herois não trave
		while(found == false){
			int r = Random.Range (0, heroes.Length);
			if(heroes[r].isAlive()){
				ReadyTarget(heroes[r]);
				found = true;
			}
		}
	}

	public override void ReadyTarget (Combatant c)
	{
		UseSkill(this,c);
	}

	public override void UseSkill (Combatant u, Combatant t)
	{
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect(this,t);
	}

	public override void UseSkill ()
	{
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (this);
	}
}
