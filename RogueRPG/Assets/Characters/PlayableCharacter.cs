using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character {

	void Awake (){
		isHero = true;
	}

	public override void ChooseSkill ()
	{
		EventManager.OnPlayerChoosedSkill += ReadySkill;
		EventManager.ShowSkillsOf (this);
	}

	public override void ReadySkill (Skill s)
	{
		choosedSkill = s;
		EventManager.OnPlayerChoosedSkill -= ReadySkill;
		if (choosedSkill.getIsSingleTarget ()) {
			ChooseTarget ();
		} else {
			UseSkill();
		}
	}

	public override void ChooseTarget ()
	{
		EventManager.OnPlayerChoosedTarget += ReadyTarget;
		EventManager.ShowTargetsOf (this,choosedSkill.getTargets());
	}

	public override void ReadyTarget (Character c)
	{
		UseSkill (this,c);
		EventManager.OnPlayerChoosedTarget -= ReadyTarget;
	}



	public override void UseSkill (Character u, Character t)
	{
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (u,t);
	}

	public override void UseSkill ()
	{
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (this);
	}

}
