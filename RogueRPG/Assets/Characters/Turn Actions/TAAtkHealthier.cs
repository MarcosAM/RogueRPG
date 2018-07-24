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


		//TODO Definir quando o equipamento é primariamente melee ou ranged
		for (int i = 0; i < tempHeroesTiles.Length; i++) {
			if (combatBehavior.getTargetTile() != null) {
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
	}
}
