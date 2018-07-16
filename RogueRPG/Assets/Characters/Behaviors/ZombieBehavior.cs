using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : CombatBehavior {

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

		int maxIndex = character.getPosition() + choosedSkill.getMeleeEffect().getRange();
		if (maxIndex >= tempHeroesTiles.Length)
			maxIndex = tempHeroesTiles.Length - 1;
		int minIndex = character.getPosition () - choosedSkill.getMeleeEffect().getRange ();
		if (minIndex < 0)
			minIndex = 0;

		float hpFromLeft;
		float hpFromRight;

//		for(int i = 0; i <= choosedSkill.getRange(); i++){
//			if (character.getPosition () + i <= maxIndex) {
//				if (tempHeroesTiles [character.getPosition () + i].getOccupant () != null)
//					hpFromRight = tempHeroesTiles [character.getPosition () + i].getOccupant ().getHp ();
//				else
//					hpFromRight = 0;
//			} else {
//				hpFromRight = 0;
//			}
//
//			if (character.getPosition () - i >= minIndex) {
//				if (tempHeroesTiles [character.getPosition () - i].getOccupant () != null)
//					hpFromLeft = tempHeroesTiles [character.getPosition () - i].getOccupant ().getHp ();
//				else
//					hpFromLeft = 0;
//			} else {
//				hpFromLeft = 0;
//			}
//
//			if(hpFromLeft > 0 || hpFromRight > 0){
//				if (hpFromLeft > hpFromRight) {
//					targetTile = tempHeroesTiles [character.getPosition () - 1];
//					character.getHUD ().UseSkillAnimation ();
//					return;
//				} else {
//					targetTile = tempHeroesTiles [character.getPosition () + 1];
//					character.getHUD ().UseSkillAnimation ();
//					return;
//				}
//			}
//		}

		for(int i = 0; i <= tempHeroesTiles.Length; i++){
			if (character.getPosition () + i < tempHeroesTiles.Length) {
				if (tempHeroesTiles [character.getPosition () + i].getOccupant () != null)
					hpFromRight = tempHeroesTiles [character.getPosition () + i].getOccupant ().getHp ();
				else
					hpFromRight = 0;
			} else {
				hpFromRight = 0;
			}

			if (character.getPosition () - i >= 0) {
				if (tempHeroesTiles [character.getPosition () - i].getOccupant () != null)
					hpFromLeft = tempHeroesTiles [character.getPosition () - i].getOccupant ().getHp ();
				else
					hpFromLeft = 0;
			} else {
				hpFromLeft = 0;
			}
			if(hpFromLeft > 0 || hpFromRight > 0){
				if (hpFromLeft > hpFromRight) {
					if (character.getPosition () - i >= minIndex) {
						targetTile = tempHeroesTiles [character.getPosition () - i];
						character.getHUD ().UseSkillAnimation ();
						return;
					} else {
						if (character.getPosition () - i >= 0) {
							//TODO fazer com que ele ande só no máximo a distância da skill de andar
							targetTile = tempEnemiesTiles [character.getPosition () - i];
							character.getHUD ().UseSkillAnimation ();
							return;
						}
					}
				} else {
					if (character.getPosition () + i <= maxIndex) {
						targetTile = tempHeroesTiles [character.getPosition () + i];
						character.getHUD ().UseSkillAnimation ();
						return;
					} else {
						if (character.getPosition () + i < tempEnemiesTiles.Length) {
							targetTile = tempEnemiesTiles [character.getPosition () + i];
							character.getHUD ().UseSkillAnimation ();
							return;
						}
					}
				}
			}
		}
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
