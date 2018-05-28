using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour {

	List<Character> heroSide = new List<Character> ();
	List<Character> enemySide = new List<Character> ();
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
			heroSide.Remove (character);
			AddToHeroSide (character);
		} else {
			enemySide.Remove (character);
			AddToEnemySide (character);
		}
	}
}
