﻿using System.Collections;
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
			cHUDManager.ShowCombatants(heroSide,enemySide);
		}
	}

	public void AddToEnemySide(Character character){
		if(enemySide.Count<4){
			enemySide.Add (character);
			cHUDManager.ShowCombatants(heroSide,enemySide);
		}
	}

	public void MoveCharacterTo(Character character, int position){
		if (character.isPlayable ()) {
			if (heroSide [position] != null) {
				heroSide.Remove (character);
				heroSide.Insert (position, character);
			} else {
				int oldPosition = heroSide.IndexOf (character);
				heroSide[position] = character;
				heroSide [oldPosition] = null;
			}
		} else {
			if (enemySide [position] != null) {
				enemySide.Remove (character);
				enemySide.Insert (position, character);
			} else {
				int oldPosition = heroSide.IndexOf (character);
				enemySide[position] = character;
				enemySide[oldPosition] = null;
			}
		}
		ShowCharactersToThePlayer ();
	}

	public void PutCharactersInBattleground(){
		enemySide.Clear ();
		Character[] playableCharacters = FindObjectsOfType<PlayableCharacter> ();
		Character[] nonPlayableCharactersTemp = FindObjectsOfType<NonPlayableCharacter> ();
		Character[] nonPlayableCharacters = new Character[4];
		int count = 0;
		for(int i = 0;i<nonPlayableCharactersTemp.Length;i++){
			if(nonPlayableCharactersTemp[i].isAlive()){
				nonPlayableCharacters [count] = nonPlayableCharactersTemp [i];
				count++;
			}
		}
		for(int i = 0; i<4; i++){
			if (i < playableCharacters.Length) {
				AddToHeroSide (playableCharacters [i]);
			} else {
				AddToHeroSide (null);
			}
			if (i < nonPlayableCharacters.Length) {
				AddToEnemySide (nonPlayableCharacters [i]);
			} else {
				AddToEnemySide (null);
			}
		}
		ShowCharactersToThePlayer ();
	}

	public void ShowCharactersToThePlayer(){
		cHUDManager.ShowCombatants(heroSide,enemySide);
	}

	public int getPositionOf (Character character){
		if (character.isPlayable ()) {
			return heroSide.IndexOf (character);
		} else {
			return enemySide.IndexOf (character);
		}
	}

	public int HowManyHeroes(){
		int heroesCountdown=0;
		for(int i = 0;i<heroSide.Count;i++){
			if(heroSide[i] != null){
				heroesCountdown++;
			}
		}
		return heroesCountdown;
	}

	public int HowManyEnemies(){
		int enemiesCountdown=0;
		for(int i = 0;i<enemySide.Count;i++){
			if(enemySide[i] != null){
				enemiesCountdown++;
			}
		}
		return enemiesCountdown;
	}

	public List<Character> getHeroSide(){
		return heroSide;
	}
	public List<Character> getEnemySide(){
		return enemySide;
	}
//	public void setHeroSide (List<Character> heroes){
//		this.heroSide = heroes;
//		for(int i = 0;i<=4-heroes.Count;i++){
//			this.heroSide.Add(null);
//		}
//	}
	public void ClearAndSetASide(List<Character> side){
		bool sideIsPlayers = false;
		int sideSize = side.Count;
		for(int i=0;i<side.Count;i++){
			if(side[i]!=null){
				sideIsPlayers = side[i].isPlayable();
			}
		}
		if (sideIsPlayers) {
			heroSide.Clear ();
			this.heroSide = side;
			for (int i = 0; i < 4 - sideSize; i++) {
				this.heroSide.Add (null);
			}
		} else {
			enemySide.Clear ();
			this.enemySide = side;
			for (int i = 0; i < 4 - sideSize; i++) {
				this.enemySide.Add (null);
			}
		}
	}
}