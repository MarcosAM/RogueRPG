using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehavior : CombatBehavior {

	public override void StartTurn ()
	{
		base.StartTurn ();
		character.StartTurn();
		ChooseSkill ();
//		if (choosedSkill.getIsSingleTarget ()) {
		ChooseTarget ();
//		} else {
//			UseSkill();
//		}
	}

	public void ChooseSkill ()
	{
		choosedSkill = character.skills[Random.Range(0,character.skills.Length)];
	}

	public void ChooseTarget ()
	{
		if(choosedSkill.getTargets() == Skill.Targets.Enemies){
			Battleground.Tile[] tempHeroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
			int heroesAlive = 0;
			for(int i =0;i<tempHeroesTiles.Length;i++){
				if(tempHeroesTiles[i].getOccupant()!=null)
					if (tempHeroesTiles [i].getOccupant ().isAlive ())
						heroesAlive++;
			}
			if (heroesAlive > 0) {
				Battleground.Tile[] heroesTile = new Battleground.Tile[heroesAlive];
				for (int i = 0; i < tempHeroesTiles.Length; i++) {
					if(tempHeroesTiles[i].getOccupant()!=null)
						if (tempHeroesTiles [i].getOccupant ().isAlive ())
							heroesTile [i] = tempHeroesTiles [i];
				}
				int r = Random.Range(0,heroesTile.Length);
				UseSkill (heroesTile[r]);
			} else {
				//TODO terminar a batalha ou encontro ou dungeon wtv
				print("Termina essa batalha");
			}
		}
		if(choosedSkill.getTargets() == Skill.Targets.Self){
			UseSkill (DungeonManager.getInstance().getBattleground().getEnemiesTiles()[character.getPosition()]);
		}
	}

	public void UseSkill (Battleground.Tile tile){
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.Effect (character,tile);
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
