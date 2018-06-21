using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehavior : MonoBehaviour, ICombatBehavior {

	private Character character;
	private Skill choosedSkill;

	public void Act(){
		//TODO corrigir essa gambiarra!!
		character.StartTurn();
		ChooseSkill ();
		if (choosedSkill.getIsSingleTarget ()) {
			ChooseTarget ();
		} else {
			UseSkill();
		}
	}

	public void Initialize(Character character){
		this.character = character;
	}

	public void ChooseSkill ()
	{
		choosedSkill = character.skills[Random.Range(0,character.skills.Length)];
	}

	public void ChooseTarget ()
	{
		if(choosedSkill.getTargets() == Skill.Targets.Enemies){
			PlayableCharacter[] heroes = FindObjectsOfType<PlayableCharacter>();
			bool found = false;
			//TODO versão que se não tiver herois não trave
			while(found == false){
				int r = Random.Range (0, heroes.Length);
				if(heroes[r].isAlive()){
					UseSkill(character,heroes[r]);
					found = true;
				}
			}
		}
		if(choosedSkill.getTargets() == Skill.Targets.Self){
			UseSkill (character,character);
		}
	}

	public void UseSkill (Character u, Character t)
	{
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect(character,t);
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
