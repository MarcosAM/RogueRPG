using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAAtkHealthier : TurnAction {

	public override void tryToDefineEquipSkillTargetFor ()
	{
		base.tryToDefineEquipSkillTargetFor ();

		Battleground.Tile[] tempHeroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
		Battleground.Tile[] tempEnemiesTiles = DungeonManager.getInstance ().getBattleground ().getEnemiesTiles ();

		combatBehavior.setChoosedSkill (character.getUsableSkills()[Random.Range(0,character.getUsableSkills().Count -1)]);
		combatBehavior.setTargetTile (null);

		for(int i=0; i < tempHeroesTiles.Length;i++){
			if(tempHeroesTiles[i].getOccupant() != null){
				if (combatBehavior.getChoosedSkill ().getType () == Skill.Types.Melee) {
					if (Mathf.Abs (tempHeroesTiles [i].getIndex() - character.getPosition ()) <= combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
						if (combatBehavior.getTargetTile () != null) {
							if(combatBehavior.getTargetTile().getOccupant() != null){
								if(tempHeroesTiles[i].getOccupant().getHp() > combatBehavior.getTargetTile().getOccupant().getHp()){
									combatBehavior.setTargetTile (tempHeroesTiles [i]);
								}
							}
						} else {
							combatBehavior.setTargetTile (tempHeroesTiles [i]);
						}
					}
				} else {
					if (Mathf.Abs (tempHeroesTiles [i].getIndex() - character.getPosition ()) > combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
						if (combatBehavior.getTargetTile () != null) {
							if(combatBehavior.getTargetTile().getOccupant() != null){
								if(tempHeroesTiles[i].getOccupant().getHp() > combatBehavior.getTargetTile().getOccupant().getHp()){
									combatBehavior.setTargetTile (tempHeroesTiles [i]);
								}
							}
						} else {
							combatBehavior.setTargetTile (tempHeroesTiles [i]);
						}
					}
				}
			}
		}

//		for (int i=0; i < tempHeroesTiles.Length; i++){
//			if (combatBehavior.getTargetTile () != null) {
//				if (combatBehavior.getTargetTile ().getOccupant () != null) {
//					if (tempHeroesTiles [i].getOccupant ().getHp () > combatBehavior.getTargetTile ().getOccupant ().getHp ()) {
//						if (combatBehavior.getChoosedSkill ().getType () == Skill.Types.Melee) {
//							if (Mathf.Abs (tempHeroesTiles [i].getOccupant ().getPosition () - character.getPosition ()) <= combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
//								combatBehavior.setTargetTile (tempHeroesTiles [i]);
//							}
//						} else {
//							if (Mathf.Abs (tempHeroesTiles [i].getOccupant ().getPosition () - character.getPosition ()) > combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
//								if (character.CanIHitWith (tempHeroesTiles [i], combatBehavior.getChoosedSkill ().getRangedEffect ())) {
//									combatBehavior.setTargetTile (tempHeroesTiles [i]);
//								}
//							}
//						}
//					}
//				} else {
//					if (combatBehavior.getChoosedSkill ().getType () == Skill.Types.Melee) {
//						if (Mathf.Abs (tempHeroesTiles [i].getOccupant ().getPosition () - character.getPosition ()) <= combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
//							combatBehavior.setTargetTile (tempHeroesTiles [i]);
//						}
//					} else {
//						if (Mathf.Abs (tempHeroesTiles [i].getOccupant ().getPosition () - character.getPosition ()) > combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
//							if (character.CanIHitWith (tempHeroesTiles [i], combatBehavior.getChoosedSkill ().getRangedEffect ())) {
//								combatBehavior.setTargetTile (tempHeroesTiles [i]);
//							}
//						}
//					}
//				}
//			} else {
//				if (combatBehavior.getChoosedSkill ().getType () == Skill.Types.Melee) {
//					if (Mathf.Abs (tempHeroesTiles [i].getOccupant ().getPosition () - character.getPosition ()) <= combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
//						combatBehavior.setTargetTile (tempHeroesTiles [i]);
//					}
//				} else {
//					if (Mathf.Abs (tempHeroesTiles [i].getOccupant ().getPosition () - character.getPosition ()) > combatBehavior.getChoosedSkill ().getMeleeEffect ().getRange ()) {
//						if (character.CanIHitWith (tempHeroesTiles [i], combatBehavior.getChoosedSkill ().getRangedEffect ())) {
//							combatBehavior.setTargetTile (tempHeroesTiles [i]);
//						}
//					}
//				}
//			}
//		}

	}

	public TAAtkHealthier(CombatBehavior combatBehavior){
		this.combatBehavior = combatBehavior;
		this.character = combatBehavior.getCharacter ();
	}
}
