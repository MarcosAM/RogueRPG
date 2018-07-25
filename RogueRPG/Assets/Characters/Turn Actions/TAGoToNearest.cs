using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAGoToNearest : TurnAction {

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

			float hpFromLeft;
			float hpFromRight;
			int leftIndex = 0;
			int rightIndex = 0;
			for (int i = 0; i < tempEnemiesTiles.Length; i++) {
				if (i >= minIndex && i <= maxIndex) {
					for (int j = 0; j < tempHeroesTiles.Length; j++) {
						hpFromLeft = 0;
						hpFromRight = 0;
						leftIndex = i - j;
						if (leftIndex < 0)
							leftIndex = 0;
						rightIndex = i + j;
						if (rightIndex > tempHeroesTiles.Length - 1)
							rightIndex = tempHeroesTiles.Length - 1;
						if (tempHeroesTiles [leftIndex].getOccupant () != null || tempHeroesTiles [rightIndex].getOccupant () != null) {
							break;
						}
						if(tempHeroesTiles[leftIndex].getOccupant() != null){
							if(tempHeroesTiles[leftIndex].getOccupant().isAlive()){
								if(Mathf.Abs(tempHeroesTiles[leftIndex].getOccupant().getPosition() - character.getPosition()) <= combatBehavior.getChoosedSkill().getAlliesEffect().getRange() ){
									combatBehavior.setTargetTile (tempHeroesTiles[leftIndex]);
									return;
								}
							}
						}
						if(tempHeroesTiles[rightIndex].getOccupant() != null){
							if(tempHeroesTiles[rightIndex].getOccupant().isAlive()){
								if(Mathf.Abs(tempHeroesTiles[rightIndex].getOccupant().getPosition() - character.getPosition()) <= combatBehavior.getChoosedSkill().getAlliesEffect().getRange() ){
									combatBehavior.setTargetTile (tempHeroesTiles[rightIndex]);
									return;
								}
							}
						}
					}
				}
			}
		}
	}

	public TAGoToNearest(CombatBehavior combatBehavior){
		this.combatBehavior = combatBehavior;
		this.character = combatBehavior.getCharacter ();
	}
}
