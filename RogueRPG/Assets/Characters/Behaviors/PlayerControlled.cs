using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : MonoBehaviour, ICombatBehavior {

	private Character character;
	private Skill choosedSkill;

	public void Act(){
		//TODO corrigir essa gambiarra!!
		if(character.OnMyTurnStarts != null){
			character.OnMyTurnStarts ();
		}
		character.SpendBuffs();
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
			ChooseTarget ();
		} else {
			UseSkill();
		}
	}

	public void ChooseTarget ()
	{
		EventManager.OnPlayerChoosedTarget += ReadyTarget;
		EventManager.ShowTargetsOf (character,choosedSkill.getTargets());
	}

	public void ReadyTarget (Character c)
	{
		UseSkill (character,c);
		EventManager.OnPlayerChoosedTarget -= ReadyTarget;
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
}
