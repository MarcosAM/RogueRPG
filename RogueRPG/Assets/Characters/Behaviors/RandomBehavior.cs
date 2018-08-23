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
		choosedSkill = character.getUsableSkills()[Random.Range(0,character.getUsableSkills().Count)];
	}

	public void ChooseTarget ()
	{
		targetTile = null;
		Battleground.Tile[] tempHeroesTiles = DungeonManager.getInstance ().getBattleground ().getHeroesTiles ();
		Battleground.Tile[] tempEnemiesTiles = DungeonManager.getInstance ().getBattleground ().getEnemiesTiles ();
		if(Random.value > 0.8f){
			int heroesAliveAtRange = 0;
			for (int i = 0; i < tempHeroesTiles.Length; i++) {
				if (tempHeroesTiles [i].getOccupant () != null)
				if (tempHeroesTiles [i].getOccupant ().isAlive () && Mathf.Abs(i - character.getPosition()) <= choosedSkill.getMeleeEffect().getRange())
					heroesAliveAtRange++;
			}
			if (heroesAliveAtRange > 0) {
				Battleground.Tile[] heroesTile = new Battleground.Tile[heroesAliveAtRange];
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
				//			character.getHUD ().UseSkillAnimation ();
			}
		}
		else {
			targetTile = DungeonManager.getInstance ().getBattleground ().getEnemiesTiles () [character.getPosition ()];
		}
		if(targetTile == null){
			int minIndex = character.getPosition () - choosedSkill.getAlliesEffect ().getRange ();
			if (minIndex < 0)
				minIndex = 0;
			int maxIndex = character.getPosition () + choosedSkill.getAlliesEffect ().getRange ();
			if (maxIndex >= tempHeroesTiles.Length)
				maxIndex = tempHeroesTiles.Length - 1;
			int direction = 1;
			for(int i = 0;i<tempHeroesTiles.Length;i++){
				if (Random.value > 0.5f)
					direction *= -1;
				if(character.getPosition() + i*direction >= 0 && character.getPosition() + i*direction <= tempHeroesTiles.Length - 1){
					if(tempHeroesTiles[character.getPosition() + i*direction].getOccupant()){
						targetTile = tempEnemiesTiles [character.getPosition() + i * direction];
						if (character.getPosition () + i * direction >= minIndex && character.getPosition () + i * direction <= maxIndex) {
							UseSkill ();
							return;
						}
					}
				}
				if(character.getPosition() - i*direction >= 0 && character.getPosition() - i*direction <= tempHeroesTiles.Length - 1){
					if(tempHeroesTiles[character.getPosition() - i*direction].getOccupant()){
						targetTile = tempEnemiesTiles [character.getPosition() - i * direction];
						if (character.getPosition () - i * direction >= minIndex && character.getPosition () - i * direction <= maxIndex) {
							UseSkill ();
							return;
						}
					}
				}
			}
		}
		UseSkill ();
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
