﻿using System.Collections;
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
		choosedSkill = character.getUsableSkills()[Random.Range(0,character.getUsableSkills().Count)];
	}

	public void ChooseTarget ()
	{
		if (Random.value < 0.8f) {
			Battleground.Tile[] tempHeroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
			int heroesAlive = 0;
			for (int i = 0; i < tempHeroesTiles.Length; i++) {
				if (tempHeroesTiles [i].getOccupant () != null)
				if (tempHeroesTiles [i].getOccupant ().isAlive ())
					heroesAlive++;
			}
			if (heroesAlive > 0) {
				Battleground.Tile[] heroesTile = new Battleground.Tile[heroesAlive];
				int c = 0;
				for (int i = 0; i < tempHeroesTiles.Length; i++) {
					if (tempHeroesTiles [i].getOccupant () != null)
					if (tempHeroesTiles [i].getOccupant ().isAlive ()) {
						heroesTile [c] = tempHeroesTiles [i];
						c++;
					}
				}
				int r = Random.Range (0, heroesTile.Length);
				targetTile = heroesTile [r];
				character.getHUD ().UseSkillAnimation ();
				//				UseSkill (heroesTile[r]);
			} else {
				//TODO terminar a batalha ou encontro ou dungeon wtv
				print ("Termina essa batalha");
			}
		} else {
			targetTile = DungeonManager.getInstance ().getBattleground ().getEnemiesTiles () [character.getPosition ()];
			character.getHUD ().UseSkillAnimation ();
		}
	}

//	public void UseSkill (Battleground.Tile tile){
//		EventManager.OnSkillUsed += EndTurn;
//		choosedSkill.Effect (character,tile);
////		Debug.Log ("Na rodada " + DungeonManager.getInstance().getRound() + " " + character.getName()+" usou skill!");
//	}

	public override void UseSkill ()
	{
		base.UseSkill ();
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.UseEquipmentOn (character,targetTile);
	}

//	public void UseSkill (Character u, Character t)
//	{
//		EventManager.OnSkillUsed += EndTurn;
//		choosedSkill.Effect(character,t);
//	}
//
//	public void UseSkill ()
//	{
//		EventManager.OnSkillUsed += EndTurn;
//		choosedSkill.Effect (character);
//	}

	void EndTurn(){
		EventManager.OnSkillUsed -= EndTurn;
		character.EndTurn ();
	}
}
