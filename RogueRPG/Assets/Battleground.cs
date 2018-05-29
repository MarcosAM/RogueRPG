using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour {

	[SerializeField]List<Character> heroSide = new List<Character> ();
	[SerializeField]List<Character> enemySide = new List<Character> ();
	CombHUDManager cHUDManager;

	void Awake(){
		heroSide.Capacity = 4;
		enemySide.Capacity = 4;
		cHUDManager = FindObjectOfType<CombHUDManager> ();
	}

	public void AddToHeroSide(Character character){
		if(heroSide.Count<4){
			heroSide.Add (character);
			if(character != null){
				character.getMovement().setPosition(heroSide.IndexOf(character));
				cHUDManager.ShowCharacterAt (character,heroSide.IndexOf(character));
			}
		}
	}

	public void AddToEnemySide(Character character){
		if(enemySide.Count<4){
			enemySide.Add (character);
			if(character != null){
				character.getMovement ().setPosition(enemySide.IndexOf(character));
				cHUDManager.ShowCharacterAt (character,enemySide.IndexOf(character));
			}
		}
	}

	public void MoveCharacterTo(Character character, int position){
		if (character.getIsHero ()) {
			if (heroSide [position] != null) {
				heroSide.Remove (character);
				heroSide.Insert (position, character);
			} else {
				int oldPosition = heroSide.IndexOf (character);
				heroSide[position] = character;
				heroSide [oldPosition] = null;
			}
			character.getMovement ().setPosition(heroSide.IndexOf(character));
		} else {
			if (enemySide [position] != null) {
				enemySide.Remove (character);
				enemySide.Insert (position, character);
			} else {
				int oldPosition = heroSide.IndexOf (character);
				enemySide[position] = character;
				enemySide[oldPosition] = null;
			}
			character.getMovement ().setPosition(enemySide.IndexOf(character));
		}
		cHUDManager.ShowCombatants(heroSide,enemySide);
	}
}
