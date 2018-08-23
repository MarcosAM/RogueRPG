using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleground : MonoBehaviour {

	[SerializeField]List<Character> heroSide = new List<Character> ();
	[SerializeField]List<Character> enemySide = new List<Character> ();
	[SerializeField]Tile[] heroTiles = new Tile[4];
	[SerializeField]Tile[] enemyTiles = new Tile[4];
	CombHUDManager cHUDManager;

	void Awake(){
		cHUDManager = FindObjectOfType<CombHUDManager> ();
		heroSide.Capacity = 4;
		enemySide.Capacity = 4;
		CreateTiles ();
	}

//	public void AddToHeroSide(Character character){
//		if(heroSide.Count<4){
//			heroSide.Add (character);
//			cHUDManager.ShowCombatants(heroSide,enemySide);
//		}
//	}

//	public void AddToEnemySide(Character character){
//		if(enemySide.Count<4){
//			enemySide.Add (character);
//			cHUDManager.ShowCombatants(heroSide,enemySide);
//		}
//	}

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
				int oldPosition = enemySide.IndexOf (character);
				enemySide[position] = character;
				enemySide[oldPosition] = null;
			}
		}
		ShowCharactersToThePlayer ();
	}

//	public void PutCharactersInBattleground(){
//		enemySide.Clear ();
//		Character[] playableCharacters = FindObjectsOfType<PlayableCharacter> ();
//		Character[] nonPlayableCharactersTemp = FindObjectsOfType<NonPlayableCharacter> ();
//		Character[] nonPlayableCharacters = new Character[4];
//		int count = 0;
//		for(int i = 0;i<nonPlayableCharactersTemp.Length;i++){
//			if(nonPlayableCharactersTemp[i].isAlive()){
//				nonPlayableCharacters [count] = nonPlayableCharactersTemp [i];
//				count++;
//			}
//		}
//		for(int i = 0; i<4; i++){
//			if (i < playableCharacters.Length) {
//				AddToHeroSide (playableCharacters [i]);
//			} else {
//				AddToHeroSide (null);
//			}
//			if (i < nonPlayableCharacters.Length) {
//				AddToEnemySide (nonPlayableCharacters [i]);
//			} else {
//				AddToEnemySide (null);
//			}
//		}
//		ShowCharactersToThePlayer ();
//	}

	public void ShowCharactersToThePlayer(){
		setOccupantsOfAllTiles (heroSide,enemySide);
//		cHUDManager.ShowCombatants(heroSide,enemySide);
		cHUDManager.ShowCombatants(heroTiles,enemyTiles);
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

	public Tile[] getHeroesTiles(){return heroTiles;}
	public Tile[] getEnemiesTiles(){return enemyTiles;}

	public Tile[] getMySideTiles(bool side){
		if (side)
			return heroTiles;
		else
			return enemyTiles;
	}

	public Tile[] getMyEnemiesTiles(bool side){
		if (side)
			return enemyTiles;
		else
			return heroTiles;
	}

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

	void CreateTiles(){
		for(int i =0;i<heroTiles.Length;i++){
			heroTiles [i] = new Tile (i,true);
		}
		for(int i =0;i<enemyTiles.Length;i++){
			enemyTiles [i] = new Tile (i,false);
		}
	}

	void setOccupantsOfAllTiles(List<Character> heroSide, List<Character> enemySide){
		for(int i=0;i<heroSide.Count;i++){
			heroTiles [i].setOccupant (heroSide[i]);
		}
		for(int i=0;i<enemySide.Count;i++){
			enemyTiles [i].setOccupant (enemySide[i]);
		}

//		string side = "";
//		foreach(Tile tile in heroTiles){
//			if (tile.getOccupant() != null) {
//				side += tile.getOccupant ().getName () + " - ";
//			} else {
//				side += "null - ";
//			}
//		}
//		print (side);
//		side = "";
//		foreach(Tile tile in enemyTiles){
//			if (tile.getOccupant() != null) {
//				side += tile.getOccupant ().getName () + " - ";
//			} else {
//				side += "null - ";
//			}
//		}
//		print (side);
	}

	public class Tile{
		[SerializeField]Character occupant;
		int index;
//		Vector2 localPosition;
		bool fromHero;

		public Tile(int index){
			this.index = index;
		}
		public Tile(int index, Vector2 localPosition){
			this.index = index;
//			this.localPosition = localPosition;
		}
		public Tile(int index, Vector2 localPosition, bool fromHero){
			this.index = index;
//			this.localPosition = localPosition;
			this.fromHero = fromHero;
		}
		public Tile(int index, bool fromHero){
			this.index = index;
			//			this.localPosition = localPosition;
			this.fromHero = fromHero;
		}
		public void setOccupant(Character occupant) {this.occupant = occupant;}
		public Character getOccupant() {return occupant;}
		public int getIndex(){return index;}
//		public Vector2 getLocalPosition(){return localPosition;}
		public Vector2 getLocalPosition(){
			if (fromHero) {
				return FindObjectOfType<CombHUDManager> ().getHeroesCombatantHUD () [index].getRectTransform ().localPosition;
			} else {
				return FindObjectOfType<CombHUDManager> ().getEnemiesCombatantHUD () [index].getRectTransform ().localPosition;
			}
		}
		public bool isFromHero(){return fromHero;}
	}
}