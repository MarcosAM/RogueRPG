using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehavior : CombatBehavior {

	public override void StartTurn ()
	{
		base.StartTurn ();
		character.StartTurn();
		ChooseSkill ();
		ChooseTarget ();
	}

	public void ChooseSkill ()
	{
		choosedSkill = character.getUsableSkills()[Random.Range(0,character.getUsableSkills().Count)];
	}

	public void ChooseTarget ()
	{
		Battleground.Tile[] tempHeroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
		Battleground.Tile[] tempEnemiesTiles = DungeonManager.getInstance ().getBattleground ().getEnemiesTiles ();

		targetTile = tempEnemiesTiles[character.getPosition()];
		if (tempHeroesTiles [character.getPosition ()].getOccupant () == null) {
			for (int i = 0; i < tempHeroesTiles.Length; i++) {
				if (targetTile != null) {
					if (tempHeroesTiles [i].getOccupant () != null) {
						if (tempHeroesTiles [i].getOccupant ().getHp () > targetTile.getOccupant ().getHp ()) {
							targetTile = tempHeroesTiles [i];
						}
					}
				} else {
					if (tempHeroesTiles [i].getOccupant () != null) {
						targetTile = tempHeroesTiles [i];
					}
				}
			}
		} else {
			int mostDistant = 0;
			int currentDistance = 0;
			int leftIndex = 0;
			int rightIndex = 0;
			int minIndex = character.getPosition () - choosedSkill.getAlliesEffect ().getRange ();
			if(minIndex < 0)
				minIndex = 0;
			int maxIndex = character.getPosition () + choosedSkill.getAlliesEffect ().getRange ();
			if (maxIndex >= tempEnemiesTiles.Length)
				maxIndex = tempEnemiesTiles.Length - 1;
			for (int i = 0; i < tempEnemiesTiles.Length; i++) {
				currentDistance = 0;
				for (int j = 0; j < tempHeroesTiles.Length; j++) {
					leftIndex = i - j;
					if (leftIndex < minIndex)
						leftIndex = 0;
					rightIndex = i + j;
					if (rightIndex > maxIndex)
						rightIndex = maxIndex;
					if (tempHeroesTiles [leftIndex].getOccupant () != null || tempHeroesTiles [rightIndex].getOccupant () != null) {
						break;
					} else {
						currentDistance++;
					}
				}
				if(currentDistance > mostDistant || (currentDistance == mostDistant && (targetTile.getOccupant() != null && tempEnemiesTiles[i].getOccupant() == null))){
					targetTile = tempEnemiesTiles[i];
					mostDistant = currentDistance;
				}
			}
		}
		character.getHUD ().UseSkillAnimation ();
	}

	public override void UseSkill ()
	{
		base.UseSkill ();
		EventManager.OnSkillUsed += EndTurn;
		choosedSkill.UseEquipmentOn (character,targetTile);
	}

	void EndTurn(){
		EventManager.OnSkillUsed -= EndTurn;
		character.EndTurn ();
	}
}
