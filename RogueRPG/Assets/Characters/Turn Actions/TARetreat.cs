using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TARetreat : TurnAction {
	
	public override void tryToDefineEquipSkillTargetFor ()
	{
		base.tryToDefineEquipSkillTargetFor ();
		combatBehavior.setChoosedSkill (null);
		combatBehavior.setTargetTile (null);

		foreach (Skill skill in character.getUsableSkills()){
			foreach(SkillEffect skillEffect in skill.getAllSkillEffects()){
				if(skillEffect.getKind() == SkillEffect.Kind.Movement){
					if (combatBehavior.getChoosedSkill () != null) {
						if(skillEffect.getRange() > combatBehavior.getChoosedSkill().getAlliesEffect().getRange()){
							combatBehavior.setChoosedSkill (skill);
						}
					} else {
						combatBehavior.setChoosedSkill (skill);
					}
				}
			}
		}

		if (combatBehavior.getChoosedSkill () != null) {
			Battleground.Tile[] tempHeroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
			Battleground.Tile[] tempEnemiesTiles = DungeonManager.getInstance ().getBattleground ().getEnemiesTiles ();

			int minIndex = character.getPosition () - combatBehavior.getChoosedSkill ().getAlliesEffect ().getRange ();
			if (minIndex < 0)
				minIndex = 0;
			int maxIndex = character.getPosition () + combatBehavior.getChoosedSkill ().getAlliesEffect ().getRange ();
			if (maxIndex > tempEnemiesTiles.Length - 1)
				maxIndex = tempEnemiesTiles.Length - 1;

			int mostDistant = 0;
			int currentDistance = 0;
			int leftIndex = 0;
			int rightIndex = 0;
			for (int i = 0; i < tempEnemiesTiles.Length; i++) {
				currentDistance = 0;
				if (i >= minIndex && i <= maxIndex) {
					for (int j = 0; j < tempHeroesTiles.Length; j++) {
						leftIndex = i - j;
						if (leftIndex < 0)
							leftIndex = 0;
						rightIndex = i + j;
						if (rightIndex > tempHeroesTiles.Length - 1)
							rightIndex = tempHeroesTiles.Length - 1;
						if (tempHeroesTiles [leftIndex].getOccupant () != null || tempHeroesTiles [rightIndex].getOccupant () != null) {
							break;
						} else {
							currentDistance++;
						}
					}
					if (currentDistance > mostDistant) {
						combatBehavior.setTargetTile (tempEnemiesTiles [i]);
						mostDistant = currentDistance;
					} else {
						if (combatBehavior.getTargetTile () != null) {
							if (currentDistance == mostDistant && (combatBehavior.getTargetTile ().getOccupant () != null && tempEnemiesTiles [i].getOccupant () == null)) {
								combatBehavior.setTargetTile (tempEnemiesTiles [i]);
								mostDistant = currentDistance;
							}
						}
					}
				}
			}
		}
	} 
}
