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
	}

	public void Initialize(Character character){
		this.character = character;
	}

	public void ChooseSkill ()
	{
		ReadySkill (character.skills[Random.Range(0,character.skills.Length)]);
	}

	public void ReadySkill (Skill s)
	{
		choosedSkill = s;
		if (choosedSkill.getIsSingleTarget ()) {
			ChooseTarget ();
		} else {
			UseSkill();
		}
	}

	public void ChooseTarget ()
	{
		PlayableCharacter[] heroes = FindObjectsOfType<PlayableCharacter>();
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

	public void ReadyTarget (Character c)
	{
		UseSkill(character,c);
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
